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

        private static readonly MultiplicativeGroup<PermutationOf3Int> S3 = new(e, rho1, rho2, sigma1, sigma2, sigma3);
        private static readonly Klein4Group k4g = new();

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
            var nonTrivialSubgroups = S3.FindNonTrivialSubgroups().ToArray();

            nonTrivialSubgroups.Should().HaveCount(4);
            nonTrivialSubgroups.Where(s => s.Count() == 2).Should().HaveCount(3);
            nonTrivialSubgroups.Where(s => s.Count() == 3).Should().ContainSingle();
        }

        [Fact]
        public void NormalSubroups_Tests()
        {
            S3.HasNormalSubgroup(e, rho1, rho2).Should().BeTrue();
            S3.HasNormalSubgroup(e, sigma1).Should().BeFalse();
            S3.HasNormalSubgroup(e, sigma2).Should().BeFalse();
            S3.HasNormalSubgroup(e, sigma3).Should().BeFalse();

            k4g.HasNormalSubgroup('e', 'a').Should().BeTrue();
            k4g.HasNormalSubgroup('e', 'b').Should().BeTrue();
            k4g.HasNormalSubgroup('e', 'c').Should().BeTrue();
        }

        [Fact]
        public void S3_Coset_Tests_NormalSubgroup()
        {
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

            eLeftCoset1.SetEquals(subgroup).Should().BeTrue();
            rho1LeftCoset.SetEquals(subgroup).Should().BeTrue();
            rho2LeftCoset.SetEquals(subgroup).Should().BeTrue();
            sigma1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma3LeftCoset.SetEquals(subgroup).Should().BeFalse();

            eLeftCoset1.SetEquals(eRightCoset1).Should().BeTrue();
            rho1LeftCoset.SetEquals(rho1RightCoset).Should().BeTrue();
            rho2LeftCoset.SetEquals(rho2RightCoset).Should().BeTrue();
            sigma1LeftCoset.SetEquals(sigma1RightCoset).Should().BeTrue();
            sigma2LeftCoset.SetEquals(sigma2RightCoset).Should().BeTrue();
            sigma3LeftCoset.SetEquals(sigma3RightCoset).Should().BeTrue();
        }

        [Fact]
        public void S3_Coset_Tests_e_sigma1()
        {
            var subgroup = new[] { e, sigma1 };

            var eLeftCoset1 = S3.GetCoset(e, CosetType.Left, subgroup);            // { e, sigma1 }
            var eRightCoset1 = S3.GetCoset(e, CosetType.Right, subgroup);          // { e, sigma1 }

            var rho1LeftCoset = S3.GetCoset(rho1, CosetType.Left, subgroup);       // { rho1, sigma3 }
            var rho1RightCoset = S3.GetCoset(rho1, CosetType.Right, subgroup);     // { rho1, sigma2 }

            var rho2LeftCoset = S3.GetCoset(rho2, CosetType.Left, subgroup);       // { rho2, sigma2 }
            var rho2RightCoset = S3.GetCoset(rho2, CosetType.Right, subgroup);     // { rho2, sigma3 }

            var sigma1LeftCoset = S3.GetCoset(sigma1, CosetType.Left, subgroup);   // { e, sigma1 }
            var sigma1RightCoset = S3.GetCoset(sigma1, CosetType.Right, subgroup); // { e, sigma1 }

            var sigma2LeftCoset = S3.GetCoset(sigma2, CosetType.Left, subgroup);   // { rho2, sigma2 }
            var sigma2RightCoset = S3.GetCoset(sigma2, CosetType.Right, subgroup); // { rho1, sigma2 }

            var sigma3LeftCoset = S3.GetCoset(sigma3, CosetType.Left, subgroup);   // { rho1, sigma3 }
            var sigma3RightCoset = S3.GetCoset(sigma3, CosetType.Right, subgroup); // { rho2, sigma3 }

            eLeftCoset1.SetEquals(subgroup).Should().BeTrue();
            rho1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            rho2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma1LeftCoset.SetEquals(subgroup).Should().BeTrue();
            sigma2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma3LeftCoset.SetEquals(subgroup).Should().BeFalse();

            eLeftCoset1.SetEquals(eRightCoset1).Should().BeTrue();
            rho1LeftCoset.SetEquals(rho1RightCoset).Should().BeFalse();
            rho2LeftCoset.SetEquals(rho2RightCoset).Should().BeFalse();
            sigma1LeftCoset.SetEquals(sigma1RightCoset).Should().BeTrue();
            sigma2LeftCoset.SetEquals(sigma2RightCoset).Should().BeFalse();
            sigma3LeftCoset.SetEquals(sigma3RightCoset).Should().BeFalse();
        }

        [Fact]
        public void S3_Coset_Tests_e_sigma2()
        {
            var subgroup = new[] { e, sigma2 };

            var eLeftCoset1 = S3.GetCoset(e, CosetType.Left, subgroup);            // { }
            var eRightCoset1 = S3.GetCoset(e, CosetType.Right, subgroup);          // { }

            var rho1LeftCoset = S3.GetCoset(rho1, CosetType.Left, subgroup);       // { }
            var rho1RightCoset = S3.GetCoset(rho1, CosetType.Right, subgroup);     // { }

            var rho2LeftCoset = S3.GetCoset(rho2, CosetType.Left, subgroup);       // { }
            var rho2RightCoset = S3.GetCoset(rho2, CosetType.Right, subgroup);     // { }

            var sigma1LeftCoset = S3.GetCoset(sigma1, CosetType.Left, subgroup);   // { }
            var sigma1RightCoset = S3.GetCoset(sigma1, CosetType.Right, subgroup); // { }

            var sigma2LeftCoset = S3.GetCoset(sigma2, CosetType.Left, subgroup);   // { }
            var sigma2RightCoset = S3.GetCoset(sigma2, CosetType.Right, subgroup); // { }

            var sigma3LeftCoset = S3.GetCoset(sigma3, CosetType.Left, subgroup);   // { }
            var sigma3RightCoset = S3.GetCoset(sigma3, CosetType.Right, subgroup); // { }

            eLeftCoset1.SetEquals(subgroup).Should().BeTrue();
            rho1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            rho2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma2LeftCoset.SetEquals(subgroup).Should().BeTrue();
            sigma3LeftCoset.SetEquals(subgroup).Should().BeFalse();

            eLeftCoset1.SetEquals(eRightCoset1).Should().BeTrue();
            rho1LeftCoset.SetEquals(rho1RightCoset).Should().BeFalse();
            rho2LeftCoset.SetEquals(rho2RightCoset).Should().BeFalse();
            sigma1LeftCoset.SetEquals(sigma1RightCoset).Should().BeFalse();
            sigma2LeftCoset.SetEquals(sigma2RightCoset).Should().BeTrue();
            sigma3LeftCoset.SetEquals(sigma3RightCoset).Should().BeFalse();
        }

        [Fact]
        public void S3_Coset_Tests_e_sigma3()
        {
            var subgroup = new[] { e, sigma3 };

            var eLeftCoset1 = S3.GetCoset(e, CosetType.Left, subgroup);            // { }
            var eRightCoset1 = S3.GetCoset(e, CosetType.Right, subgroup);          // { }

            var rho1LeftCoset = S3.GetCoset(rho1, CosetType.Left, subgroup);       // { }
            var rho1RightCoset = S3.GetCoset(rho1, CosetType.Right, subgroup);     // { }

            var rho2LeftCoset = S3.GetCoset(rho2, CosetType.Left, subgroup);       // { }
            var rho2RightCoset = S3.GetCoset(rho2, CosetType.Right, subgroup);     // { }

            var sigma1LeftCoset = S3.GetCoset(sigma1, CosetType.Left, subgroup);   // { }
            var sigma1RightCoset = S3.GetCoset(sigma1, CosetType.Right, subgroup); // { }

            var sigma2LeftCoset = S3.GetCoset(sigma2, CosetType.Left, subgroup);   // { }
            var sigma2RightCoset = S3.GetCoset(sigma2, CosetType.Right, subgroup); // { }

            var sigma3LeftCoset = S3.GetCoset(sigma3, CosetType.Left, subgroup);   // { }
            var sigma3RightCoset = S3.GetCoset(sigma3, CosetType.Right, subgroup); // { }

            eLeftCoset1.SetEquals(subgroup).Should().BeTrue();
            rho1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            rho2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma3LeftCoset.SetEquals(subgroup).Should().BeTrue();

            eLeftCoset1.SetEquals(eRightCoset1).Should().BeTrue();
            rho1LeftCoset.SetEquals(rho1RightCoset).Should().BeFalse();
            rho2LeftCoset.SetEquals(rho2RightCoset).Should().BeFalse();
            sigma1LeftCoset.SetEquals(sigma1RightCoset).Should().BeFalse();
            sigma2LeftCoset.SetEquals(sigma2RightCoset).Should().BeFalse();
            sigma3LeftCoset.SetEquals(sigma3RightCoset).Should().BeTrue();
        }

        [Fact]
        public void Klein4Group_Coset_Tests_e_a()
        {
            var subgroup = new[] { 'e', 'a' };

            var eLeftCoset = k4g.GetCoset('e', CosetType.Left, subgroup);   // { e, a }
            var eRightCoset = k4g.GetCoset('e', CosetType.Right, subgroup); // { e, a }
            var aLeftCoset = k4g.GetCoset('a', CosetType.Left, subgroup);   // { e, a }
            var aRightCoset = k4g.GetCoset('a', CosetType.Right, subgroup); // { e, a }
            var bLeftCoset = k4g.GetCoset('b', CosetType.Left, subgroup);   // { b, c }
            var bRightCoset = k4g.GetCoset('b', CosetType.Right, subgroup); // { b, c }
            var cLeftCoset = k4g.GetCoset('c', CosetType.Left, subgroup);   // { b, c }
            var cRightCoset = k4g.GetCoset('c', CosetType.Right, subgroup); // { b, c }

            eLeftCoset.SetEquals(subgroup).Should().BeTrue();
            aLeftCoset.SetEquals(subgroup).Should().BeTrue();
            bLeftCoset.SetEquals(subgroup).Should().BeFalse();
            cLeftCoset.SetEquals(subgroup).Should().BeFalse();

            eLeftCoset.SetEquals(eRightCoset).Should().BeTrue();
            aLeftCoset.SetEquals(aRightCoset).Should().BeTrue();
            bLeftCoset.SetEquals(bRightCoset).Should().BeTrue();
            cLeftCoset.SetEquals(cRightCoset).Should().BeTrue();
        }

        [Fact]
        public void Klein4Group_Coset_Tests_e_b()
        {
            var subgroup = new[] { 'e', 'b' };

            var eLeftCoset = k4g.GetCoset('e', CosetType.Left, subgroup);
            var eRightCoset = k4g.GetCoset('e', CosetType.Right, subgroup);
            var aLeftCoset = k4g.GetCoset('a', CosetType.Left, subgroup);
            var aRightCoset = k4g.GetCoset('a', CosetType.Right, subgroup);
            var bLeftCoset = k4g.GetCoset('b', CosetType.Left, subgroup);
            var bRightCoset = k4g.GetCoset('b', CosetType.Right, subgroup);
            var cLeftCoset = k4g.GetCoset('c', CosetType.Left, subgroup);
            var cRightCoset = k4g.GetCoset('c', CosetType.Right, subgroup);

            eLeftCoset.SetEquals(subgroup).Should().BeTrue();
            aLeftCoset.SetEquals(subgroup).Should().BeFalse();
            bLeftCoset.SetEquals(subgroup).Should().BeTrue();
            cLeftCoset.SetEquals(subgroup).Should().BeFalse();

            eLeftCoset.SetEquals(eRightCoset).Should().BeTrue();
            aLeftCoset.SetEquals(aRightCoset).Should().BeTrue();
            bLeftCoset.SetEquals(bRightCoset).Should().BeTrue();
            cLeftCoset.SetEquals(cRightCoset).Should().BeTrue();
        }

        [Fact]
        public void Klein4Group_Coset_Tests_e_c()
        {
            var subgroup = new[] { 'e', 'c' };

            var eLeftCoset = k4g.GetCoset('e', CosetType.Left, subgroup);
            var eRightCoset = k4g.GetCoset('e', CosetType.Right, subgroup);
            var aLeftCoset = k4g.GetCoset('a', CosetType.Left, subgroup);
            var aRightCoset = k4g.GetCoset('a', CosetType.Right, subgroup);
            var bLeftCoset = k4g.GetCoset('b', CosetType.Left, subgroup);
            var bRightCoset = k4g.GetCoset('b', CosetType.Right, subgroup);
            var cLeftCoset = k4g.GetCoset('c', CosetType.Left, subgroup);
            var cRightCoset = k4g.GetCoset('c', CosetType.Right, subgroup);

            eLeftCoset.SetEquals(subgroup).Should().BeTrue();
            aLeftCoset.SetEquals(subgroup).Should().BeFalse();
            bLeftCoset.SetEquals(subgroup).Should().BeFalse();
            cLeftCoset.SetEquals(subgroup).Should().BeTrue();

            eLeftCoset.SetEquals(eRightCoset).Should().BeTrue();
            aLeftCoset.SetEquals(aRightCoset).Should().BeTrue();
            bLeftCoset.SetEquals(bRightCoset).Should().BeTrue();
            cLeftCoset.SetEquals(cRightCoset).Should().BeTrue();
        }
    }
}