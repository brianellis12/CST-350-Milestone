using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone.Models
{
    public class Cell
    {
        public int Col { get; set; }
        public int Row { get; set; }
        public int LiveNeighbors { get; set; }
        public bool IsVisited { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsLive { get; set; }

        

        // default constructor.
        // this.property is optional in c#.
        public Cell()
        {
            this.Col = -1;
            this.Row = -1;
            this.LiveNeighbors = 0;
            this.IsVisited = false;
            this.IsFlagged = false;
            this.IsLive = false;
        }

        // constructor that uses row and col parameters
        // this.property is optional in C#.

        public Cell(int r, int c)
        {
            Row = r;
            Col = c;
            LiveNeighbors = 0;
            IsVisited = false;
            IsFlagged = false;
            IsLive = false; 
        }

        public override string Tostring()
        {
            return "R=" + Row + " C=" + Col + " Flag=" + IsFlagged + " Visit=" + IsVisited + " N=" + LiveNeighbors + " L=" + IsLive; 
        }
    }
}
