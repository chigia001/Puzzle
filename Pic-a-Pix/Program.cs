﻿using System.Text;
using CommandLine;
using Pic_a_Pix.Bussiness;
using Pic_a_Pix.Excel;
using Pic_a_Pix.Model;

namespace Pic_a_Pix
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandLineInput = new CommandLineInput();
            var parser = new Parser();
            if (parser.ParseArguments(args, commandLineInput))
            {
                var puzzle = new Puzzle(commandLineInput.HintFile);
                var solver = new PicAPixSolver();
                var excelObject = new ExcelObject(puzzle);
                for (int i = 0; i < commandLineInput.Loop; i++)
                {
                    solver.Solve(puzzle,i);
                    excelObject.AddSheet(i);
                }
                excelObject.WriteToExcelFile(commandLineInput.OutputFile);

            }
        }
        public class CommandLineInput
        {
            [Option('o', null, Required = true, HelpText = "Output file to create.")]
            public string OutputFile { get; set; }
            [Option('h', null, Required = true, HelpText = "Hint file to Read.")]
            public string HintFile { get; set; }

            [Option('l', null, Required = true, HelpText = "Hint file to Read.")]
            public int Loop { get; set; }


            [HelpOption]
            public string GetUsage()
            {
                // this without using CommandLine.Text
                var usage = new StringBuilder();
                usage.AppendLine("Quickstart Pic-a-Pix Solver 1.0");
                usage.AppendLine("Read user manual for usage instructions...");
                return usage.ToString();
            }
        }
    }
}
