using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTrivia.Model
{
    /// <summary>
    /// holds the question name of the Move and the Year
    /// is what players are trying to guess
    /// </summary>
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
    }
}
