using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System.Security.Cryptography.X509Certificates;

namespace Activity_2_RegisterAndLoginApp.Controllers
{
    public class GameController : Controller
    {
        public static Board gameboard;
        public IActionResult Index()
        {
            gameboard = new Board(10, .01f);
            gameboard.setupBombs();
            gameboard.CalcLiveNeighbors();
            return View("Index", gameboard);
        }

        public IActionResult leftClick(int col, int row)
        {
            if (gameboard.checkForLose())
            {
               
               return View("EndGame");
           
            } 
            else if (gameboard.checkForWin())
            {
                return View("Victory");
            }
            else
            {
                gameboard.leftClick(col, row);
            }
            
            return View("Index", gameboard);
        }
    }
}
