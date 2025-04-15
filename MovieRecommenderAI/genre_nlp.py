import re

# 🎯 Genres your system can detect and use for vector representation
KNOWN_GENRES = [
    "Action", "Comedy", "Drama", "Horror", "Romance", "Animated", "Religious"
]

# 🧠 Maps natural language keywords to genres
synonym_map = {
    # Horror
    "scary": "Horror", 
    "horror": "Horror", 
    "ghost": "Horror", 
    "killer": "Horror", 
    "haunted": "Horror",

    # Comedy
    "funny": "Comedy", 
    "comedy": "Comedy", 
    "laugh": "Comedy", 
    "hilarious": "Comedy", 
    "joke": "Comedy",

    # Romance
    "romantic": "Romance", 
    "love": "Romance", 
    "romance": "Romance", 
    "date": "Romance",
    "relationship": "Romance", 
    "heart": "Romance",

    # Action
    "action": "Action", 
    "exciting": "Action", 
    "explosion": "Action", 
    "thrill": "Action",
    "fight": "Action", 
    "adrenaline": "Action", 
    "thriller": "Action",

    # Drama
    "sad": "Drama", 
    "drama": "Drama", 
    "emotional": "Drama", 
    "serious": "Drama",
    "deep": "Drama", 
    "life": "Drama",

    # Animated
    "animated": "Animated", 
    "cartoon": "Animated", 
    "kids": "Animated", 
    "family": "Animated",
    "pixar": "Animated", 
    "disney": "Animated",

    # Religious
    "religious": "Religious"
}

# 🔍 Extracts genres from natural language input
def extract_genres_from_text(text):
    text = text.lower()
    found = set()

    for word, genre in synonym_map.items():
        pattern = r"\b" + re.escape(word) + r"\b"
        if re.search(pattern, text):
            found.add(genre)

    return list(found)

# 🧪 Test (optional)
if __name__ == "__main__":
    user_input = input("Enter a movie preference: ")
    genres = extract_genres_from_text(user_input)
    print("Extracted genres:", genres)

