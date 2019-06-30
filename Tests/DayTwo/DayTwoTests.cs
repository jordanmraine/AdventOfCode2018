using System;
using System.IO;

using Xunit;

using DayTwo;

namespace Tests.DayTwo
{
    public class DayTwoTests
    {
        private readonly string Part1Sample1FilePath = Directory.GetCurrentDirectory() + "/DayTwo/part1Sample1.txt";

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
    }
}
