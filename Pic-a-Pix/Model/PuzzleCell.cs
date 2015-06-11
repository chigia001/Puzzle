using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pic_a_Pix.Model
{
    public class PuzzleCell
    {

        public PuzzleCell(int rowIndex, int columnIndex, IList<Color> possibleColor,Line row,Line column)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
            this.PossibleColor = new List<Color>();
            foreach(var color in possibleColor)
            {
                this.PossibleColor.Add(color);
            }
            this.Row = row;
            this.Column = column;
        }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public IList<Color> PossibleColor { get; set; }
        public Line Row { get; set; }
        public Line Column { get; set; }
    }
}
