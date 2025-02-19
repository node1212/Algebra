using Algebra.Core;
using Algebra.Core.Permutations.T4;

namespace Algebra.Tests
{
    public class CayleyTableTests
    {
        [Fact]
        public void CayleyTable_ShouldBe_Printable()
        {
            var permutations = PermutationOf3Int.Generate();
            var cayleyTable = new CayleyTable<PermutationOf3Int>(permutations);
            cayleyTable.Fill((e1, e2) => e1 * e2);
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