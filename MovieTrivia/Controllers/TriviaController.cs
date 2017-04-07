using Microsoft.AspNetCore.Mvc;
using MovieTrivia.Application;
using MovieTrivia.ViewModel;

namespace MovieTrivia.Controllers
{
    [Route("api/[controller]")]
    public class TriviaController : Controller
    {
        private readonly TriviaService _triviaService;

        public TriviaController(TriviaService triviaService)
        {
            _triviaService = triviaService;
        }

        [HttpGet]
        public Trivia  Get()
        {
            var trivia = _triviaService.NewTrivia();
            return MapTriviaToViewModel(trivia);
        }

        [HttpGet]
        [Route("{id:int}")]
        public Trivia Get(int id)
        {
            var trivia = _triviaService.GetTrivia(id);
            return MapTriviaToViewModel(trivia);
        }

        // the TriviaService returns a Model.Trivia object.  We want to send out
        // a ViewModel.Trivia, so that we can show only the data we want to allow
        // the consumers to see.  This maps from Model.Trivia to ViewModel.Trivia
        // (We also take in a viewmodel in PUT or POST for similar reasons)
        // TODO: Consider AutoMapper if this gets more unwieldy
        private static Trivia MapTriviaToViewModel(Model.Trivia trivia)
        {
            return new Trivia()
            {
                Id = trivia.Id,
                Status = trivia.Status,
                PlayerOne = new MovieTrivia.ViewModel.Player
                {
                    Name = trivia.PlayerOne.Name,
                    Score = trivia.Score(Model.PlayerId.One)
                },
                PlayerTwo = new MovieTrivia.ViewModel.Player
                {
                    Name = trivia.PlayerTwo.Name,
                    Score = trivia.Score(Model.PlayerId.Two)
                },
                Rounds = trivia.Rounds
            };
        }
    }
}