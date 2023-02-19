using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System.Security.Cryptography.X509Certificates;

namespace Activity_2_RegisterAndLoginApp.Controllers
{
    public class GameController : Controller
    {
        public static Board gameboard = new Board(8, .1f);
        public IActionResult Index()
        {
            gameboard.setupBombs();
            return View("Index", gameboard);
        }

        public IActionResult leftClick(int col, int row)
        {
            if (gameboard.Grid[col,row].IsLive == true)
            {
                gameboard.checkForLose();
            } else
            {
                gameboard.Grid[col, row].IsVisited = true;
                gameboard.leftClick(col, row);
            }
            
            return View("Index", gameboard);
        }
    }
}
