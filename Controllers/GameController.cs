using Activity_2_RegisterAndLoginApp.Controllers;
using Activity_2_RegisterAndLoginApp.Models;
using Activity_2_RegisterAndLoginApp.Services;
using Activity_2_RegisterAndLoginApp.Utility;
using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System.Security.Cryptography.X509Certificates;

namespace Milestone.Controllers
{
    public class GameController : Controller
    {
        public ILoggers Logger { get; set; }

        public static Board gameboard;
        public static GameBoardService boardService;
		public static SaveGameService saveGameService;

<<<<<<< Updated upstream
		[HttpGet]
		[CustomAuthorization]
		public IActionResult Index()
=======
        public GameController(ILoggers loggers)
>>>>>>> Stashed changes
        {
            Logger = loggers;
        }

        [HttpGet]
		[CustomAuthorization]
		public IActionResult Index()
        {

			Logger.Info("Game has been created");
            gameboard = new Board(10, .1f);
            boardService = new GameBoardService(gameboard);
            boardService.setupBombs();
            boardService.CalcLiveNeighbors();
			saveGameService = new SaveGameService();
            return View("Index", gameboard);
        }

<<<<<<< Updated upstream
        [HttpGet]
=======
        [HttpPost]
>>>>>>> Stashed changes
        [CustomAuthorization]
        public IActionResult saveGame()
		{
			Logger.Info("Game has been saved");
			string username = HttpContext.Session.GetString("username") ?? ""; 			
			saveGameService.saveGame(username, gameboard);

			return PartialView("_SaveGamePartial");
		}

<<<<<<< Updated upstream
        [HttpGet]
=======
        [HttpPost]
>>>>>>> Stashed changes
        [CustomAuthorization]
        public IActionResult viewGames()
		{
			string username = HttpContext.Session.GetString("username") ?? "";
			List<GameModel> games = saveGameService.getUserGames(username);
			
			// Testing data to be removed


			return PartialView("ViewGames", games);
		}

<<<<<<< Updated upstream
        [HttpGet]
=======
        [HttpPost]
>>>>>>> Stashed changes
        [CustomAuthorization]
        public IActionResult loadGame(int id)
		{
			Logger.Info("Game has been loaded");
			GameModel game = saveGameService.getGameById(id);

			gameboard = saveGameService.deserialize(game.gameData);
			boardService = new GameBoardService(gameboard);
			boardService.CalcLiveNeighbors();
			return PartialView("_GridCellPartial", gameboard);
		}

<<<<<<< Updated upstream
        [HttpGet]
=======
        [HttpPost]
>>>>>>> Stashed changes
        [CustomAuthorization]
        public IActionResult deleteGame(int id)
		{
			string username = HttpContext.Session.GetString("username") ?? "";
			saveGameService.deleteGame(id);

			return PartialView("_GridCellPartial", gameboard);
		}

<<<<<<< Updated upstream
        [HttpGet]
=======
        [HttpPost]
>>>>>>> Stashed changes
        [CustomAuthorization]
        public IActionResult leftClick(int col, int row)
		{
			boardService.leftClick(col, row);
			if (boardService.checkForLose())
			{

				return PartialView("EndGame");

			}
			else if (boardService.checkForWin())
			{
				return PartialView("Victory");
			}
			else
			{
				return PartialView("_GridCellPartial", gameboard);
			}
		}

<<<<<<< Updated upstream
        [HttpGet]
=======
        [HttpPost]
>>>>>>> Stashed changes
        [CustomAuthorization]
        public IActionResult rightClick(int col, int row)
		{
			boardService.rightClick(col, row);

			return PartialView("_GridCellPartial", gameboard);
		}
	}
}
