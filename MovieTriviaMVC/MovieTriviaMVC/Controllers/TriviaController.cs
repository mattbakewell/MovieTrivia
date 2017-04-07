using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MovieTriviaMVC.Controllers
{
    public class TriviaController : Controller
    {
        private readonly HttpClient _client;

        public TriviaController()
        {
            _client = new HttpClient();
             _client.BaseAddress = new Uri("http://localhost:5000/api/");
            //_client.BaseAddress = new Uri("http://trivia-backend.azurewebsites.net/api/");
        }

        [HttpGet]
        public IActionResult Index(int playerId, int? id)
        {
            Trivia trivia;
            HttpResponseMessage response;

            if (id.HasValue)
            {
                response = _client.GetAsync($"trivia/{id}").Result;
                trivia = GetTriviaFromResponse(response);
                trivia.PlayerId = playerId;

                return View(trivia);
            }
            else
            {
                response = _client.GetAsync("trivia").Result;
                trivia = GetTriviaFromResponse(response);

                // PRG style to avoid double posting
                return RedirectToAction("Index", new { playerId = playerId, id = trivia.Id });
            }
        }

        [HttpPost]
        public IActionResult Answer(Trivia model)
        {
            // make a Answer
            var answer = new Answer
            {
                TriviaId = model.Id,
                RoundId = model.Round.Id,
                PlayerId = model.PlayerId,
                Year = int.Parse(model.Answer)
            };

            // send it to the api
            var jsonData = JsonConvert.SerializeObject(answer);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var retVal = _client.PostAsync("answer", httpContent).Result;
            // TODO: Check return status for anything other than "OK"

            return RedirectToAction("Index", "Trivia", new { playerId = model.PlayerId, id = model.Id });
        }

        private Trivia GetTriviaFromResponse(HttpResponseMessage response)
        {
            // Converts a response from the API to a Trivia Object ready to send to the View
            var content = response.Content.ReadAsStringAsync().Result;
            var jsonObject = JObject.Parse(content);
            var trivia = jsonObject.ToObject<Trivia>();
            trivia.Round = jsonObject.SelectToken("rounds").Children().Last().ToObject<Round>(); ;
            return trivia;
        }
    }
}