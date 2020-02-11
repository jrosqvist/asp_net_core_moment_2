using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moment2.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Moment2.Controllers
{
    public class HomeController : Controller
    {
        // Startsidan
        public IActionResult Index()
        {
            // Läs in json-filen med filmer
            var JsonStr = System.IO.File.ReadAllText("movies.json");
            // Konvertera och casta till Movie-objekt
            var JsonObj = JsonConvert.DeserializeObject<IEnumerable<Movie>>(JsonStr);

            // Plockar ut antalet filmer
            ViewData["MovieCount"] = JsonObj.Count();

            // Hämtar sessionsvariabeln med senast tillagda filmtitel
            string sess = HttpContext.Session.GetString("movie-title");
            // Sparar i viewbag
            ViewBag.MovieTitle = sess;

            return View();
        }

        // Filmlistnings-sidan, ändring av routing till svenska
        [HttpGet("/filmer")]
        public IActionResult Movies()
        {
            // Hämtar sessionsvariabeln med senast tillagda filmtitel
            string sess = HttpContext.Session.GetString("movie-title");
            // Sparar i viewbag
            ViewBag.MovieTitle = sess;

            // Läs in json-filen med filmer
            var JsonStr = System.IO.File.ReadAllText("movies.json");
            // Konvertera och casta till Movie-objekt
            var JsonObj = JsonConvert.DeserializeObject<IEnumerable<Movie>>(JsonStr);
            // Skickar med film-objekten till vyn
            return View(JsonObj);
        }

        // Lägga till-sidan, ändring av routing till svenska
        [HttpGet("/laggtillfilm")]
        public IActionResult AddMovie()
        {
            return View();
        }

        // Lägg-till-sidan efter POST med formuläret
        [HttpPost("/laggtillfilm")]
        public IActionResult AddMovie(Movie movie)
        {
            // Körs om formuläret är korrekt ifyllt
            if (ModelState.IsValid)
            {
                // Läs in json-filen med filmer
                var JsonStr = System.IO.File.ReadAllText("movies.json");
                // Konvertera och casta till Movie-objekt
                var JsonObj = JsonConvert.DeserializeObject<List<Movie>>(JsonStr);
                // Lägger till i json-filen med filmer
                JsonObj.Add(movie);
                // Skriver till json-filen
                System.IO.File.WriteAllText("movies.json", JsonConvert.SerializeObject(JsonObj, Formatting.Indented));

                // Tömmer inmatningsfälten
                ModelState.Clear();

                //Plockar fram filmens titel
                string movieTitle = movie.Title;
                // Skickar tillbaka titeln för användning i vyn
                ViewBag.MovieTitle = movieTitle;
                //Skapar en sessionsvariabel som innehåller titeln på senaste tillagda filmen
                HttpContext.Session.SetString("movie-title", movieTitle);
            }
            return View();
        }

        // Om-sidan - svensk rout
        [HttpGet("/om")]
        public IActionResult About()
        {
            return View();
        }

    }
}
