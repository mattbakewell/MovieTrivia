using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTrivia.Model
{
    public class Trivia
    {
        public int Id { get; set; }
        public IList<Round> Rounds { get; set; }

        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
    }
}
