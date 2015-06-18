using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Pic_a_Pix.Model;

namespace Pic_a_Pix.Excel
{
    public class ExcelObject
    {
        public IWorkbook workbook;
        public ISheet sheet;
        public Puzzle puzzle;

        public Dictionary<Color, ICellStyle> StyleDictionary { get; set; }
        public ExcelObject(Puzzle puzzle)
        {
            workbook = new XSSFWorkbook();
            this.puzzle = puzzle;
        }

        public void AddSheet(int step)
        {
            var sheetName = string.Format("step-{0}", step);
            workbook.CreateSheet(sheetName);
            sheet = workbook.GetSheet(sheetName);

            workbook.SetPrintArea(0, 0, puzzle.ColumnSize - 1, 0, puzzle.RowSize - 1);
            CreateStyle(puzzle);

            for (int columnIndex = 0; columnIndex < puzzle.ColumnSize; columnIndex++)
                sheet.SetColumnWidth(columnIndex, 741);

            for (int rowIndex = 0; rowIndex < puzzle.RowSize; rowIndex++)
            {
                var row = sheet.CreateRow(rowIndex);
                var puzzleRow = puzzle.Rows[rowIndex];
                for (int columnIndex = 0; columnIndex < puzzle.ColumnSize; columnIndex++)
                {
                    var cell = row.CreateCell(columnIndex);

                    if (puzzleRow.Cells[columnIndex].PossibleColor.Count == 1)
                    {
                        var color = puzzleRow.Cells[columnIndex].PossibleColor[0];
                        cell.CellStyle = StyleDictionary[color];
                    }
                    else
                    {
                        cell.SetCellValue(string.Join(",",puzzleRow.Cells[columnIndex].PossibleColor.Select(x => x.ColorName).ToArray()));
                    }
                }
            }
        }

        public void WriteToExcelFile(string outputPath)
        {
            FileStream wFile = new FileStream(outputPath, FileMode.Create);
            workbook.Write(wFile);
            wFile.Close();
        }

        public void CreateStyle(Puzzle puzzle)
        {
            StyleDictionary = new Dictionary<Color, ICellStyle>();
            foreach(var color in puzzle.PossibleColor)
            {
                XSSFCellStyle style = (XSSFCellStyle )workbook.CreateCellStyle();
                byte[] rgb = StringToByteArray(color.ColorCode);
                style.SetFillForegroundColor(new XSSFColor(rgb));
                style.FillPattern = FillPattern.SolidForeground;
                StyleDictionary.Add(color, style);
            }
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
