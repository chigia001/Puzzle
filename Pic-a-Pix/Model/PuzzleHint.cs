﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pic_a_Pix.Model
{
    public class PuzzleHint
    {

        public PuzzleHint(string hint, IList<PuzzleHint> Hints, IList<Color> PossibleColors, IList<Color> LinePossibleColors, int endIndex)
        {
            // TODO: Complete member initialization
            var pattern = @"^(\d+)(\w)?$";
            var match = Regex.Match(hint, pattern);
            if (match.Success)
            {
                HintLength = Convert.ToInt32(match.Groups[1].Value);
                if (match.Groups[2].Index >0)
                {
                    HintColor = ColorDictionary.current.Colors[match.Groups[2].Value];
                }
                else
                    HintColor = ColorDictionary.current.DefaultColor;

                if (!PossibleColors.Contains(HintColor))
                    PossibleColors.Add(HintColor);

                if (!LinePossibleColors.Contains(HintColor))
                    LinePossibleColors.Add(HintColor);

                HintOrdinal = Hints.Count();
                CellBelongToHint = new List<PuzzleCell>();
            }
            StartIndex = 0;
            EndIndex = endIndex-1;
        }
        public int HintLength { private set; get; }
        public int HintOrdinal { private set; get; }
        public Color HintColor { private set; get; }
        public IList<PuzzleCell> CellBelongToHint { get; set; }
        public  bool IsCompleted { get; set; }

        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
       
    }
}
