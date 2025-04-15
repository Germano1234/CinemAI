
# 🔧 Setup Instructions

## 🧠 Python AI Recommender

### 1. Navigate to the Python project folder
```bash
cd MovieRecommenderAI
```

### 2. (Optional) Create a virtual environment
```bash
python -m venv venv
```

### 3. Activate the virtual environment
- On **Windows**:
```bash
venv\Scripts\activate
```
- On **Mac/Linux**:
```bash
source venv/bin/activate
```

### 4. Install dependencies
```bash
pip install flask
pip install textblob
pip install scikit-learn
pip install numpy
pip install pandas
```

> Optionally, you can combine all the above into:
```bash
pip install flask textblob scikit-learn numpy pandas
```

### 5. Download NLTK corpora for TextBlob (first-time only)
```bash
python -m textblob.download_corpora
```

### 6. Run the app
- To run the main recommender:
```bash
python app.py
```
- To run the recommender with the large dataset:
```bash
python app_large.py
```

### 7. Open in browser
Visit:
```
http://127.0.0.1:5000/
```

---

## 💻 Windows Forms Movie Theater System (C# + SQL Server)

### 1. Open solution in Visual Studio
- Open the file:
```
MovieTheater.sln
```

### 2. Restore NuGet packages (if prompted)
- Visual Studio should do this automatically.
- If not, go to:
```
Tools > NuGet Package Manager > Manage NuGet Packages for Solution
```
and install any missing packages.

### 3. Build the project
```bash
Ctrl + Shift + B
```

### 4. Create the database
- Open SQL Server Management Studio (SSMS).
- Open and execute the script:
```
script.sql
```

### 5. Check the connection string
- Make sure the database connection string in your project (typically in `App.config`) points to your SQL Server instance and the database you just created.

### 6. Run the app
```bash
F5
```

---

## 📦 Required Software to Install

### For Python AI Recommender:
- Python 3.8+
- pip
- (Optional) Virtualenv

### For C# Movie Theater System:
- Visual Studio 2022 or later
- .NET Framework Developer Pack (4.7+)
- SQL Server (Express or full)
- SQL Server Management Studio (SSMS)
```
