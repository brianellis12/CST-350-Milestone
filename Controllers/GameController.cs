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
			//string username = HttpContext.Session.GetString("username") ?? "";
			//List<GameModel> games = saveGameService.getUserGames(username);
			
			// Testing data to be removed
			List<GameModel> games = new List<GameModel>();

			games.Add(new GameModel(1, 10, new DateTime(2023, 1, 15), "Some game data for user 10"));
			games.Add(new GameModel(2, 11, new DateTime(2023, 1, 16), "Some game data for user 11"));
			games.Add(new GameModel(3, 12, new DateTime(2023, 1, 17), "Some game data for user 12"));
			games.Add(new GameModel(4, 13, new DateTime(2023, 1, 18), "Some game data for user 13"));
			games.Add(new GameModel(5, 14, new DateTime(2023, 1, 19), "Some game data for user 14"));


			return View("ViewGames", games);
		}

		public IActionResult loadGame(int id)
		{
			GameModel game = saveGameService.getGameById(id);

			Board selectedBoard = saveGameService.deserialize(game.gameData);

			return PartialView("Index", selectedBoard);
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
