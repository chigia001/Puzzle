using System.Collections.Generic;

namespace Pic_a_Pix.Model
{
    public class PuzzleCell
    {

        public PuzzleCell(int rowIndex, int columnIndex, IList<Color> possibleColor,Line row,Line column)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            PossibleColor = new List<Color>();
            foreach(var color in possibleColor)
            {
                PossibleColor.Add(color);
            }
            Row = row;
            Column = column;
        }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public IList<Color> PossibleColor { get; set; }
        public Line Row { get; set; }
        public Line Column { get; set; }
    }
}
