from flask import Flask, request, jsonify
from genre_nlp import extract_genres_from_text, KNOWN_GENRES
import numpy as np
import random
from textblob import TextBlob
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
from sklearn.neighbors import NearestNeighbors

app = Flask(__name__)

# 🔹 1. Recommendation Based on User Text Input
@app.route("/recommend_from_list", methods=["POST"])
def recommend_from_list():
    data = request.get_json()
    user_input = data.get("text", "")
    movies = data.get("movies", [])

    # 🛡️ Validate input
    if not user_input or not movies:
        return jsonify({"error": "Missing input or movie list"}), 400

    # ✅ Step 1: Spell-correct the input to handle typos like "romnatic" or "comed"
    user_input = str(TextBlob(user_input).correct())

    # 🎯 Step 2: Try to extract genres from the corrected input using NLP + fuzzy match
    extracted_genres = extract_genres_from_text(user_input)

    # 🧠 Step 3: Compute TF-IDF similarity between user input and movie descriptions
    movie_titles = [m["title"] for m in movies]
    movie_descriptions = [m.get("description", "") for m in movies]

    tfidf = TfidfVectorizer(stop_words='english')
    all_texts = [user_input] + movie_descriptions
    tfidf_matrix = tfidf.fit_transform(all_texts)
    similarities = cosine_similarity(tfidf_matrix[0:1], tfidf_matrix[1:]).flatten()

    # 🧮 Step 4: Combine genre matching and description similarity into a hybrid score
    combined_scores = []
    for i, movie in enumerate(movies):
        # 🎬 Genre match = 1.0 if movie contains all extracted genres, else 0.0
        if extracted_genres:
            movie_genres = [g.strip() for g in movie["genre"].split(",") if g.strip()]
            genre_score = 1.0 if all(g in movie_genres for g in extracted_genres) else 0.0
        else:
            genre_score = 0.0

        # 💡 Final score = 60% genre match + 40% TF-IDF similarity
        final_score = genre_score * 0.6 + similarities[i] * 0.4
        combined_scores.append((movie["title"], final_score))

    # ⚠️ Step 5: Handle unclear input with random fallback
    if max(score for _, score in combined_scores) < 0.05:
        fallback_titles = [m["title"] for m in random.sample(movies, min(5, len(movies)))]
        return jsonify({"recommendations": fallback_titles})

    # 🔝 Step 6: Sort by score and return top 10
    combined_scores.sort(key=lambda x: x[1], reverse=True)
    top_titles = [title for title, _ in combined_scores[:10]]
    
    return jsonify({"recommendations": top_titles})




# 🔹 2. Recommendation Based on Watched Movie History (KNN Hybrid)
@app.route("/recommend_by_history", methods=["POST"])
def recommend_by_history():
    data = request.get_json()
    watched = data.get("watched", [])
    all_movies = data.get("all_movies", [])

    if not watched or not all_movies:
        return jsonify({"error": "Insufficient data"}), 400

    # Creates a binary genre vector (1 if movie has that genre)
    def genre_vector(movie):
        genres = [g.strip() for g in movie["genre"].split(",") if g.strip()]
        return np.array([1 if g in genres else 0 for g in KNOWN_GENRES])

    # Build the average genre profile vector from watched movies
    watched_genres = [genre_vector(m) for m in watched]
    profile_vector = np.mean(watched_genres, axis=0)

    # TF-IDF description matrix
    all_descriptions = [m.get("description", "") for m in all_movies]
    tfidf = TfidfVectorizer(stop_words='english', max_features=100)
    tfidf_matrix = tfidf.fit_transform(all_descriptions).toarray()

    # Combine genre and description into hybrid features
    genre_matrix = np.array([genre_vector(m) for m in all_movies])
    weighted_matrix = np.hstack([
        genre_matrix * 0.6,     # Genre weight: 60%
        tfidf_matrix * 0.4      # Description weight: 40%
    ])

    # Extend user vector with zeros for TF-IDF dimensions
    profile_vector_extended = np.hstack([
        profile_vector * 0.6,
        np.zeros(tfidf_matrix.shape[1])
    ])

    # Exclude already-watched movies from candidates
    watched_titles = {m["title"] for m in watched}
    candidate_indices = [i for i, m in enumerate(all_movies) if m["title"] not in watched_titles]
    if not candidate_indices:
        return jsonify({"error": "No unseen movies available."}), 400

    candidate_vectors = weighted_matrix[candidate_indices]

    # ✅ Use scikit-learn's KNN to find 10 most similar (closest) movies
    k = min(10, len(candidate_vectors))  # dynamically choose k
    knn = NearestNeighbors(n_neighbors=k, metric="cosine")
    knn.fit(candidate_vectors)
    distances, indices = knn.kneighbors([profile_vector_extended])

    # Convert KNN results into movie titles
    recommendations = [all_movies[candidate_indices[i]]["title"] for i in indices[0]]
    return jsonify({"recommendations": recommendations})

if __name__ == "__main__":
    app.run(debug=True)

