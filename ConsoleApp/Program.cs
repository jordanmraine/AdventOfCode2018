using System;

using Kirkin.CommandLine;

using Solutions.DayOne;
using Solutions.DayTwo;
using Solutions.DayThree;

namespace ConsoleApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    args = new[] { "--help" };
                }

                CommandLineParser parser = new CommandLineParser
                {
                    ShowAppDetailsInHelp = true
                };

                parser.DefineCommand("day-one", cmd =>
                {
                    cmd.Help = "Executes day one of the Advent of Code 2018 exercise";

                    cmd.AddOption("file", positional: true, help: "(Required) File path of the input .txt file");

                    cmd.Executed += (s, e) => ExecuteDayOneCommand(e.Args.GetOption("file"));
                });

                parser.DefineCommand("day-two", cmd =>
                {
                    cmd.Help = "Executes day two of the Advent of Code 2018 exercise";

                    cmd.AddOption("file", positional: true, help: "(Required) File path of the input .txt file");

                    cmd.Executed += (s, e) => ExecuteDayTwoCommand(e.Args.GetOption("file"));
                });

                parser.DefineCommand("day-three", cmd =>
                {
                    cmd.Help = "Executes day three of the Advent of Code 2018 exercise";

                    cmd.AddOption("file", positional: true, help: "(Required) File path of the input .txt file");

                    cmd.Executed += (s, e) => ExecuteDayThreeCommand(e.Args.GetOption("file"));
                });

                parser.Parse(args)
                    .Execute();
            }
            catch (Exception ex)
            {
                Out.WriteException(ex);
            }
        }

        private static void ExecuteDayOneCommand(string filePath)
        {
            DeviceCalibrator deviceCalibrator = new DeviceCalibrator(filePath);
            Out.WriteLine($"Device calibration value: {deviceCalibrator.GetCalibrationValue(0)}");
            Out.WriteLine($"First repeated sum: {deviceCalibrator.GetFirstRepeatedSum()}");
        }

        private static void ExecuteDayTwoCommand(string filePath)
        {
            InventoryManagementSystem inventoryManagementSystem = new InventoryManagementSystem(filePath);
            Out.WriteLine($"Check sum: {inventoryManagementSystem.GetChecksum()}");
            Out.WriteLine($"Common string: {inventoryManagementSystem.GetLettersCommonBetweenCorrectBoxes()}");
        }

        private static void ExecuteDayThreeCommand(string filePath)
        {
            SantaSuitFabricCalculator santaSuitFabricCalculator = new SantaSuitFabricCalculator(filePath);
            Out.WriteLine($"Number of overlapping squares: {santaSuitFabricCalculator.GetNumberOfOverlappingSquares()}");
            Out.WriteLine($"Claim ID with no overlapping squares: {santaSuitFabricCalculator.GetFabricClaimIdOfNonOverlappingClaim()}");
        }
    }
}
