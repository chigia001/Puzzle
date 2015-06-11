using Pic_a_Pix.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pic_a_Pix.Bussiness
{
    public class PicAPixSolver:ISolver
    {
        public void Solve(Puzzle puzzle)
        {
            for (int i = 0; i < 10; i++)
            {
                foreach (var line in puzzle.Rows)
                {
                    Solve(line);
                }
                foreach (var line in puzzle.Columns)
                {
                    Solve(line);
                } 
            }
        }

        private void Solve(Line line)
        {
            var hintIndex = 0;
            var currentHint = line.Hints[hintIndex];
            PuzzleHint previousHint;
            var forward = new Dictionary<PuzzleHint, int>();
            var backward = new Dictionary<PuzzleHint, int>();
            var blockWidth = 0;
            for(int i=0;i<line.Cells.Count;i++)
            {
                var cell = line.Cells[i];
                if (cell.PossibleColor.Contains(currentHint.HintColor))
                    blockWidth++;
                else 
                    blockWidth=0;
                if (blockWidth == currentHint.HintLength)
                {
                    forward.Add(currentHint, i);
                    blockWidth = 0;
                    
                    hintIndex++;
                    previousHint = currentHint;
                    if (hintIndex < line.Hints.Count)
                        currentHint = line.Hints[hintIndex];
                    else break;
                    if (previousHint.HintColor==currentHint.HintColor)
                        i++;
                }
            }
            blockWidth = 0;
            hintIndex = line.Hints.Count - 1;
            currentHint = line.Hints[hintIndex];
            previousHint = null;
            for (int j = line.Cells.Count-1; j >=0; j--)
            {
                var cell = line.Cells[j];
                if (cell.PossibleColor.Contains(currentHint.HintColor))
                    blockWidth++;
                else
                    blockWidth = 0;
                if (blockWidth == currentHint.HintLength)
                {
                    backward.Add(currentHint, j);
                    blockWidth = 0;
                    hintIndex--;

                    previousHint = currentHint;
                    if (hintIndex >= 0)
                        currentHint = line.Hints[hintIndex];
                    else break;

                    if (previousHint.HintColor == currentHint.HintColor)
                        j--;
                }
            }
            foreach( var hint in line.Hints)
            {
                int start = 0;
                int end = 0;
                if (backward.TryGetValue(hint, out start) && forward.TryGetValue(hint, out end) && start <= end)
                {
                    for (int i = start; i <= end; i++)
                    {
                        line.Cells[i].PossibleColor.Clear();
                        line.Cells[i].PossibleColor.Add(hint.HintColor);
                    }
                }
            }
        }
    }
}
