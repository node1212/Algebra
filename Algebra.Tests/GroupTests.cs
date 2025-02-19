using Algebra.Core;
using Algebra.Core.Permutations.T4;

namespace Algebra.Tests
{
    public class GroupTests
    {
        [Fact]
        public void Permutation3Group_Passes_Validation()
        {
            var S3 = new MultiplicativeGroup<PermutationOf3Int>(PermutationOf3Int.Generate());

            Assert.True(S3.IsValid);
        }

        [Fact]
        public void Permutation4Group_Passes_Validation()
        {
            var S4 = new MultiplicativeGroup<PermutationOf4Char>(PermutationOf4Char.Generate());

            Assert.True(S4.IsValid);
        }

        [Fact]
        public void Multiplication_Should_Work()
        {
            var e = PermutationOf3Int.MultiplicativeIdentity;
            var rho1 = new PermutationOf3Int(2, 3, 1); // (123)
            var rho2 = new PermutationOf3Int(3, 1, 2); // (132)
            var sigma1 = new PermutationOf3Int(2, 1, 3); // (12)
            var sigma2 = new PermutationOf3Int(1, 3, 2); // (23)
            var sigma3 = new PermutationOf3Int(3, 2, 1); // (13)

            Assert.Equal(rho2, rho1 * rho1);
            Assert.Equal(sigma2, sigma1 * rho1);
            Assert.Equal(sigma3, sigma1 * rho1 * rho1);

            Assert.Equal(e, sigma1 * sigma1);
            Assert.Equal(e, rho1 * rho1 * rho1);
            Assert.Equal(sigma1 * rho1 * rho1, rho1 * sigma1);
        }

        [Fact]
        public void Parsing_Should_Work()
        {
            var e = PermutationOf3Int.Parse("(1)(2)(3)").ToString(); // 1 2 3
            var rho1 = PermutationOf3Int.Parse("(123)").ToString();  // 2 3 1
            var rho2 = PermutationOf3Int.Parse("(132)").ToString();  // 1 3 2
            var sigma1 = PermutationOf3Int.Parse("(12)").ToString(); // 2 1 3
            var sigma2 = PermutationOf3Int.Parse("(23)").ToString(); // 1 3 2
            var sigma3 = PermutationOf3Int.Parse("(13)").ToString(); // 3 2 1

            Assert.Equal("e ", e);
            Assert.Equal("ρ₁", rho1);
            Assert.Equal("ρ₂", rho2);
            Assert.Equal("σ₁", sigma1);
            Assert.Equal("σ₂", sigma2);
            Assert.Equal("σ₃", sigma3);
        }

        [Fact]
        public void Klein4Group_Passes_Validation()
        {
            var k4g = new Klein4Group();

            Assert.True(k4g.IsValid);
        }
    }
}