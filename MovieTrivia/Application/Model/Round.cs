using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTrivia.Model
{
    /// <summary>
    /// Each Trivia Game has 8 rounds
    /// Each round has The Movie Question, Player scores and the Answer
    /// 
    /// </summary>
    public class Round
    {
        public int Id { get; set; }
        public int Counter { get; set; }
        public Movie Movie { get; set; }
        public int PlayerOneAnswer { get; set; }
        public int PlayerTwoAnswer { get; set; }

        public int Score(PlayerId player)
        {
            var answer = ( player == PlayerId.One) ? PlayerOneAnswer : PlayerTwoAnswer;

            if (answer == 0)
                return 0;

            return Score(answer, Movie.Year);

        }

        private int Score(int answer, int year)
        {
            // 5 for correct answer -3 for wrong answer
            return (answer == year) ? 5 : -3;
        }

        public bool IsFinished
        {
            get
            {
                return (PlayerOneAnswer != 0 && PlayerTwoAnswer != 0);
            }
        }
    }
    public enum PlayerId
    {
        One = 1,
        Two = 2
    }
}
