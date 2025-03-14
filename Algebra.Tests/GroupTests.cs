using Algebra.Core;
using Algebra.Core.Groups;
using Algebra.Core.Permutations.T4;
using FluentAssertions;

namespace Algebra.Tests
{
    public class GroupTests
    {
        private static readonly MultiplicativeGroup<PermutationOf3Int> S3 = new S3Int();
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
        public void DihedralGroup_Tests()
        {
            var cayleyTable = new CayleyTable<char>(new char[,]
            {//    e    a    b    c    d    f
           /*e*/{ 'e', 'a', 'b', 'c', 'd', 'f' },
           /*a*/{ 'a', 'e', 'd', 'f', 'b', 'c' },
           /*b*/{ 'b', 'f', 'e', 'd', 'c', 'a' },
           /*c*/{ 'c', 'd', 'f', 'e', 'a', 'b' },
           /*d*/{ 'd', 'c', 'a', 'b', 'f', 'e' },
           /*f*/{ 'f', 'b', 'c', 'a', 'e', 'd' },
            }, 'e', 'a', 'b', 'c', 'd', 'f');

            var D3 = new Group<char>(cayleyTable);

            D3.IsValid().Should().BeTrue();

            D3.IsCyclic().Should().BeFalse();

            var subgroups = D3.FindNonTrivialSubgroups();
            subgroups.Should().HaveCount(4);
            subgroups.Where(s => s.Order == 2).Should().HaveCount(3);
            subgroups.Where(s => s.Order == 3).Should().ContainSingle();

            var normalSubgroup = new[] { 'e', 'd', 'f' };

            D3.HasNormalSubgroup(normalSubgroup).Should().BeTrue();

            var quotientGroup = D3.GetQuotientGroup(normalSubgroup);
            quotientGroup.IsValid().Should().BeTrue();
        }

        [Fact]
        public void SubGroup_Tests()
        {
            S3.IsValid().Should().BeTrue();

            S3.IsCyclic().Should().BeFalse();

            S3.HasSubgroup(S3Int.e).Should().BeTrue();
            S3.HasTrivialSubgroup(S3Int.e).Should().BeTrue();

            S3.HasSubgroup(S3Int.e, S3Int.rho1, S3Int.rho2).Should().BeTrue();
            S3.HasTrivialSubgroup(S3Int.e, S3Int.rho1, S3Int.rho2).Should().BeFalse();

            S3.HasSubgroup(S3Int.e, S3Int.sigma1).Should().BeTrue();
            S3.HasTrivialSubgroup(S3Int.e, S3Int.sigma1).Should().BeFalse();

            S3.HasSubgroup(S3Int.e, S3Int.sigma2).Should().BeTrue();
            S3.HasTrivialSubgroup(S3Int.e, S3Int.sigma2).Should().BeFalse();

            S3.HasSubgroup(S3Int.e, S3Int.sigma3).Should().BeTrue();
            S3.HasTrivialSubgroup(S3Int.e, S3Int.sigma3).Should().BeFalse();

            S3.HasSubgroup(S3Int.e, S3Int.rho1, S3Int.rho2, S3Int.sigma1, S3Int.sigma2, S3Int.sigma3).Should().BeTrue();
            S3.HasTrivialSubgroup(S3Int.e, S3Int.rho1, S3Int.rho2, S3Int.sigma1, S3Int.sigma2, S3Int.sigma3).Should().BeTrue();

            S3.HasSubgroup(S3Int.e, S3Int.rho1).Should().BeFalse();
            S3.HasSubgroup(S3Int.e, S3Int.rho2).Should().BeFalse();
            S3.HasSubgroup(S3Int.e, S3Int.sigma1, S3Int.sigma2).Should().BeFalse();
            S3.HasSubgroup(S3Int.e, S3Int.sigma1, S3Int.sigma3).Should().BeFalse();
            S3.HasSubgroup(S3Int.e, S3Int.sigma2, S3Int.sigma3).Should().BeFalse();
        }

        [Fact]
        public void FindSubgroups_Tests()
        {
            var nonTrivialSubgroups = S3.FindNonTrivialSubgroups().ToArray();

            nonTrivialSubgroups.Should().HaveCount(4);
            nonTrivialSubgroups.Where(s => s.Order == 2).Should().HaveCount(3);
            nonTrivialSubgroups.Where(s => s.Order == 3).Should().ContainSingle();

            var indices = nonTrivialSubgroups.Select(S3.GetSubgroupIndex).ToArray();
        }

        [Fact]
        public void NormalSubroups_Tests()
        {
            S3.HasNormalSubgroup(S3Int.e, S3Int.rho1, S3Int.rho2).Should().BeTrue();
            S3.HasNormalSubgroup(S3Int.e, S3Int.sigma1).Should().BeFalse();
            S3.HasNormalSubgroup(S3Int.e, S3Int.sigma2).Should().BeFalse();
            S3.HasNormalSubgroup(S3Int.e, S3Int.sigma3).Should().BeFalse();

            k4g.HasNormalSubgroup('e', 'a').Should().BeTrue();
            k4g.HasNormalSubgroup('e', 'b').Should().BeTrue();
            k4g.HasNormalSubgroup('e', 'c').Should().BeTrue();

            k4g.IsCyclic().Should().BeFalse(); // TODO move it
        }

        [Fact]
        public void S3_Coset_Tests_NormalSubgroup()
        {
            var subgroup = new[] { S3Int.e, S3Int.rho1, S3Int.rho2 };

            #region Trivial
            var eLeftCoset1 = S3.GetCoset(S3Int.e, CosetType.Left, subgroup).Elements.ToHashSet();            // { e, S3Int.rho1, S3Int.rho2 }
            var eRightCoset1 = S3.GetCoset(S3Int.e, CosetType.Right, subgroup).Elements.ToHashSet();          // { e, S3Int.rho1, S3Int.rho2 }

            var rho1LeftCoset = S3.GetCoset(S3Int.rho1, CosetType.Left, subgroup).Elements.ToHashSet();       // { e, S3Int.rho1, S3Int.rho2 }
            var rho1RightCoset = S3.GetCoset(S3Int.rho1, CosetType.Right, subgroup).Elements.ToHashSet();     // { e, S3Int.rho1, S3Int.rho2 }

            var rho2LeftCoset = S3.GetCoset(S3Int.rho2, CosetType.Left, subgroup).Elements.ToHashSet();       // { e, S3Int.rho1, S3Int.rho2 }
            var rho2RightCoset = S3.GetCoset(S3Int.rho2, CosetType.Right, subgroup).Elements.ToHashSet();     // { e, S3Int.rho1, S3Int.rho2 }
            #endregion

            var sigma1LeftCoset = S3.GetCoset(S3Int.sigma1, CosetType.Left, subgroup).Elements.ToHashSet();   // { S3Int.sigma1, S3Int.sigma2, S3Int.sigma3 }
            var sigma1RightCoset = S3.GetCoset(S3Int.sigma1, CosetType.Right, subgroup).Elements.ToHashSet(); // { S3Int.sigma1, S3Int.sigma2, S3Int.sigma3 }

            var sigma2LeftCoset = S3.GetCoset(S3Int.sigma2, CosetType.Left, subgroup).Elements.ToHashSet();   // { S3Int.sigma1, S3Int.sigma2, S3Int.sigma3 }
            var sigma2RightCoset = S3.GetCoset(S3Int.sigma2, CosetType.Right, subgroup).Elements.ToHashSet(); // { S3Int.sigma1, S3Int.sigma2, S3Int.sigma3 }

            var sigma3LeftCoset = S3.GetCoset(S3Int.sigma3, CosetType.Left, subgroup).Elements.ToHashSet();   // { S3Int.sigma1, S3Int.sigma2, S3Int.sigma3 }
            var sigma3RightCoset = S3.GetCoset(S3Int.sigma3, CosetType.Right, subgroup).Elements.ToHashSet(); // { S3Int.sigma1, S3Int.sigma2, S3Int.sigma3 }

            eLeftCoset1.SetEquals(subgroup).Should().BeTrue();      // trivial
            rho1LeftCoset.SetEquals(subgroup).Should().BeTrue();    // trivial
            rho2LeftCoset.SetEquals(subgroup).Should().BeTrue();    // trivial
            sigma1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma3LeftCoset.SetEquals(subgroup).Should().BeFalse();

            eLeftCoset1.SetEquals(eRightCoset1).Should().BeTrue();         // trivial
            rho1LeftCoset.SetEquals(rho1RightCoset).Should().BeTrue();     // trivial
            rho2LeftCoset.SetEquals(rho2RightCoset).Should().BeTrue();     // trivial
            sigma1LeftCoset.SetEquals(sigma1RightCoset).Should().BeTrue(); // subgroup is normal
            sigma2LeftCoset.SetEquals(sigma2RightCoset).Should().BeTrue(); // subgroup is normal
            sigma3LeftCoset.SetEquals(sigma3RightCoset).Should().BeTrue(); // subgroup is normal
        }

        [Fact]
        public void S3_Coset_Tests_e_sigma1()
        {
            var subgroup = new[] { S3Int.e, S3Int.sigma1 };

            #region Trivial
            var eLeftCoset1 = S3.GetCoset(S3Int.e, CosetType.Left, subgroup).Elements.ToHashSet();            // { e, S3Int.sigma1 }
            var eRightCoset1 = S3.GetCoset(S3Int.e, CosetType.Right, subgroup).Elements.ToHashSet();          // { e, S3Int.sigma1 }
            #endregion

            var rho1LeftCoset = S3.GetCoset(S3Int.rho1, CosetType.Left, subgroup).Elements.ToHashSet();       // { S3Int.rho1, S3Int.sigma3 }
            var rho1RightCoset = S3.GetCoset(S3Int.rho1, CosetType.Right, subgroup).Elements.ToHashSet();     // { S3Int.rho1, S3Int.sigma2 }

            var rho2LeftCoset = S3.GetCoset(S3Int.rho2, CosetType.Left, subgroup).Elements.ToHashSet();       // { S3Int.rho2, S3Int.sigma2 }
            var rho2RightCoset = S3.GetCoset(S3Int.rho2, CosetType.Right, subgroup).Elements.ToHashSet();     // { S3Int.rho2, S3Int.sigma3 }

            #region Trivial
            var sigma1LeftCoset = S3.GetCoset(S3Int.sigma1, CosetType.Left, subgroup).Elements.ToHashSet();   // { e, S3Int.sigma1 }
            var sigma1RightCoset = S3.GetCoset(S3Int.sigma1, CosetType.Right, subgroup).Elements.ToHashSet(); // { e, S3Int.sigma1 }
            #endregion

            var sigma2LeftCoset = S3.GetCoset(S3Int.sigma2, CosetType.Left, subgroup).Elements.ToHashSet();   // { S3Int.rho2, S3Int.sigma2 }
            var sigma2RightCoset = S3.GetCoset(S3Int.sigma2, CosetType.Right, subgroup).Elements.ToHashSet(); // { S3Int.rho1, S3Int.sigma2 }

            var sigma3LeftCoset = S3.GetCoset(S3Int.sigma3, CosetType.Left, subgroup).Elements.ToHashSet();   // { S3Int.rho1, S3Int.sigma3 }
            var sigma3RightCoset = S3.GetCoset(S3Int.sigma3, CosetType.Right, subgroup).Elements.ToHashSet(); // { S3Int.rho2, S3Int.sigma3 }

            #region Is coset equal to subgroup?
            eLeftCoset1.SetEquals(subgroup).Should().BeTrue();      // trivial
            rho1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            rho2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma1LeftCoset.SetEquals(subgroup).Should().BeTrue();  // trivial
            sigma2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma3LeftCoset.SetEquals(subgroup).Should().BeFalse();
            #endregion

            #region Is left coset equal to its right coset?
            eLeftCoset1.SetEquals(eRightCoset1).Should().BeTrue();          // trivial
            rho1LeftCoset.SetEquals(rho1RightCoset).Should().BeFalse();     // subroup is not normal
            rho2LeftCoset.SetEquals(rho2RightCoset).Should().BeFalse();     // subroup is not normal
            sigma1LeftCoset.SetEquals(sigma1RightCoset).Should().BeTrue();  // trivial
            sigma2LeftCoset.SetEquals(sigma2RightCoset).Should().BeFalse(); // subroup is not normal
            sigma3LeftCoset.SetEquals(sigma3RightCoset).Should().BeFalse(); // subroup is not normal
            #endregion
        }

        [Fact]
        public void S3_Coset_Tests_e_sigma2()
        {
            var subgroup = new[] { S3Int.e, S3Int.sigma2 };

            var eLeftCoset1 = S3.GetCoset(S3Int.e, CosetType.Left, subgroup).Elements.ToHashSet();            // { }
            var eRightCoset1 = S3.GetCoset(S3Int.e, CosetType.Right, subgroup).Elements.ToHashSet();          // { }

            var rho1LeftCoset = S3.GetCoset(S3Int.rho1, CosetType.Left, subgroup).Elements.ToHashSet();       // { }
            var rho1RightCoset = S3.GetCoset(S3Int.rho1, CosetType.Right, subgroup).Elements.ToHashSet();     // { }

            var rho2LeftCoset = S3.GetCoset(S3Int.rho2, CosetType.Left, subgroup).Elements.ToHashSet();       // { }
            var rho2RightCoset = S3.GetCoset(S3Int.rho2, CosetType.Right, subgroup).Elements.ToHashSet();     // { }

            var sigma1LeftCoset = S3.GetCoset(S3Int.sigma1, CosetType.Left, subgroup).Elements.ToHashSet();   // { }
            var sigma1RightCoset = S3.GetCoset(S3Int.sigma1, CosetType.Right, subgroup).Elements.ToHashSet(); // { }

            var sigma2LeftCoset = S3.GetCoset(S3Int.sigma2, CosetType.Left, subgroup).Elements.ToHashSet();   // { }
            var sigma2RightCoset = S3.GetCoset(S3Int.sigma2, CosetType.Right, subgroup).Elements.ToHashSet(); // { }

            var sigma3LeftCoset = S3.GetCoset(S3Int.sigma3, CosetType.Left, subgroup).Elements.ToHashSet();   // { }
            var sigma3RightCoset = S3.GetCoset(S3Int.sigma3, CosetType.Right, subgroup).Elements.ToHashSet(); // { }

            #region Is coset equal to subgroup?
            eLeftCoset1.SetEquals(subgroup).Should().BeTrue();      // trivial
            rho1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            rho2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma2LeftCoset.SetEquals(subgroup).Should().BeTrue();  // trivial
            sigma3LeftCoset.SetEquals(subgroup).Should().BeFalse();
            #endregion

            #region Is left coset equal to its right coset?
            eLeftCoset1.SetEquals(eRightCoset1).Should().BeTrue();          // trivial
            rho1LeftCoset.SetEquals(rho1RightCoset).Should().BeFalse();     // subroup is not normal
            rho2LeftCoset.SetEquals(rho2RightCoset).Should().BeFalse();     // subroup is not normal
            sigma1LeftCoset.SetEquals(sigma1RightCoset).Should().BeFalse(); // subroup is not normal
            sigma2LeftCoset.SetEquals(sigma2RightCoset).Should().BeTrue();  // trivial
            sigma3LeftCoset.SetEquals(sigma3RightCoset).Should().BeFalse(); // subroup is not normal
            #endregion
        }

        [Fact]
        public void S3_Coset_Tests_e_sigma3()
        {
            var subgroup = new[] { S3Int.e, S3Int.sigma3 };

            var eLeftCoset1 = S3.GetCoset(S3Int.e, CosetType.Left, subgroup).Elements.ToHashSet();            // { }
            var eRightCoset1 = S3.GetCoset(S3Int.e, CosetType.Right, subgroup).Elements.ToHashSet();          // { }

            var rho1LeftCoset = S3.GetCoset(S3Int.rho1, CosetType.Left, subgroup).Elements.ToHashSet();       // { }
            var rho1RightCoset = S3.GetCoset(S3Int.rho1, CosetType.Right, subgroup).Elements.ToHashSet();     // { }

            var rho2LeftCoset = S3.GetCoset(S3Int.rho2, CosetType.Left, subgroup).Elements.ToHashSet();       // { }
            var rho2RightCoset = S3.GetCoset(S3Int.rho2, CosetType.Right, subgroup).Elements.ToHashSet();     // { }

            var sigma1LeftCoset = S3.GetCoset(S3Int.sigma1, CosetType.Left, subgroup).Elements.ToHashSet();   // { }
            var sigma1RightCoset = S3.GetCoset(S3Int.sigma1, CosetType.Right, subgroup).Elements.ToHashSet(); // { }

            var sigma2LeftCoset = S3.GetCoset(S3Int.sigma2, CosetType.Left, subgroup).Elements.ToHashSet();   // { }
            var sigma2RightCoset = S3.GetCoset(S3Int.sigma2, CosetType.Right, subgroup).Elements.ToHashSet(); // { }

            var sigma3LeftCoset = S3.GetCoset(S3Int.sigma3, CosetType.Left, subgroup).Elements.ToHashSet();   // { }
            var sigma3RightCoset = S3.GetCoset(S3Int.sigma3, CosetType.Right, subgroup).Elements.ToHashSet(); // { }

            #region Is coset equal to subgroup?
            eLeftCoset1.SetEquals(subgroup).Should().BeTrue();      // trivial
            rho1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            rho2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma1LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma2LeftCoset.SetEquals(subgroup).Should().BeFalse();
            sigma3LeftCoset.SetEquals(subgroup).Should().BeTrue();  // trivial
            #endregion

            #region Is left coset equal to its right coset?
            eLeftCoset1.SetEquals(eRightCoset1).Should().BeTrue();          // trivial
            rho1LeftCoset.SetEquals(rho1RightCoset).Should().BeFalse();     // subroup is not normal
            rho2LeftCoset.SetEquals(rho2RightCoset).Should().BeFalse();     // subroup is not normal
            sigma1LeftCoset.SetEquals(sigma1RightCoset).Should().BeFalse(); // subroup is not normal
            sigma2LeftCoset.SetEquals(sigma2RightCoset).Should().BeFalse(); // subroup is not normal
            sigma3LeftCoset.SetEquals(sigma3RightCoset).Should().BeTrue();  // trivial
            #endregion
        }

        [Fact]
        public void Klein4Group_Coset_Tests_e_a()
        {
            var subgroup = new[] { Klein4Group.e, Klein4Group.a };

            var eLeftCoset = k4g.GetCoset(Klein4Group.e, CosetType.Left, subgroup).Elements.ToHashSet();   // { e, a }
            var eRightCoset = k4g.GetCoset(Klein4Group.e, CosetType.Right, subgroup).Elements.ToHashSet(); // { e, a }
            var aLeftCoset = k4g.GetCoset(Klein4Group.a, CosetType.Left, subgroup).Elements.ToHashSet();   // { e, a }
            var aRightCoset = k4g.GetCoset(Klein4Group.a, CosetType.Right, subgroup).Elements.ToHashSet(); // { e, a }
            var bLeftCoset = k4g.GetCoset(Klein4Group.b, CosetType.Left, subgroup).Elements.ToHashSet();   // { b, c }
            var bRightCoset = k4g.GetCoset(Klein4Group.b, CosetType.Right, subgroup).Elements.ToHashSet(); // { b, c }
            var cLeftCoset = k4g.GetCoset(Klein4Group.c, CosetType.Left, subgroup).Elements.ToHashSet();   // { b, c }
            var cRightCoset = k4g.GetCoset(Klein4Group.c, CosetType.Right, subgroup).Elements.ToHashSet(); // { b, c }

            #region Is coset equal to subgroup?
            eLeftCoset.SetEquals(subgroup).Should().BeTrue();   // trivial
            aLeftCoset.SetEquals(subgroup).Should().BeTrue();   // trivial
            bLeftCoset.SetEquals(subgroup).Should().BeFalse();
            cLeftCoset.SetEquals(subgroup).Should().BeFalse();
            #endregion

            #region Is left coset equal to its right coset?
            eLeftCoset.SetEquals(eRightCoset).Should().BeTrue();    // trivial
            aLeftCoset.SetEquals(aRightCoset).Should().BeTrue();    // trivial
            bLeftCoset.SetEquals(bRightCoset).Should().BeTrue();    // subgroup is normal
            cLeftCoset.SetEquals(cRightCoset).Should().BeTrue();    // subgroup is normal
            #endregion
        }

        [Fact]
        public void Klein4Group_Coset_Tests_e_b()
        {
            var subgroup = new[] { Klein4Group.e, Klein4Group.b };

            var eLeftCoset = k4g.GetCoset(Klein4Group.e, CosetType.Left, subgroup).Elements.ToHashSet();
            var eRightCoset = k4g.GetCoset(Klein4Group.e, CosetType.Right, subgroup).Elements.ToHashSet();
            var aLeftCoset = k4g.GetCoset(Klein4Group.a, CosetType.Left, subgroup).Elements.ToHashSet();
            var aRightCoset = k4g.GetCoset(Klein4Group.a, CosetType.Right, subgroup).Elements.ToHashSet();
            var bLeftCoset = k4g.GetCoset(Klein4Group.b, CosetType.Left, subgroup).Elements.ToHashSet();
            var bRightCoset = k4g.GetCoset(Klein4Group.b, CosetType.Right, subgroup).Elements.ToHashSet();
            var cLeftCoset = k4g.GetCoset(Klein4Group.c, CosetType.Left, subgroup).Elements.ToHashSet();
            var cRightCoset = k4g.GetCoset(Klein4Group.c, CosetType.Right, subgroup).Elements.ToHashSet();

            #region Is coset equal to subgroup?
            eLeftCoset.SetEquals(subgroup).Should().BeTrue();   // trivial
            aLeftCoset.SetEquals(subgroup).Should().BeFalse();
            bLeftCoset.SetEquals(subgroup).Should().BeTrue();   // trivial
            cLeftCoset.SetEquals(subgroup).Should().BeFalse();
            #endregion

            #region Is left coset equal to its right coset?
            eLeftCoset.SetEquals(eRightCoset).Should().BeTrue();    // trivial
            aLeftCoset.SetEquals(aRightCoset).Should().BeTrue();    // subgroup is normal
            bLeftCoset.SetEquals(bRightCoset).Should().BeTrue();    // trivial
            cLeftCoset.SetEquals(cRightCoset).Should().BeTrue();    // subgroup is normal
            #endregion
        }

        [Fact]
        public void Klein4Group_Coset_Tests_e_c()
        {
            var subgroup = new[] { Klein4Group.e, Klein4Group.c };
              
            var eLeftCoset = k4g.GetCoset(Klein4Group.e, CosetType.Left, subgroup).Elements.ToHashSet();
            var eRightCoset = k4g.GetCoset(Klein4Group.e, CosetType.Right, subgroup).Elements.ToHashSet();
            var aLeftCoset = k4g.GetCoset(Klein4Group.a, CosetType.Left, subgroup).Elements.ToHashSet();
            var aRightCoset = k4g.GetCoset(Klein4Group.a, CosetType.Right, subgroup).Elements.ToHashSet();
            var bLeftCoset = k4g.GetCoset(Klein4Group.b, CosetType.Left, subgroup).Elements.ToHashSet();
            var bRightCoset = k4g.GetCoset(Klein4Group.b, CosetType.Right, subgroup).Elements.ToHashSet();
            var cLeftCoset = k4g.GetCoset(Klein4Group.c, CosetType.Left, subgroup).Elements.ToHashSet();
            var cRightCoset = k4g.GetCoset(Klein4Group.c, CosetType.Right, subgroup).Elements.ToHashSet();

            #region Is coset equal to subgroup?
            eLeftCoset.SetEquals(subgroup).Should().BeTrue();   // trivial
            aLeftCoset.SetEquals(subgroup).Should().BeFalse();
            bLeftCoset.SetEquals(subgroup).Should().BeFalse();
            cLeftCoset.SetEquals(subgroup).Should().BeTrue();   // trivial
            #endregion

            #region Is left coset equal to its right coset?
            eLeftCoset.SetEquals(eRightCoset).Should().BeTrue();    // trivial
            aLeftCoset.SetEquals(aRightCoset).Should().BeTrue();
            bLeftCoset.SetEquals(bRightCoset).Should().BeTrue();
            cLeftCoset.SetEquals(cRightCoset).Should().BeTrue();    // trivial
            #endregion
        }

        [Fact]
        public void QuotientGroup_Tests()
        {
            var normalSubgroup = new[] { S3Int.e, S3Int.rho1, S3Int.rho2 };
            var quotientGroup = S3.GetQuotientGroup(normalSubgroup);
            quotientGroup.IsValid().Should().BeTrue();
            quotientGroup.Order.Should().Be(2);
            quotientGroup.IsAbelian().Should().BeTrue();
        }
    }
}