using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Finds the letters that are common between the 2 correct boxes.
        /// </summary>
        /// <returns>The letters as a string that are common between the 2 correct boxes.</returns>
        /// <exception cref="NoCommonStringsException">Thrown if there's no 2 strings with only one character differing.</exception>
        public string GetLettersCommonBetweenCorrectBoxes()
        {
            // Minor perf: sort the list of strings so common strings
            // have a higher chance of being close to each other.
            IList<string> allBoxStrings = File.ReadAllLines(FilePath)
                .OrderBy(s => s)
                .ToList();

            // Loop through all strings.
            for (int i = 0; i < allBoxStrings.Count; i++)
            {
                string originalString = allBoxStrings[i];

                // Loop through all the strings we haven't checked yet.
                for (int j = i + 1; j < allBoxStrings.Count; j++)
                {
                    string stringToCompare = allBoxStrings[j];

                    // If the strings aren't the same length we can move on.
                    if (originalString.Length != stringToCompare.Length)
                    {
                        break;
                    }

                    int numDifferences = 0;

                    // Loop through each char in the original string and compare it to the new string.
                    for (int k = 0; k < originalString.Length; k++)
                    {
                        if (k >= stringToCompare.Length)
                        {
                            break;
                        }

                        if (originalString[k] != stringToCompare[k])
                        {
                            numDifferences++;
                        }

                        // More than one difference, move on.
                        if (numDifferences > 1)
                        {
                            break;
                        }
                    }

                    if (numDifferences == 1)
                    {
                        return GetCommonLettersBetweenTwoStrings(originalString, stringToCompare);
                    }
                }
            }

            throw new NoCommonStringsException();
        }

        private string GetCommonLettersBetweenTwoStrings(string firstString, string secondString)
        {
            string commonCharacters = string.Empty;

            for (int i = 0; i < firstString.Length; i++)
            {
                if (firstString[i] == secondString[i])
                {
                    commonCharacters += firstString[i];
                }
            }

            return commonCharacters;
        }
    }

    public class NoCommonStringsException : Exception
    {
        public NoCommonStringsException() : base() { }

        public NoCommonStringsException(string message) : base(message) { }

        public NoCommonStringsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
