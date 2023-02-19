using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone.Models
{
    public class Board
    {
        // The cell class represents one square onthe board. Grid is a 2d array of cells
        public Cell[,] Grid { get; set; }

        // the board is square. Size is the both the length and width of the board
        public int Size { get; set; }

        // difficulty is a percent value.  0.05 difficulty means that 5% of the squares will contain a bomb.
        public float Difficulty { get; set; }

        // total clicks in the game
        public int clicks;

        // starting time for the game. Used to determine score
        public DateTime startTime;

        public DateTime endTime;

        // score - calcualted at the end of the game
        public int score;

        // Create a board game with size and difficulty
        public Board(int size, float difficulty)
        {
            // Initialize Fields
            Size = size;
            Difficulty = difficulty;

            startTime = DateTime.Now;

            // Fill the Grid with new cells.
            Grid = new Cell[Size, Size];
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Grid[row, col] = new Cell(row, col);
                }
            }

        }

        public Board(int size)
        {
            // what if the user only provides a size?
            this.Size = size;
            // default difficulty is set to 0.1
            this.Difficulty = 0.1f;
        }

        public Board(float diff)
        {
            // what if the user only provides a difficulty parameter?
            this.Size = 10;
            this.Difficulty = diff;
        }

        public Board()
        {
            // default constructor
            this.Size = 10;
            this.Difficulty = 0.11f;
        }

        // Place bombs on some squares
        public void setupBombs()
        {
            // Random number generator for calculating bomb placement
            Random random = new Random();

            // Loop through entire grid
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // choose a random number between 0.00 and 1.00.  If random  < difficulty then place a bomb on this square.
                    Grid[i, j].IsLive = random.NextDouble() <= Difficulty; 
                }
            }

        }

        // Calculate how many neighbors are live / bombs
        public void CalcLiveNeighbors()
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {


                    // NW of current square
                    if (r - 1 >= 0 && c - 1 >= 0 && Grid[r - 1, c - 1].IsLive) Grid[r, c].LiveNeighbors++;

                    // N
                    if (r - 1 >= 0 && Grid[r - 1, c].IsLive) Grid[r, c].LiveNeighbors++;

                    // NE
                    if (r - 1 >= 0 && c + 1 < Size && Grid[r - 1, c + 1].IsLive) Grid[r, c].LiveNeighbors++;

                    // W
                    if (c - 1 >= 0 && Grid[r, c - 1].IsLive) Grid[r, c].LiveNeighbors++;

                    // E
                    if (c + 1 < Size && Grid[r, c + 1].IsLive) Grid[r, c].LiveNeighbors++;

                    // SW
                    if (r + 1 < Size && c - 1 >= 0 && Grid[r + 1, c - 1].IsLive) Grid[r, c].LiveNeighbors++;

                    // S
                    if (r + 1 < Size && Grid[r + 1, c].IsLive) Grid[r, c].LiveNeighbors++;

                    // SE
                    if (r + 1 < Size && c + 1 < Size && Grid[r + 1, c + 1].IsLive) Grid[r, c].LiveNeighbors++;

                }
            }
        }

        internal bool checkForWin()
        {
            // assume victory until proven otherwise.
            bool won = true;

            // double for loop to check every cell status.
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    if (Grid[r,c].IsVisited == false && Grid[r,c].IsLive == false)
                    {
                        // if a cell is not visited and does not have a bomb, then the game is not over. must continue playing.
                        won = false;
                        // break because there is no need to continue checking other cells on the board.
                        break;
                    }
                    if (Grid[r,c].IsLive == true && Grid[r,c].IsFlagged == false)
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
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    if (Grid[r,c].IsLive && Grid[r,c].IsVisited)
                    {
                        lost = true; 
                    }
                }
            }

            return lost;
        }

        internal void rightClick(int rowGuess, int colGuess)
        {
            Grid[rowGuess, colGuess].IsFlagged = !(Grid[rowGuess, colGuess].IsFlagged);
              }

        internal void leftClick(int rowGuess, int colGuess)
        {
            if (!Grid[rowGuess,colGuess].IsFlagged)
            {
                FloodFill(rowGuess, colGuess);
                clicks++;
            }
         
        }

        public void FloodFill(int r, int c)
        // use recursion to clear adjacent cells that are empty.
        {
            //set current cell visited to true
            Grid[r, c].IsVisited = true;

            // if current cell has a live neighbor, then stop.
            if (Grid[r, c].LiveNeighbors > 0) return;
            
            // N.  Call flood fill on the cell north of here if it has not yet been visited.
              if (r-1 >=0  && Grid[r-1, c].IsVisited == false)
                        FloodFill(r-1, c);

            // S
            if (r + 1 < Size && Grid[r + 1, c].IsVisited == false)
                FloodFill(r + 1, c);

            // W
            if (c - 1 >= 0 && Grid[r, c-1].IsVisited == false)
                FloodFill(r, c - 1);

            // E
            if (c + 1 < Size && Grid[r, c + 1].IsVisited == false )
                FloodFill(r, c + 1);

            // NW
            if (r - 1 >= 0 && c - 1  >= 0 && Grid[r - 1, c - 1].IsVisited == false )
                FloodFill(r - 1, c - 1);

            // NE
            if (r - 1 >= 0 && c + 1 < Size && Grid[r - 1, c + 1].IsVisited == false )
                FloodFill(r - 1, c + 1);

            // SW
            if (r + 1 < Size && c - 1 >= 0 && Grid[r + 1, c - 1].IsVisited == false )
                FloodFill(r + 1, c - 1);

            // SE
            if (r + 1 < Size && c + 1 < Size && Grid[r + 1, c + 1].IsVisited == false && Grid[r, c].LiveNeighbors == 0)
                FloodFill(r + 1, c + 1);

        }
    }
}
