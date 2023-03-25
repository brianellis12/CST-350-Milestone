using Activity_2_RegisterAndLoginApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Milestone.Models;
using System.Security.Cryptography.X509Certificates;

namespace Activity_2_RegisterAndLoginApp.Controllers
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

        public IActionResult ShowWinOrLossMessage()
        {
			if (boardService.checkForLose())
			{

				return View("EndGame");

			}
			else if (boardService.checkForWin())
			{
				return View("Victory");
			}
            else
            {
				return View("Index", gameboard);
			}
		}
        public IActionResult leftClick(int col, int row)
        {
           
            
                boardService.leftClick(col, row);
            

            var cell = gameboard.Grid[col, row];
            return PartialView("_GridCellPartial", cell);
		}
    }
}
