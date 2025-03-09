using Algebra.Core;
using Algebra.Core.Permutations.T4;
using FluentAssertions;

namespace Algebra.Tests
{
    public class GroupTests
    {
        private static readonly PermutationOf3Int e = PermutationOf3Int.MultiplicativeIdentity;
        private static readonly PermutationOf3Int rho1 = new(2, 3, 1); // (123)
        private static readonly PermutationOf3Int rho2 = new(3, 1, 2); // (132)
        private static readonly PermutationOf3Int sigma1 = new(2, 1, 3); // (12)
        private static readonly PermutationOf3Int sigma2 = new(1, 3, 2); // (23)
        private static readonly PermutationOf3Int sigma3 = new(3, 2, 1); // (13)

        [Fact]
        public void Permutation4Group_Passes_Validation()
        {
            var S4 = new MultiplicativeGroup<PermutationOf4Char>(PermutationOf4Char.Generate());

            S4.IsValid().Should().BeTrue();
        }

        [Fact]
        public void Klein4Group_Passes_Validation()
        {
            var k4g = new Klein4Group();

            k4g.IsValid().Should().BeTrue();
        }

        [Fact]
        public void SubGroup_Tests()
        {
            var S3 = new MultiplicativeGroup<PermutationOf3Int>(e, rho1, rho2, sigma1, sigma2, sigma3);

            S3.IsValid().Should().BeTrue();

            S3.HasSubgroup(e).Should().BeTrue();
            S3.HasTrivialSubgroup(e).Should().BeTrue();

            S3.HasSubgroup(e, rho1, rho2).Should().BeTrue();
            S3.HasTrivialSubgroup(e, rho1, rho2).Should().BeFalse();

            S3.HasSubgroup(e, sigma1).Should().BeTrue();
            S3.HasTrivialSubgroup(e, sigma1).Should().BeFalse();

            S3.HasSubgroup(e, sigma2).Should().BeTrue();
            S3.HasTrivialSubgroup(e, sigma2).Should().BeFalse();

            S3.HasSubgroup(e, sigma3).Should().BeTrue();
            S3.HasTrivialSubgroup(e, sigma3).Should().BeFalse();

            S3.HasSubgroup(e, rho1, rho2, sigma1, sigma2, sigma3).Should().BeTrue();
            S3.HasTrivialSubgroup(e, rho1, rho2, sigma1, sigma2, sigma3).Should().BeTrue();

            S3.HasSubgroup(e, rho1).Should().BeFalse();
            S3.HasSubgroup(e, rho2).Should().BeFalse();
            S3.HasSubgroup(e, sigma1, sigma2).Should().BeFalse();
            S3.HasSubgroup(e, sigma1, sigma3).Should().BeFalse();
            S3.HasSubgroup(e, sigma2, sigma3).Should().BeFalse();
        }

        [Fact]
        public void FindSubgroups_Tests()
        {
            var S3 = new MultiplicativeGroup<PermutationOf3Int>(e, rho1, rho2, sigma1, sigma2, sigma3);
            var nonTrivialSubgroups = S3.FindNonTrivialSubgroups().ToArray();

            nonTrivialSubgroups.Should().HaveCount(4);
            nonTrivialSubgroups.Where(s => s.Count() == 2).Should().HaveCount(3);
            nonTrivialSubgroups.Where(s => s.Count() == 3).Should().ContainSingle();
        }

        [Fact]
        public void Coset_Tests_PermutationOf3Int()
        {
            var S3 = new MultiplicativeGroup<PermutationOf3Int>(e, rho1, rho2, sigma1, sigma2, sigma3);

            var subgroup = new[] { e, rho1, rho2 };
            var eLeftCoset1 = S3.GetCoset(e, CosetType.Left, subgroup);            // { e, rho1, rho2 }
            var eRightCoset1 = S3.GetCoset(e, CosetType.Right, subgroup);          // { e, rho1, rho2 }
            var rho1LeftCoset = S3.GetCoset(rho1, CosetType.Left, subgroup);       // { e, rho1, rho2 }
            var rho1RightCoset = S3.GetCoset(rho1, CosetType.Right, subgroup);     // { e, rho1, rho2 }
            var rho2LeftCoset = S3.GetCoset(rho2, CosetType.Left, subgroup);       // { e, rho1, rho2 }
            var rho2RightCoset = S3.GetCoset(rho2, CosetType.Right, subgroup);     // { e, rho1, rho2 }
            var sigma1LeftCoset = S3.GetCoset(sigma1, CosetType.Left, subgroup);   // { sigma1, sigma2, sigma3 }
            var sigma1RightCoset = S3.GetCoset(sigma1, CosetType.Right, subgroup); // { sigma1, sigma2, sigma3 }
            var sigma2LeftCoset = S3.GetCoset(sigma2, CosetType.Left, subgroup);   // { sigma1, sigma2, sigma3 }
            var sigma2RightCoset = S3.GetCoset(sigma2, CosetType.Right, subgroup); // { sigma1, sigma2, sigma3 }
            var sigma3LeftCoset = S3.GetCoset(sigma3, CosetType.Left, subgroup);   // { sigma1, sigma2, sigma3 }
            var sigma3RightCoset = S3.GetCoset(sigma3, CosetType.Right, subgroup); // { sigma1, sigma2, sigma3 }
        }

        [Fact]
        public void Coset_Tests_Klein4Group()
        {
            var k4g = new Klein4Group();

            var subgroup = new[] { 'e', 'a' };
            var eLeftCoset1 = k4g.GetCoset('e', CosetType.Left, subgroup);  // { e, a }
            var eRightCoset1 = k4g.GetCoset('e', CosetType.Right, subgroup);// { e, a }
            var aLeftCoset = k4g.GetCoset('a', CosetType.Left, subgroup);   // { e, a }
            var aRightCoset = k4g.GetCoset('a', CosetType.Right, subgroup); // { e, a }
            var bLeftCoset = k4g.GetCoset('b', CosetType.Left, subgroup);   // { b, c }
            var bRightCoset = k4g.GetCoset('b', CosetType.Right, subgroup); // { b, c }
            var cLeftCoset = k4g.GetCoset('c', CosetType.Left, subgroup);   // { b, c }
            var cRightCoset = k4g.GetCoset('c', CosetType.Right, subgroup); // { b, c }
        }
    }
}