using System;
using System.IO;

namespace Solutions
{
    public class AdventOfCodeBase
    {
        public string FilePath { get; set; }

        public AdventOfCodeBase(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);

            FilePath = filePath;
        }
    }
}
