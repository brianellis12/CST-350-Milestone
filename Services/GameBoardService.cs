using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Milestone.Models;
using Activity_2_RegisterAndLoginApp.Models;

namespace Activity_2_RegisterAndLoginApp.Services
{
    public class GameBoardService
    {
        Board gameBoard;
        public GameBoardService(Board board)
        {
            gameBoard = board;
        }

        public void setupBombs()
        {
            // Random number generator for calculating bomb placement
            Random random = new Random();

            // Loop through entire gameBoard.Grid
            for (int i = 0; i < gameBoard.Size; i++)
            {
                for (int j = 0; j < gameBoard.Size; j++)
                {
                    // choose a random number between 0.00 and 1.00.  If random  < difficulty then place a bomb on this square.
                    gameBoard.Grid[i, j].IsLive = random.NextDouble() <= gameBoard.Difficulty;
                }
            }

        }

        // Calculate how many neighbors are live / bombs
        public void CalcLiveNeighbors()
        {
            for (int r = 0; r < gameBoard.Size; r++)
            {
                for (int c = 0; c < gameBoard.Size; c++)
                {


                    // NW of current square
                    if (r - 1 >= 0 && c - 1 >= 0 && gameBoard.Grid[r - 1, c - 1].IsLive) gameBoard.Grid[r, c].LiveNeighbors++;

                    // N
                    if (r - 1 >= 0 && gameBoard.Grid[r - 1, c].IsLive) gameBoard.Grid[r, c].LiveNeighbors++;

                    // NE
                    if (r - 1 >= 0 && c + 1 < gameBoard.Size && gameBoard.Grid[r - 1, c + 1].IsLive) gameBoard.Grid[r, c].LiveNeighbors++;

                    // W
                    if (c - 1 >= 0 && gameBoard.Grid[r, c - 1].IsLive) gameBoard.Grid[r, c].LiveNeighbors++;

                    // E
                    if (c + 1 < gameBoard.Size && gameBoard.Grid[r, c + 1].IsLive) gameBoard.Grid[r, c].LiveNeighbors++;

                    // SW
                    if (r + 1 < gameBoard.Size && c - 1 >= 0 && gameBoard.Grid[r + 1, c - 1].IsLive) gameBoard.Grid[r, c].LiveNeighbors++;

                    // S
                    if (r + 1 < gameBoard.Size && gameBoard.Grid[r + 1, c].IsLive) gameBoard.Grid[r, c].LiveNeighbors++;

                    // SE
                    if (r + 1 < gameBoard.Size && c + 1 < gameBoard.Size && gameBoard.Grid[r + 1, c + 1].IsLive) gameBoard.Grid[r, c].LiveNeighbors++;

                }
            }
        }

        internal bool checkForWin()
        {
            // assume victory until proven otherwise.
            bool won = true;

            // double for loop to check every cell status.
            for (int r = 0; r < gameBoard.Size; r++)
            {
                for (int c = 0; c < gameBoard.Size; c++)
                {
                    if (gameBoard.Grid[r, c].IsVisited == false && gameBoard.Grid[r, c].IsLive == false)
                    {
                        // if a cell is not visited and does not have a bomb, then the game is not over. must continue playing.
                        won = false;
                        // break because there is no need to continue checking other cells on the board.
                        break;
                    }
                    if (gameBoard.Grid[r, c].IsLive == true && gameBoard.Grid[r, c].IsFlagged == false)
                    {
                        // if a cell has a bomb and does not have a flag, then the game is not over. must continue.
                        won = false;
                        break;
                    }
                }
                // break because there is no need to continue checking other cells on the board.
                if (!won) break;
            }
            return won;
        }

        internal bool checkForLose()
        {
            bool lost = false;
            for (int r = 0; r < gameBoard.Size; r++)
            {
                for (int c = 0; c < gameBoard.Size; c++)
                {
                    if (gameBoard.Grid[r, c].IsLive && gameBoard.Grid[r, c].IsVisited)
                    {
                        lost = true;
                    }
                }
            }

            return lost;
        }

        internal void rightClick(int rowGuess, int colGuess)
        {
            gameBoard.Grid[rowGuess, colGuess].IsFlagged = !(gameBoard.Grid[rowGuess, colGuess].IsFlagged);
        }

        internal void leftClick(int rowGuess, int colGuess)
        {
            if (!gameBoard.Grid[rowGuess, colGuess].IsFlagged)
            {
                FloodFill(rowGuess, colGuess);
                gameBoard.clicks++;
            }

        }

        public void FloodFill(int r, int c)
        // use recursion to clear adjacent cells that are empty.
        {
            //set current cell visited to true
            gameBoard.Grid[r, c].IsVisited = true;

            // if current cell has a live neighbor, then stop.
            if (gameBoard.Grid[r, c].LiveNeighbors > 0) return;

            // N.  Call flood fill on the cell north of here if it has not yet been visited.
            if (r - 1 >= 0 && gameBoard.Grid[r - 1, c].IsVisited == false)
                FloodFill(r - 1, c);

            // S
            if (r + 1 < gameBoard.Size && gameBoard.Grid[r + 1, c].IsVisited == false)
                FloodFill(r + 1, c);

            // W
            if (c - 1 >= 0 && gameBoard.Grid[r, c - 1].IsVisited == false)
                FloodFill(r, c - 1);

            // E
            if (c + 1 < gameBoard.Size && gameBoard.Grid[r, c + 1].IsVisited == false)
                FloodFill(r, c + 1);

            // NW
            if (r - 1 >= 0 && c - 1 >= 0 && gameBoard.Grid[r - 1, c - 1].IsVisited == false)
                FloodFill(r - 1, c - 1);

            // NE
            if (r - 1 >= 0 && c + 1 < gameBoard.Size && gameBoard.Grid[r - 1, c + 1].IsVisited == false)
                FloodFill(r - 1, c + 1);

            // SW
            if (r + 1 < gameBoard.Size && c - 1 >= 0 && gameBoard.Grid[r + 1, c - 1].IsVisited == false)
                FloodFill(r + 1, c - 1);

            // SE
            if (r + 1 < gameBoard.Size && c + 1 < gameBoard.Size && gameBoard.Grid[r + 1, c + 1].IsVisited == false && gameBoard.Grid[r, c].LiveNeighbors == 0)
                FloodFill(r + 1, c + 1);

        }
	}
}
