﻿using Activity_2_RegisterAndLoginApp.Services;
using Microsoft.AspNetCore.Mvc;
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

		public IActionResult leftClick(int col, int row)
		{


			boardService.leftClick(col, row);
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

				var cell = gameboard.Grid[col, row];
				return PartialView("_GridCellPartial", gameboard);
			}
		}	

		public IActionResult rightClick(int col, int row)
		{
			boardService.rightClick(col, row);


			var cell = gameboard.Grid[col, row];
			return PartialView("_GridCellPartial", gameboard);
		}
	}
}
