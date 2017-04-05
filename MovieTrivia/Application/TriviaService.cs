using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using MovieTrivia.Model;

namespace MovieTrivia.Application
{
    /// <summary>
    /// This service encapsulates the flow of Game.
    /// This helps folowing the DRY principle keeping the controllers nice and clean
    /// and hiding the working of the private methods
    /// </summary>
    public class TriviaService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TriviaService (ApplicationDbContext applicationContext)
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
        }

    }
}
