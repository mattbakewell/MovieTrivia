using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using HtmlAgilityPack;
using MovieTrivia.Model;

namespace MovieTrivia.Application
{
    /// <summary>
    /// This service encapsulates the flow of Trivia.
    /// This helps folowing the DRY principle keeping the controllers nice and clean
    /// and hiding the working of the private methods
    /// </summary>
    public class TriviaService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TriviaService(ApplicationDbContext applicationContext)
        {
            _applicationDbContext = applicationContext;
        }

        public Trivia NewTrivia()
        {
            var trivia = new Trivia
            {
                Rounds = new List<Round> { NewRound() },
                PlayerOne = new Player { Name = " Player One" },
                PlayerTwo = new Player { Name = "Player Two" }
            };
            // save the new game before returning it
            // TODO: check movie doesn't already exist or we'll get a key constraint error
            // For demo, it's unlikely (fingers crossed!) but needs to be addressed before production
            _applicationDbContext.Trivia.Add(trivia);
            _applicationDbContext.SaveChanges();

            return trivia;
        }
        public void MakeAnswer(ViewModel.Answer answer)
        {
            // First load the trivia and get the current round
            var trivia = GetTrivia(answer.TriviaId);
            var round = trivia.Rounds.Where(r => r.Id == answer.RoundId).SingleOrDefault();

            // add the answer to the appropriate property depending upon which player is sending the answer
            if (answer.PlayerId == (int)PlayerId.One)
            {
                if (round.PlayerOneAnswer == 0) // Don't allow a double post overwrite the first answer
                    round.PlayerOneAnswer = answer.Year;
            }
            else
            {
                if (round.PlayerTwoAnswer == 0) // Don't allow a double post overwrite the first answer
                    round.PlayerTwoAnswer = answer.Year;
            }

            // save the trivia
            _applicationDbContext.SaveChanges();
        }

        public Trivia GetTrivia(int id)
        {
            // load the trivia
            var trivia = _applicationDbContext.Trivia
                .Include(p1 => p1.PlayerOne)
                .Include(p2 => p2.PlayerTwo)
                .Include(r => r.Rounds).ThenInclude(m => m.Movie)
                .Where(g => g.Id == id).SingleOrDefault();

            AddRoundIfTriviaNotCompleted(trivia);
            return trivia;
        }

        private void AddRoundIfTriviaNotCompleted(Trivia trivia)
        {
            // make sure the trivia isn't a finished one
            if (trivia.Status != "Finished")
            {
                // if the last round is complete, add a new one
                var latestRound = trivia.Rounds.Last();
                if (latestRound.IsFinished)
                {
                    trivia.Rounds.Add(NewRound(latestRound.Counter));
                    _applicationDbContext.SaveChanges();
                }
            }
        }

        private Round NewRound(int lastRoundNumber = 0)
        {
            // Load datasource into Movies Database
            var movies = GetFromWeb();

            // generate a random number between 1 and 250
            var randomGenerator = new Random();
            var randomId = randomGenerator.Next(1, 250);

            // use the number to load the movie
            var movie = movies.Where(m => m.Id == randomId).SingleOrDefault();

            // and return a new round countaining the movie
            return new Round { Counter = lastRoundNumber + 1, Movie = movie };
        }

        private IList<Movie> GetFromWeb()
        {
            // Use HttpClient class to get HTML from IMDB
            // and return list of top 250 movies
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://www.imdb.com/chart/");

            var response = client.GetAsync("top?ref_=nb_mv_3.chttp").Result;

            var htmlResult = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                htmlResult = response.Content.ReadAsStringAsync().Result;
            }

            return ParseMoviesFromHtml(htmlResult);
        }

        private static IList<Movie> ParseMoviesFromHtml(string htmlString)
        {
            // Parse HTML string to a list of movies
            // This uses HtmlAgilityPack to save from relying on regex or parsing it ourselves
            IList<Movie> moviesToReturn = new List<Movie>();

            var html = new HtmlDocument();
            html.LoadHtml(htmlString);

            var titleNodes = html.DocumentNode.SelectNodes("//td")
                .Where(l => l.GetAttributeValue("class", "") == "titleColumn");

            Movie movie;
            string name = null;
            string year = null;
            int counter = 0;

            foreach (HtmlNode link in titleNodes)
            {
                counter++; // TODO: Use IMDB Primary Key instead of counter (Discuss keeping history of movies too)
                name = link.ChildNodes.Where(t => t.Name == "a").First().InnerText;
                year = link.ChildNodes.Where(t => t.Name == "span").First().InnerText;

                movie = new Movie { Id = counter, Title = name, Year = int.Parse(year.Substring(1, year.LastIndexOf(")") - 1)) };
                moviesToReturn.Add(movie);
            }

            return moviesToReturn;
        }

    }
}
