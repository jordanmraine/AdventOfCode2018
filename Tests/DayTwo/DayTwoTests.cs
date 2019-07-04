using System;
using System.IO;

using Xunit;

using DayTwo;

namespace Tests.DayTwo
{
    public class DayTwoTests
    {
        private readonly string Part1Sample1FilePath = $"{Directory.GetCurrentDirectory()}/DayTwo/part1Sample1.txt";
        private readonly string Part2Sample1FilePath = $"{Directory.GetCurrentDirectory()}/DayTwo/part2Sample1.txt";

        private readonly string NoCommonStringsFilePath = $"{Directory.GetCurrentDirectory()}/DayTwo/noCommonStrings.txt";

        [Fact]
        public void InventoryManagementSystem_Constructor_NoExceptionThrown()
        {
            InventoryManagementSystem inventoryManagementSystem = new InventoryManagementSystem(Part1Sample1FilePath);
            Assert.NotNull(inventoryManagementSystem);
        }

        [Fact]
        public void InventoryManagementSystem_Constructor_ArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new InventoryManagementSystem(null));
            Assert.Throws<ArgumentNullException>(() => new InventoryManagementSystem(string.Empty));
        }

        [Fact]
        public void InventoryManagementSystem_Constructor_FileNotFoundExceptionThrown()
        {
            Assert.Throws<FileNotFoundException>(() => new InventoryManagementSystem("This/Is/Not/A/Real/Directory"));
        }

        [Fact]
        public void InventoryManagementSystem_GetChecksum_Succeeds()
        {
            InventoryManagementSystem inventoryManagementSystem = new InventoryManagementSystem(Part1Sample1FilePath);
            int result = inventoryManagementSystem.GetChecksum();
            Assert.Equal(12, result);
        }

        [Fact]
        public void InventoryManagementSystem_GetLettersCommonBetweenCorrectBoxes_NoCommonStringsExceptionThrown()
        {
            InventoryManagementSystem inventoryManagementSystem = new InventoryManagementSystem(NoCommonStringsFilePath);
            Assert.Throws<NoCommonStringsException>(() => inventoryManagementSystem.GetLettersCommonBetweenCorrectBoxes());
        }

        [Fact]
        public void InventoryManagementSystem_GetLettersCommonBetweenCorrectBoxes_Succeeds()
        {
            InventoryManagementSystem inventoryManagementSystem = new InventoryManagementSystem(Part2Sample1FilePath);
            string result = inventoryManagementSystem.GetLettersCommonBetweenCorrectBoxes();
            Assert.Equal("fgij", result);
        }
    }
}
