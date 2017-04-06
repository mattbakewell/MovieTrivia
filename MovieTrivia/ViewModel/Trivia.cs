using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTrivia.Model;


namespace MovieTrivia.ViewModel
{
    /// This Game object is what the consumer of the api sees.
    /// We don't want them to see the Model.Game as that shows internal
    /// workings of the Game that shouldn't be visible
    public class Trivia
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public IList<Round> Rounds { get; set; }
    }
}
