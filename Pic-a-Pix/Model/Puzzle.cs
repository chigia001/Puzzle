using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Pic_a_Pix.Model
{
    public class Puzzle
    {
        public IList<Line> Rows { get; set; }
        public IList<Line> Columns { get; set; }
        public int RowSize { get; set; }
        public int ColumnSize { get; set; }

        public IList<Color> PossibleColor { get; set; }
        public Puzzle(string hintPath)
        {
            var inputLines = File.ReadAllLines(hintPath);
            string readStage = string.Empty;

            Rows = new List<Line>();
            Columns = new List<Line>();
            PossibleColor = new List<Color>();

            PossibleColor.Add(ColorDictionary.current.Blank);

            foreach (var inputLine in inputLines)
            {
                var r = new Regex(@"^\[.*\]$");

                if (r.Match(inputLine).Success)
                {
                    readStage = inputLine;
                    continue;
                }
                switch (readStage)
                {
                    case "[Size]":
                        var dimensions = inputLine.Split(' ');
                        RowSize = Convert.ToInt32(dimensions[0]);
                        ColumnSize = Convert.ToInt32(dimensions[1]);
                        break;
                    case "[Row]":
                        Rows.Add(new Line(inputLine, LineType.Row, Rows, PossibleColor,ColumnSize));
                        break;
                    case "[Column]":
                        Columns.Add(new Line(inputLine, LineType.Column, Columns, PossibleColor,RowSize));
                        break;
                    default:
                        throw new Exception("Un-regconized section");
                }
            }

            for (int rowIndex = 0; rowIndex < RowSize; rowIndex++)
            {
                var row = Rows[rowIndex];
                for (int columnIndex = 0; columnIndex < ColumnSize; columnIndex++)
                {
                    var column = Columns[columnIndex];
                    var cell = new PuzzleCell(rowIndex, columnIndex, PossibleColor, row, column);
                    row.Cells.Add(cell);
                    column.Cells.Add(cell);
                }
            }
        }
    }
}
