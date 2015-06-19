using System.Collections.Generic;
using System.Linq;

namespace Pic_a_Pix.Model
{
    public class PuzzleCell
    {

        public PuzzleCell(int rowIndex, int columnIndex,Line row,Line column)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            Row = row;
            Column = column;

            PossibleColor = row.PossibleColors.Where(x => column.PossibleColors.Contains(x)).ToList();
            
        }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public IList<Color> PossibleColor { get; set; }
        public Line Row { get; set; }
        public Line Column { get; set; }
    }
}
