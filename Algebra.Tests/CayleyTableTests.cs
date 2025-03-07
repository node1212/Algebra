using System.Text;
using Algebra.Core;
using Algebra.Core.Permutations.T4;
using FluentAssertions;

namespace Algebra.Tests
{
    public class CayleyTableTests
    {
        private static readonly CayleyTable<char> CayleyTable = new(new char[,]
        {//    a    b    c    d    e    f
                { 'f', 'd', 'a', 'e', 'b', 'c' },//a
                { 'e', 'c', 'b', 'f', 'a', 'd' },//b
                { 'a', 'b', 'c', 'd', 'e', 'f' },//c
                { 'b', 'a', 'd', 'c', 'f', 'e' },//d
                { 'd', 'f', 'e', 'a', 'c', 'b' },//e
                { 'c', 'e', 'f', 'b', 'd', 'a' },//f
            }, 'a', 'b', 'c', 'd', 'e', 'f');

        [Fact]
        public void Identity_ShouldBe_Found()
        {
            var identity = CayleyTable.GetIdentities().Single();
            identity.Type.Should().Be(NeutralElementType.TwoSided);
            CayleyTable.Identity.Should().Be('c');
        }

        [Fact]
        public void Inverses_ShouldBe_Found()
        {
            var inverses = CayleyTable.Header.ToDictionary(e => e, CayleyTable.GetInverse);
            inverses['a'].Should().Be('f');
            inverses['f'].Should().Be('a');
        }

        [Fact]
        public void CayleyTable_ShouldBe_Printable()
        {
            var permutations = PermutationOf3Int.Generate();
            var cayleyTable = new CayleyTable<PermutationOf3Int>((e1, e2) => e1 * e2, permutations);

            static string converter(string s) => s switch
            {
                "(1 2 3)" => "e ",
                "(2 3 1)" => "ρ₁",
                "(3 1 2)" => "ρ₂",
                "(2 1 3)" => "σ₁",
                "(1 3 2)" => "σ₂",
                "(3 2 1)" => "σ₃",
                _ => throw new InvalidOperationException("Should not be here"),
            };
            var sb = new StringBuilder();

            cayleyTable.Print(s => sb.Append(s), converter);

            sb.Length.Should().BeGreaterThan(0);
        }
    }
}