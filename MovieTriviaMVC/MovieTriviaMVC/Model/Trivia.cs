using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieTriviaMVC
{
    public class Trivia
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public Round Round { get; set; }
        public int PlayerId { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        [Required]
        [Range(1900, 2020, ErrorMessage = "Can only be between 1900 .. 2020")]
        public string Answer { get; set; }

        public bool ShowAnswer
        {
            get
            {
                return (GetPlayerStatus(PlayerId) == "Waiting");
            }
        }

        public string GetPlayerStatus(int playerId)
        {
            return (playerId == 1) ? GetStatusString(Round.PlayerOneAnswer) : GetStatusString(Round.PlayerTwoAnswer);
        }

        private string GetStatusString(int guess)
        {
            return (guess > 0) ? "Guess made" : "Waiting";
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    public class Round
    {
        public int Id { get; set; }
        public int Counter { get; set; }
        public Movie Movie { get; set; }
        public int PlayerOneAnswer { get; set; }
        public int PlayerTwoAnswer { get; set; }
    }

    public class Movie
    {
        public string Title { get; set; }
    }

    public class Answer
    {
        public int TriviaId { get; set; }
        public int RoundId { get; set; }
        public int PlayerId { get; set; }
        public int Year { get; set; }
    }
}


