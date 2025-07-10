using Microsoft.AspNetCore.Mvc;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames =
        [
                new VideoGame
                {
                    Id = 1,
                    Title = "The Legend of Zelda: Breath of the Wild",
                    Platform = "Nintendo Switch",
                    Developer = "Nintendo",
                    Publisher = "Nintendo"
                },

                new VideoGame
                {
                    Id = 2,
                    Title = "God of War Ragnarok",
                    Platform = "PlayStation 5",
                    Developer = "Santa Monica Studio",
                    Publisher = "Sony Interactive Entertainment"
                },

                new VideoGame
                {
                    Id = 3,
                    Title = "Elden Ring",
                    Platform = "PC",
                    Developer = "FromSoftware",
                    Publisher = "Bandai Namco Entertainment"
                }
            ];

        [HttpGet]
        public ActionResult<List<VideoGame>> GetVideoGames()
        {
            return Ok(videoGames);
        }

        [HttpGet("{id}")]
        public ActionResult<VideoGame> GetVideoGameById([FromRoute] int id)
        {
            var videoGame = videoGames.FirstOrDefault(game => game.Id == id);
            if (videoGame is null) return NotFound();
            return Ok(videoGame);
        }

        [HttpPost]
        public ActionResult<VideoGame> AddVideoGame(VideoGame newGame)
        {
            if (newGame is null) return BadRequest();

            newGame.Id = videoGames.Max(game => game.Id) + 1;
            videoGames.Add(newGame);

            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideoGame([FromRoute] int id, [FromBody] VideoGame updatedGame)
        {
            var videoGame = videoGames.FirstOrDefault(game => game.Id == id);
            if (videoGame is null) return NotFound();

            videoGame.Title = updatedGame.Title;
            videoGame.Platform = updatedGame.Platform;
            videoGame.Developer = updatedGame.Developer;
            videoGame.Publisher = updatedGame.Publisher;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideoGame([FromRoute] int id)
        {
            var videoGame = videoGames.FirstOrDefault(game => game.Id == id);
            if (videoGame is null) return NotFound();

            videoGames.Remove(videoGame);

            return NoContent();
        }
    }
}
