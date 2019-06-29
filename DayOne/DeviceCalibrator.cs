using System;
using System.IO;

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
    }
}
