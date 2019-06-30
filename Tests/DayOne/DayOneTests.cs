using System;
using System.IO;

using Xunit;

using DayOne;

namespace Tests.DayOne
{
    public class DayOneTests
    {
        private readonly string AllNumbersFilePath = Directory.GetCurrentDirectory() + "/DayOne/allNumbers.txt";
        private readonly string ContainsLettersFilePath = Directory.GetCurrentDirectory() + "/DayOne/containsLetters.txt";

        [Fact]
        public void DeviceCalibrator_Constructor_NoExceptionThrown()
        {
            DeviceCalibrator deviceCalibrator = new DeviceCalibrator(AllNumbersFilePath);
            Assert.NotNull(deviceCalibrator);
        }

        [Fact]
        public void DeviceCalibrator_Constructor_ArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new DeviceCalibrator(null));
            Assert.Throws<ArgumentNullException>(() => new DeviceCalibrator(string.Empty));
        }

        [Fact]
        public void DeviceCalibrator_Constructor_FileNotFOundExceptionThrown()
        {
            Assert.Throws<FileNotFoundException>(() => new DeviceCalibrator("This/Is/Not/A/Real/Directory"));
        }

        [Fact]
        public void DeviceCalibrator_GetCalibrationValue_FormatExceptionThrown()
        {
            DeviceCalibrator deviceCalibrator = new DeviceCalibrator(ContainsLettersFilePath);
            Assert.Throws<FormatException>(() => deviceCalibrator.GetCalibrationValue(0));
        }

        [Fact]
        public void DeviceCalibrator_GetCalibrationValue_Succeeds()
        {
            DeviceCalibrator deviceCalibrator = new DeviceCalibrator(AllNumbersFilePath);
            int calibratedNumber = deviceCalibrator.GetCalibrationValue(0);
            Assert.Equal(31, calibratedNumber);
        }

        [Fact]
        public void DeviceCalibrator_GetFirstRepeatedSum_FormatExceptionThrown()
        {
            DeviceCalibrator deviceCalibrator = new DeviceCalibrator(ContainsLettersFilePath);
            Assert.Throws<FormatException>(() => deviceCalibrator.GetFirstRepeatedSum());
        }

        [Theory]
        [InlineData("sampleOne.txt", 0)]
        [InlineData("sampleTwo.txt", 10)]
        [InlineData("sampleThree.txt", 5)]
        [InlineData("sampleFour.txt", 14)]
        public void DeviceCalibrator_GetFirstRepeatedSum_Succeeds(string fileName, int expectedValue)
        {
            DeviceCalibrator deviceCalibrator = new DeviceCalibrator($"{Directory.GetCurrentDirectory()}/DayOne/{fileName}");
            int result = deviceCalibrator.GetFirstRepeatedSum();
            Assert.Equal(expectedValue, result);
        }
    }
}
