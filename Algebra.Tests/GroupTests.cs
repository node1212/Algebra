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
        public void Klein4Group_Passes_Validation()
        {
            var k4g = new Klein4Group();

            Assert.True(k4g.IsValid);
        }
    }
}