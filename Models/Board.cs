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

			Grid = new Cell[Size, Size];
			for (int row = 0; row < Size; row++)
			{
				for (int col = 0; col < size; col++)
				{
					Grid[row, col] = new Cell(row, col);
				}
			}
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
    }
}
