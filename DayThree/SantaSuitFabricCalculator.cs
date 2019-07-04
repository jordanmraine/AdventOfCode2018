using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DayThree
{
    public class SantaSuitFabricCalculator
    {
        public string FilePath { get; set; }

        /// <summary>
        /// Inventory management system.
        /// </summary>
        /// <param name="filePath">File path of values to calibrate device with</param>
        public SantaSuitFabricCalculator(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);

            FilePath = filePath;
        }

        /// <summary>
        /// Calculates the number of overlapping squares from all claims.
        /// </summary>
        /// <returns>Number of overlapping squares</returns>
        public int GetNumberOfOverlappingSquares()
        {
            int[,] fabricArray = new int[1000,1000];

            foreach (FabricClaim fabricClaim in GetFabricClaimsFromFile(FilePath))
            {
                for (int x = fabricClaim.X; x < fabricClaim.X + fabricClaim.Width; x++)
                {
                    for (int y = fabricClaim.Y; y < fabricClaim.Y + fabricClaim.Height; y++)
                    {
                        fabricArray[x, y]++;
                    }
                }
            }

            int numCollisions = 0;

            for (int row = 0; row < fabricArray.GetLength(0); row++)
            {
                for (int column = 0; column < fabricArray.GetLength(1); column++)
                {
                    if (fabricArray[row, column] > 1)
                    {
                        numCollisions++;
                    }
                }
            }

            return numCollisions;
        }

        private IEnumerable<FabricClaim> GetFabricClaimsFromFile(string filePath)
        {
            foreach (string fabricClaimString in File.ReadLines(filePath))
            {
                // Gets the number between "#" and " @".
                Regex claimIdRegex = new Regex(@"\#(\d+)\ @");
                int claimId = Convert.ToInt32(claimIdRegex.Match(fabricClaimString).Groups[1].Value);

                // Gets the number between "@ " and ",".
                Regex xRegex = new Regex(@"\@ (\d+)\,");
                int x = Convert.ToInt32(xRegex.Match(fabricClaimString).Groups[1].Value);

                // Gets the number between "," and ":".
                Regex yRegex = new Regex(@"\,(\d+)\:");
                int y = Convert.ToInt32(yRegex.Match(fabricClaimString).Groups[1].Value);

                // Gets the number between ": " and "x".
                Regex widthRegex = new Regex(@"\: (\d+)x");
                int width = Convert.ToInt32(widthRegex.Match(fabricClaimString).Groups[1].Value);

                // Gets the number after "x".
                Regex heightRegex = new Regex(@"x(\d+)");
                int height = Convert.ToInt32(heightRegex.Match(fabricClaimString).Groups[1].Value);

                yield return new FabricClaim
                {
                    ClaimID = claimId,
                    Height = height,
                    Width = width,
                    X = x,
                    Y = y
                };
            }
        }
    }
}
