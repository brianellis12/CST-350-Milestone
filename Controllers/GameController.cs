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
        public IActionResult Index()
        {
            gameboard = new Board(10, .21f);
            boardService = new GameBoardService(gameboard);
            boardService.setupBombs();
            boardService.CalcLiveNeighbors();
            return View("Index", gameboard);
        }

		public IActionResult serialize()
		{
			boardService.serializeData(gameboard);

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
