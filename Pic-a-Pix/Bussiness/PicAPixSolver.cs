using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pic_a_Pix.Model;

namespace Pic_a_Pix.Bussiness
{
    public class PicAPixSolver : ISolver
    {
        public void Solve(Puzzle puzzle,int i)
        {
            switch (i%6)
            {
                case 0:
                    foreach (var line in puzzle.Rows)
                    {
                        SolveMethod1(line);
                    }
                    break;
                case 1:
                    foreach (var line in puzzle.Columns)
                    {
                        SolveMethod1(line);
                    }
                    break;
                case 2:
                    foreach (var line in puzzle.Rows)
                    {
                        SolveMethod2(line);
                    }
                    break;
                case 3:
                    foreach (var line in puzzle.Columns)
                    {
                        SolveMethod2(line);
                    }
                    break;
                case 4:
                    foreach (var line in puzzle.Rows)
                    {
                        SolveMethod3(line);
                    }
                    break;
                case 5:
                    foreach (var line in puzzle.Columns)
                    {
                        SolveMethod3(line);
                    }
                    break;
            }
        }

        private void Solve(Line line)
        {
            SolveMethod1(line);
        }

        private void SolveMethod1(Line line)
        {
            var hintIndex = 0;
            var currentHint = line.Hints[hintIndex];
            PuzzleHint previousHint;
            var forward = new Dictionary<PuzzleHint, int>();
            var backward = new Dictionary<PuzzleHint, int>();
            var blockWidth = 0;
            var start = 0;
            var end = 0;
            for (int i = 0; i < line.Cells.Count; i++)
            {
                if (currentHint.IsCompleted)
                {
                    blockWidth = 0;
                    i = currentHint.EndIndex + 1;
                    hintIndex++;
                    previousHint = currentHint;
                    if (hintIndex < line.Hints.Count)
                        currentHint = line.Hints[hintIndex];
                    else break;
                    if (previousHint.HintColor == currentHint.HintColor)
                        i++;
                    
                }
                if (blockWidth == 0)
                    start = i;
                var cell = line.Cells[i];
                if (cell.PossibleColor.Contains(currentHint.HintColor))
                    blockWidth++;
                else
                    blockWidth = 0;
                if (blockWidth == currentHint.HintLength)
                {
                    forward.Add(currentHint, i);
                    currentHint.StartIndex = start;
                    blockWidth = 0;

                    hintIndex++;
                    previousHint = currentHint;
                    if (hintIndex < line.Hints.Count)
                        currentHint = line.Hints[hintIndex];
                    else break;
                    if (previousHint.HintColor == currentHint.HintColor)
                        i++;
                }
            }
            blockWidth = 0;
            hintIndex = line.Hints.Count - 1;
            currentHint = line.Hints[hintIndex];
            previousHint = null;
            for (int j = line.Cells.Count - 1; j >= 0; j--)
            {
                if (currentHint.IsCompleted)
                {
                    j = currentHint.StartIndex - 1;
                    blockWidth = 0;
                    hintIndex--;

                    previousHint = currentHint;
                    if (hintIndex >= 0)
                        currentHint = line.Hints[hintIndex];
                    else break;
                    if (previousHint.HintColor == currentHint.HintColor)
                        j--;

                }
                if (blockWidth == 0)
                    end = j;
                var cell = line.Cells[j];
                if (cell.PossibleColor.Contains(currentHint.HintColor))
                    blockWidth++;
                else
                    blockWidth = 0;
                if (blockWidth == currentHint.HintLength)
                {
                    backward.Add(currentHint, j);
                    currentHint.EndIndex = end;
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
            foreach (var hint in line.Hints)
            {
                if (backward.TryGetValue(hint, out start) && forward.TryGetValue(hint, out end) && start <= end)
                {
                    MarkZone(start, end, line, hint);
                }
            }
        }

        private void SolveMethod2(Line line)
        {
            #region firstHint
            var firstHint = line.Hints.FirstOrDefault(x => !x.IsCompleted);
            if (firstHint == null)
                return;
            var i = firstHint.StartIndex;
            bool IsCompleted = false;

            for (int o = 0; o < firstHint.StartIndex; o++)
            {
                if (line.Cells[o].PossibleColor.Count > 1)
                {
                    line.Cells[o].PossibleColor.Clear();
                    line.Cells[o].PossibleColor.Add(ColorDictionary.current.Blank);
                }
            }
            while (i <= firstHint.EndIndex - firstHint.HintLength + 1 && !IsCompleted)
            {
                var cellI = line.Cells[i];
                if (cellI.PossibleColor.Contains(firstHint.HintColor))
                {
                    var j = i;
                    while (j < i + firstHint.HintLength+1 && !IsCompleted)
                    {
                        var cellJ = line.Cells[j];
                        if (!cellJ.PossibleColor.Contains(firstHint.HintColor))
                        {
                            if (j == i + firstHint.HintLength && cellJ.PossibleColor.Count==1)
                            {

                                MarkZone(i, j-1, line, firstHint);
                                IsCompleted = true;
                            }
                            else
                            {
                                for (var t = i; t < j; t++)
                                {
                                    line.Cells[t].PossibleColor.Remove(firstHint.HintColor);
                                }
                                i = j + 1;
                                firstHint.StartIndex = i;
                            }
                            break;
                        }
                        if (line.Cells[j].PossibleColor.Count != 1)
                        {
                            j++;
                        }
                        else
                        {
                            var k = j;
                            var l = k;
                            while (l + 1 < j + firstHint.HintLength && l + 1 <= firstHint.EndIndex)
                            {
                                var nextCellL = line.Cells[l + 1];
                                if (!nextCellL.PossibleColor.Contains(firstHint.HintColor))
                                {
                                    break;
                                }
                                if (nextCellL.PossibleColor.Count == 1 && k == l)
                                    k++;
                                l++;
                            }
                            for (int o = i; o < k - firstHint.HintLength + 1; o++)
                                line.Cells[o].PossibleColor.Remove(firstHint.HintColor);
                            var start = l - firstHint.HintLength + 1;
                            var end = i + firstHint.HintLength - 1;
                            MarkZone(start, end, line, firstHint);
                            IsCompleted = true;

                        }
                    }
                    if (j >= i + firstHint.HintLength + 1) break;
                }
                else i++;
            }
            #endregion
            IsCompleted = false;
            var lastHint = line.Hints.LastOrDefault(x => !x.IsCompleted);
            if (lastHint == null)
                return;

            for (int o = line.Cells.Count-1; o > lastHint.EndIndex; o--)
            {
                if (line.Cells[o].PossibleColor.Count > 1)
                {
                    line.Cells[o].PossibleColor.Clear();
                    line.Cells[o].PossibleColor.Add(ColorDictionary.current.Blank);
                }
            }
            i = lastHint.EndIndex;
            while (i >= lastHint.StartIndex + lastHint.HintLength - 1 && !IsCompleted)
            {
                var cellI = line.Cells[i];
                if (cellI.PossibleColor.Contains(lastHint.HintColor))
                {
                    var j = i;
                    while (j > i - lastHint.HintLength -1 && !IsCompleted)
                    {
                        var cellJ = line.Cells[j];
                        if (!cellJ.PossibleColor.Contains(lastHint.HintColor))
                        {
                            if (j == i - lastHint.HintLength && cellJ.PossibleColor.Count == 1)
                            {

                                MarkZone(i, j - 1, line, firstHint);
                                IsCompleted = true;
                            }
                            else
                            {
                                for (var t = i; t > j; t--)
                                {
                                    line.Cells[t].PossibleColor.Remove(lastHint.HintColor);
                                }
                                i = j - 1;
                                lastHint.EndIndex = i;
                            }
                            break;
                        }
                        if (line.Cells[j].PossibleColor.Count != 1)
                        {
                            j--;
                        }
                        else
                        {
                            var k = j;
                            var l = k;
                            while (l - 1 > j - lastHint.HintLength && l - 1 >= lastHint.StartIndex)
                            {
                                var nextCellL = line.Cells[l - 1];
                                if (!nextCellL.PossibleColor.Contains(lastHint.HintColor))
                                {
                                    break;
                                }
                                if (nextCellL.PossibleColor.Count == 1 && k == l)
                                    k--;
                                l--;
                            }
                            for (int o = i; o > k + lastHint.HintLength - 1; o--)
                                line.Cells[o].PossibleColor.Remove(lastHint.HintColor);
                            var end = l + lastHint.HintLength - 1;
                            var start = i - lastHint.HintLength + 1;
                            MarkZone(start, end, line, lastHint);
                            IsCompleted = true;

                        }
                    }
                    if (j <= i - lastHint.HintLength-1) break;
                }
                else i--;
            }
        }

        private void SolveMethod3(Line line)
        {
            if (line.Hints.All(x => x.IsCompleted))
            {
                foreach (var cell in
                line.Cells.Where(x => x.PossibleColor.Count > 1))
                {
                    cell.PossibleColor.Clear();
                    cell.PossibleColor.Add(ColorDictionary.current.Blank);
                }
            }
        }

        private void MarkZone(int start, int end, Line line, PuzzleHint hint)
        {
            for (int i = start; i <= end; i++)
            {
                if (line.Cells[i].PossibleColor.Contains(hint.HintColor))
                {
                    line.Cells[i].PossibleColor.Clear();
                    line.Cells[i].PossibleColor.Add(hint.HintColor);
                    hint.CellBelongToHint.Add(line.Cells[i]);
                }
                else
                {
                    throw  new Exception("invalid set color");
                }
            }
            if (end - start + 1 == hint.HintLength)
            {
                hint.IsCompleted = true;
                if (start > 0)
                    line.Cells[start - 1].PossibleColor.Remove(hint.HintColor);
                if (end + 1 < line.Length)
                    line.Cells[end + 1].PossibleColor.Remove(hint.HintColor);
                hint.StartIndex = start;
                hint.EndIndex = end;
            }
        }
    }
}
