using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieTheater.Model;
using MovieTheater.Repository;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using MovieTheater.Repository.Interfaces;

namespace MovieTheater.View
{
    public partial class FrmMovies : Form
    {
        public FrmMovies()
        {
            InitializeComponent();
            LoadMovieButtons();
            lblWelcome.Text = $"Welcome, {FrmLogIn.logged.Name}";
            AutoRec();
            
        }

        private async void AutoRec()
        {
            string recommended = await GetAutoRecommendation(FrmLogIn.logged.UserID);
            lblAutoRec.Text = $"🎬 Recommended for you: {recommended}";
        }

        RMovie Rmovie = new RMovie();
        public static int idChosenMovie;

        private void LoadMovieButtons()
        {
            moviesPanel.Controls.Clear();

            List<Movie> movies = Rmovie.getAll().ToList();

            foreach (Movie movie in movies)
            {
                Button movieButton = new Button();
                movieButton.Text = movie.Title;
                movieButton.Width = 200;
                movieButton.Height = 50;
                movieButton.Tag = movie.MovieID;

                movieButton.Click += MovieButton_Click;

                moviesPanel.Controls.Add(movieButton);
            }
        }

        private void MovieButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            idChosenMovie = (int)clickedButton.Tag;

            FrmShowtime frmShowtime = new FrmShowtime();
            this.Hide();
            frmShowtime.ShowDialog();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogIn frmLogIn = new FrmLogIn();
            frmLogIn.ShowDialog();
        }

        private void ViewMyTicket_Click(object sender, EventArgs e)
        {
            FrmViewMyTicket frmViewMyTicket = new FrmViewMyTicket();
            frmViewMyTicket.ShowDialog();
        }



        public class RecommendationResponse
        {
            public List<string> recommendations { get; set; }
        }
        private async Task<List<string>> GetRecommendationFromUserText(string userInput)
        {
            using (HttpClient client = new HttpClient())
            {
                List<MovieForAPI> movieList = new List<MovieForAPI>();

                List<Movie> movieEntities = Rmovie.getAll().ToList(); // Get your movie list from SQL

                foreach (Movie movie in movieEntities)
                {
                    movieList.Add(new MovieForAPI
                    {
                        title = movie.Title,
                        genre = movie.Genre,
                        description = movie.Description
                    });
                }

                var requestData = new
                {
                    text = userInput,
                    movies = movieList
                };

                string json = JsonConvert.SerializeObject(requestData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://127.0.0.1:5000/recommend_from_list", content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("API Error: " + responseString);
                    return new List<string>();
                }

                RecommendationResponse result = JsonConvert.DeserializeObject<RecommendationResponse>(responseString);
                return result.recommendations;
            }
        }

        private async void btnGetNLPRecommendation_Click(object sender, EventArgs e)
        {
            string userInput = txtNLPInput.Text;
            if (string.IsNullOrWhiteSpace(userInput)) return;

            List<string> recommendations = await GetRecommendationFromUserText(userInput);

            if (recommendations.Count > 0)
                lblNLPRecommendation.Text = $"Try: {recommendations[0]}";
            else
                lblNLPRecommendation.Text = "Sorry, no recommendations found.";
        }

        private async Task<string> GetAutoRecommendation(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                RTicket ticketRepo = new RTicket();
                List<Movie> watchedMovies = ticketRepo.GetWatchedMoviesByUserId(userId);

                RMovie movieRepo = new RMovie();
                List<Movie> allMovies = movieRepo.getAll().ToList();

                List<MovieForAPI> watched = new List<MovieForAPI>();
                foreach (Movie m in watchedMovies)
                {
                    watched.Add(new MovieForAPI { 
                        title = m.Title, 
                        genre = m.Genre,
                        description = m.Description
                    });
                }

                List<MovieForAPI> all = new List<MovieForAPI>();
                foreach (Movie m in allMovies)
                {
                    all.Add(new MovieForAPI { 
                        title = m.Title, 
                        genre = m.Genre,
                        description = m.Description
                    });
                }

                var requestData = new
                {
                    watched = watched,
                    all_movies = all
                };

                string json = JsonConvert.SerializeObject(requestData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://127.0.0.1:5000/recommend_by_history", content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return "";
                }

                RecommendationResponse result = JsonConvert.DeserializeObject<RecommendationResponse>(responseString);
                return result.recommendations.Count > 0 ? result.recommendations[0] : "No recommendation available.";
            }
        }

    }
}
