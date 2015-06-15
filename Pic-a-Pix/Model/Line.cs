using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.Formula.Functions;

namespace Pic_a_Pix.Model
{
    public class Line
    {

        public Line(string inputLine, LineType type, IList<Line> lines,IList<Color> PossibleColors,int length)
        {
            var hints = inputLine.Split(' ');

            this.Cells = new List<PuzzleCell>();
            this.Hints = new List<PuzzleHint>();

            foreach(var hint in hints)
            {
                this.Hints.Add(new PuzzleHint(hint, this.Hints, PossibleColors, length));
            }
            this.LineOrdinal = lines.Count;
            this.Type = type;
            this.Length = length;
        }
        public IList<PuzzleHint> Hints { private set; get; }
        public IList<PuzzleCell> Cells { set; get; }
        public int Length { get; set; }
        public LineType Type { get; set; }
        public int LineOrdinal { get; set; }

        public IEnumerable<PuzzleHint> ForwardPuzzleHints()
        {
            return Hints;
        }

        public IEnumerable<PuzzleHint> BackWardPuzzleHints()
        {
            for (var i = Hints.Count-1; i >=0 ; i--)
            {
                yield return Hints[i];
            }
        }
        public IEnumerable<PuzzleCell> ForwardPuzzleCells()
        {
            return Cells;
        }

        public IEnumerable<PuzzleCell> BackWardPuzzleCells()
        {
            for (var i = Cells.Count - 1; i >= 0; i--)
            {
                yield return Cells[i];
            }
        }
    }
}
