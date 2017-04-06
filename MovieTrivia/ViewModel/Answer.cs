using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTrivia.ViewModel
{
    public class Answer
    {
        public int TriviaId { get; set; }
        public int RoundId { get; set; }
        public int PlayerId { get; set; }
        public int Year { get; set; }
    }
}
