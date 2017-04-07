using Microsoft.AspNetCore.Mvc;
using MovieTrivia.Application;
using MovieTrivia.ViewModel;

namespace MovieTrivia.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        private readonly TriviaService _triviaService;

        public AnswerController(TriviaService triviaService)
        {
            _triviaService = triviaService;
        }

        [HttpPost]
        public void Post([FromBody]Answer answer)
        {
            // There is only one POST method for a answer,
            // the logic is taken care of by the TriviaService
            // NOTE: We post in a ViewModel.Answer.  There is no
            // matching Model.Answer, as we map the data to a different place
            // instead:  Round.Player1Answer, or Round.Player2Answer
            _triviaService.MakeAnswer(answer);
        }
    }
}