using Activity_2_RegisterAndLoginApp.Models;
using Activity_2_RegisterAndLoginApp.Services;
using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System.Security.Cryptography.X509Certificates;

namespace Milestone.Controllers
{
    public class GameController : Controller
    {
        public static Board gameboard;
        public static GameBoardService boardService;
		public static SaveGameService saveGameService;
        public IActionResult Index()
        {
            gameboard = new Board(10, .21f);
            boardService = new GameBoardService(gameboard);
            boardService.setupBombs();
            boardService.CalcLiveNeighbors();
			saveGameService = new SaveGameService();
            return View("Index", gameboard);
        }

		public IActionResult saveGame()
		{
			string username = HttpContext.Session.GetString("username") ?? ""; 			
			saveGameService.saveGame(username, gameboard);

			return PartialView("_SaveGamePartial");
		}

		public IActionResult viewGames()
		{
			string username = HttpContext.Session.GetString("username") ?? "";
			List<GameModel> games = saveGameService.getUserGames(username);
			
			// Testing data to be removed


			return PartialView("ViewGames", games);
		}

		public IActionResult loadGame(int id)
		{
			GameModel game = saveGameService.getGameById(id);

			gameboard = saveGameService.deserialize(game.gameData);

			return PartialView("_GridCellPartial", gameboard);
		}

		public IActionResult deleteGame(int id)
		{
			string username = HttpContext.Session.GetString("username") ?? "";
			saveGameService.deleteGame(id);

			return PartialView("_GridCellPartial", gameboard);
		}

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

		public IActionResult rightClick(int col, int row)
		{
			boardService.rightClick(col, row);

			return PartialView("_GridCellPartial", gameboard);
		}
	}
}
