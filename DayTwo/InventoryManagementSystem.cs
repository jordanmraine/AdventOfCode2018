using System;
using System.IO;
using System.Linq;

namespace DayTwo
{
    public class InventoryManagementSystem
    {
        public string FilePath { get; set; }

        /// <summary>
        /// Inventory management system.
        /// </summary>
        /// <param name="filePath">File path of values to calibrate device with</param>
        public InventoryManagementSystem(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);

            FilePath = filePath;
        }

        /// <summary>
        /// Calculates the checksum for the file.
        /// </summary>
        /// <returns>Returns the checksum</returns>
        public int GetChecksum()
        {
            int numLinesContainingTwoRepeatedCharacters = 0;
            int numLinesContainingThreeRepeatedCharaters = 0;

            foreach (string line in File.ReadAllLines(FilePath))
            {
                var grouped = line.GroupBy(l => l);

                // Convert.ToInt32 on the .Any() call would do the same but ternary is a bit cleaner..
                numLinesContainingTwoRepeatedCharacters += grouped.Any(c => c.Count() == 2) ? 1 : 0;
                numLinesContainingThreeRepeatedCharaters += grouped.Any(c => c.Count() == 3) ? 1 : 0;
            }

            return numLinesContainingTwoRepeatedCharacters * numLinesContainingThreeRepeatedCharaters;
        }
    }
}
