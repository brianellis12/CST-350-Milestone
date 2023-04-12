
using Activity_2_RegisterAndLoginApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Milestone.Models;

namespace Milestone.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GameAPIController : ControllerBase
	{
		public GameDAO repository = new GameDAO();

		[HttpGet]
		[ProducesDefaultResponseType(typeof(List<GameModelDTO>))]
		public IEnumerable<GameModelDTO> Index()
		{
			List<GameModel> games = repository.getGames();

			IEnumerable<GameModelDTO> gamesDTO = from g in games select new GameModelDTO(g.id, g.userId, g.date, g.gameData);
			return gamesDTO;
		}

		[HttpGet("showOneGame/{id}")]
		public ActionResult<GameModelDTO> showOneSavedGame(int id)
		{
			GameModel game = repository.getOneGame(id);

			GameModelDTO gameModelDTO= new GameModelDTO(game.id, game.userId, game.date, game.gameData);
			return gameModelDTO;
		}

		[HttpDelete("deleteOneGame/{id}")]
		public ActionResult <Boolean> deleteSavedGame(int id)
		{
			return repository.deleteGame(id);
		}

		[HttpPost("saveGame")] 
		public ActionResult<Boolean> saveGame([FromBody] GameModelDTO gameDTO) { 
			GameModel game = new GameModel(gameDTO.id, gameDTO.userId, gameDTO.date, gameDTO.gameData); 
			return repository.saveGame(game); 
		}
	}
}
