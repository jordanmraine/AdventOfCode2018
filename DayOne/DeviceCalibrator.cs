using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DayOne
{
    public class DeviceCalibrator
    {
        public string FilePath { get; set; }

        /// <summary>
        /// Calibrates a device.
        /// </summary>
        /// <param name="filePath">File path of values to calibrate device with</param>
        public DeviceCalibrator(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);

            FilePath = filePath;
        }

        /// <summary>
        /// Gets the calibration value.
        /// </summary>
        /// <returns>The sum of all values in FilePath</returns>
        public int GetCalibrationValue(int startFrequency)
        {
            int runningTotal = startFrequency;

            foreach (string line in File.ReadLines(FilePath))
            {
                runningTotal += int.Parse(line);
            }

            return runningTotal;
        }

        /// <summary>
        /// Gets the first repeated summed number.
        /// </summary>
        /// <returns>The first repeated summed number</returns>
        public int GetFirstRepeatedSum()
        {
            IList<int> values = new List<int>(File.ReadAllLines(FilePath).Select(l => int.Parse(l)));

            int i = 0;
            int length = values.Count;
            int runningTotal = 0;
            HashSet<int> sums = new HashSet<int>(new[] { runningTotal });

            while(true)
            {
                runningTotal += values[i++ % length];

                // HashSet.Add() returns false if the value already exists in the set.
                if (!sums.Add(runningTotal))
                {
                    return runningTotal;
                }
            }
        }
    }
}
