using System;
using System.IO;

using Xunit;

using Solutions.DayThree;

namespace Tests.DayThree
{
    public class DayThreeTests
    {
        private readonly string Part1Sample1FilePath = $"{Directory.GetCurrentDirectory()}/DayThree/part1Sample1.txt";
        private readonly string Part2Sample1FilePath = $"{Directory.GetCurrentDirectory()}/DayThree/part2Sample1.txt";

        private readonly string MultipleUniqueClaimsFilePath = $"{Directory.GetCurrentDirectory()}/DayThree/multipleUniqueClaims.txt";
        private readonly string NoUniqueClaimsFilePath = $"{Directory.GetCurrentDirectory()}/DayThree/noUniqueClaims.txt";

        [Fact]
        public void SantaSuitFabricCalculator_Constructor_NoExceptionThrown()
        {
            SantaSuitFabricCalculator santaSuitFabricCalculator = new SantaSuitFabricCalculator(Part1Sample1FilePath);
            Assert.NotNull(santaSuitFabricCalculator);
        }

        [Fact]
        public void SantaSuitFabricCalculator_Constructor_ArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new SantaSuitFabricCalculator(null));
            Assert.Throws<ArgumentNullException>(() => new SantaSuitFabricCalculator(string.Empty));
        }

        [Fact]
        public void SantaSuitFabricCalculator_Constructor_FileNotFoundExceptionThrown()
        {
            Assert.Throws<FileNotFoundException>(() => new SantaSuitFabricCalculator("This/Is/Not/A/Real/Directory"));
        }

        [Fact]
        public void SantaSuitFabricCalculator_GetNumberOfOverlappingSquares_Succeeds()
        {
            SantaSuitFabricCalculator santaSuitFabricCalculator = new SantaSuitFabricCalculator(Part1Sample1FilePath);
            int result = santaSuitFabricCalculator.GetNumberOfOverlappingSquares();
            Assert.Equal(4, result);
        }

        [Fact]
        public void SantaSuitFabricCalculator_GetFabricClaimIdOfNonOverlappingClaim_MultipleUniqueClaimFoundExceptionThrown()
        {
            SantaSuitFabricCalculator santaSuitFabricCalculator = new SantaSuitFabricCalculator(MultipleUniqueClaimsFilePath);
            Assert.Throws<MultipleUniqueClaimFoundException>(() => santaSuitFabricCalculator.GetFabricClaimIdOfNonOverlappingClaim());
        }

        [Fact]
        public void SantaSuitFabricCalculator_GetFabricClaimIdOfNonOverlappingClaim_NoUniqueClaimFoundExceptionThrown()
        {
            SantaSuitFabricCalculator santaSuitFabricCalculator = new SantaSuitFabricCalculator(NoUniqueClaimsFilePath);
            Assert.Throws<NoUniqueClaimFoundException>(() => santaSuitFabricCalculator.GetFabricClaimIdOfNonOverlappingClaim());
        }

        [Fact]
        public void SantaSuitFabricCalculator_GetFabricClaimIdOfNonOverlappingClaim_Succeeds()
        {
            SantaSuitFabricCalculator santaSuitFabricCalculator = new SantaSuitFabricCalculator(Part2Sample1FilePath);
            int result = santaSuitFabricCalculator.GetFabricClaimIdOfNonOverlappingClaim();
            Assert.Equal(3, result);
        }
    }
}
