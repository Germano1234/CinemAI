from flask import Flask, request, jsonify
import pandas as pd
import numpy as np
import ast
import random
from genre_nlp import extract_genres_from_text, KNOWN_GENRES
from textblob import TextBlob
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
from sklearn.neighbors import NearestNeighbors

app = Flask(__name__)

# 🔹 Load and preprocess large dataset
def load_movies(filepath):
    df = pd.read_csv(filepath)

    # Clean columns
    df = df[['title', 'genres', 'overview']].dropna()

    # Convert genres from JSON-like string to flat comma-separated string
    def flatten_genres(genre_str):
        try:
            genre_list = ast.literal_eval(genre_str)
            return ",".join(g['name'] for g in genre_list if 'name' in g)
        except:
            return ""

    df['genre'] = df['genres'].apply(flatten_genres)
    df['description'] = df['overview']
    df = df[['title', 'genre', 'description']]  # Final cleaned structure

    return df.to_dict(orient='records')  # list of dicts

# 🔁 Load at app startup
movie_db = load_movies("large_movies.csv")

# 🧠 Spell corrector
def correct_input_text(text):
    return str(TextBlob(text).correct())

# 🔹 1. Recommend by text input (smart hybrid logic)
@app.route("/recommend_from_list", methods=["POST"])
def recommend_from_list_large():
    data = request.get_json()
    user_input = data.get("text", "")

    if not user_input:
        return jsonify({"error": "No user input provided"}), 400

    user_input = correct_input_text(user_input)
    extracted_genres = extract_genres_from_text(user_input)

    movie_titles = [m["title"] for m in movie_db]
    movie_descriptions = [m.get("description", "") for m in movie_db]

    tfidf = TfidfVectorizer(stop_words='english')
    tfidf_matrix = tfidf.fit_transform([user_input] + movie_descriptions)
    similarities = cosine_similarity(tfidf_matrix[0:1], tfidf_matrix[1:]).flatten()

    combined_scores = []
    for i, movie in enumerate(movie_db):
        # Genre score (1.0 if it matches all extracted genres)
        if extracted_genres:
            movie_genres = [g.strip() for g in movie["genre"].split(",") if g.strip()]
            genre_score = 1.0 if all(g in movie_genres for g in extracted_genres) else 0.0
        else:
            genre_score = 0.0

        final_score = genre_score * 0.6 + similarities[i] * 0.4
        combined_scores.append((movie["title"], final_score))

    if max(score for _, score in combined_scores) < 0.05:
        fallback = [m["title"] for m in random.sample(movie_db, min(5, len(movie_db)))]
        return jsonify({"recommendations": fallback})

    combined_scores.sort(key=lambda x: x[1], reverse=True)
    top_titles = [title for title, _ in combined_scores[:10]]
    return jsonify({"recommendations": top_titles})

# 🔹 2. Recommend by watched movie history (KNN-based)
@app.route("/recommend_by_history", methods=["POST"])
def recommend_by_history_large():
    data = request.get_json()
    watched = data.get("watched", [])  # [{title, genre, description}]

    if not watched:
        return jsonify({"error": "No watched movies provided"}), 400

    def genre_vector(movie):
        genres = [g.strip() for g in movie["genre"].split(",") if g.strip()]
        return np.array([1 if g in genres else 0 for g in KNOWN_GENRES])

    watched_vectors = [genre_vector(m) for m in watched]
    profile_vector = np.mean(watched_vectors, axis=0)

    descriptions = [m.get("description", "") for m in movie_db]
    tfidf = TfidfVectorizer(stop_words='english', max_features=100)
    tfidf_matrix = tfidf.fit_transform(descriptions).toarray()

    genre_matrix = np.array([genre_vector(m) for m in movie_db])
    hybrid_matrix = np.hstack([
        genre_matrix * 0.6,
        tfidf_matrix * 0.4
    ])

    profile_vector_extended = np.hstack([
        profile_vector * 0.6,
        np.zeros(tfidf_matrix.shape[1])
    ])

    watched_titles = {m["title"] for m in watched}
    candidate_indices = [i for i, m in enumerate(movie_db) if m["title"] not in watched_titles]

    if not candidate_indices:
        return jsonify({"error": "No unseen movies available"}), 400

    candidate_vectors = hybrid_matrix[candidate_indices]

    k = min(10, len(candidate_vectors))
    knn = NearestNeighbors(n_neighbors=k, metric="cosine")
    knn.fit(candidate_vectors)
    distances, indices = knn.kneighbors([profile_vector_extended])

    recommendations = [movie_db[candidate_indices[i]]["title"] for i in indices[0]]
    return jsonify({"recommendations": recommendations})

if __name__ == "__main__":
    app.run(debug=True)
