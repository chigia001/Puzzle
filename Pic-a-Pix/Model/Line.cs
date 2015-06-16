using System.Collections.Generic;

namespace Pic_a_Pix.Model
{
    public class Line
    {

        public Line(string inputLine, LineType type, IList<Line> lines,IList<Color> PossibleColors,int length)
        {
            var hints = inputLine.Split(' ');

            Cells = new List<PuzzleCell>();
            Hints = new List<PuzzleHint>();

            foreach(var hint in hints)
            {
                Hints.Add(new PuzzleHint(hint, Hints, PossibleColors, length));
            }
            LineOrdinal = lines.Count;
            Type = type;
            Length = length;
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
