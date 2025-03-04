using Algebra.Core;
using Algebra.Core.Permutations.T4;

namespace Algebra.Tests
{
    public class CayleyTableTests
    {
        // TODO identity tests -> new tests
        // TODO inverses tests -> new tests
        // TODO shared test data
        [Fact]
        public void CayleyTable_ShouldBe_Printable()
        {
            var ct = new CayleyTable<char>(new char[,]
            {//    a    b    c    d    e    f
                { 'f', 'd', 'a', 'e', 'b', 'c' },//a
                { 'e', 'c', 'b', 'f', 'a', 'd' },//b
                { 'a', 'b', 'c', 'd', 'e', 'f' },//c
                { 'b', 'a', 'd', 'c', 'f', 'e' },//d
                { 'd', 'f', 'e', 'a', 'c', 'b' },//e
                { 'c', 'e', 'f', 'b', 'd', 'a' },//f
            }, 'a', 'b', 'c', 'd', 'e', 'f');

            Assert.Single(ct.GetIdentities());
            Assert.Equal('c', ct.Identity);

            var inverses = ct.Header.ToDictionary(e => e, ct.GetInverse);

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
            cayleyTable.Print(converter: converter);
        }
    }
}