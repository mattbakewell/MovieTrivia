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
        public string Status
        {
            get
            {
                var completedRounds = Rounds.Where(r => r.IsFinished);
                if (completedRounds.Count() == 8)
                    return "Finished";

                return "In Progress";
            }
        }
        public int Score(PlayerId whichPlayer)
        {
            var total = 0;
            foreach (var round in Rounds)
            {
                total += round.Score(whichPlayer);
            }
            return total;
        }
    }
}
