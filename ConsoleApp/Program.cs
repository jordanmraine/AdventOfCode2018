﻿using System;

using Kirkin.CommandLine;

using DayOne;

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
        }
    }
}
