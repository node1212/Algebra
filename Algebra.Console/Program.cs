using Algebra.Core;
using Algebra.Core.Permutations.T4;
using OUT = System.Console;

namespace Algebra.Console
{
    internal class Program
    {
        private static void Main()
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
            OUT.WriteLine();

            foreach (var p3 in permutations)
            {
                OUT.WriteLine($"{p3.ToString("P")} = {p3.ToString("FC")}");
                OUT.WriteLine($"Even: {p3.IsEven}");
                OUT.WriteLine($"Odd: {p3.IsOdd}");
                OUT.WriteLine($"Involution: {p3.IsInvolution}");
                OUT.WriteLine($"Derangement: {p3.IsDerangement}");
                OUT.WriteLine($"Cyclic: {p3.IsCyclic}");
                OUT.WriteLine($"Strictly cyclic: {p3.IsStrictlyCyclic}");
                OUT.WriteLine();
            }
            OUT.WriteLine();

            var p4 = new PermutationOf4Char('b', 'd', 'c', 'a');
            OUT.WriteLine($"No format: {p4}");
            OUT.WriteLine($"Permutation format: {p4.ToString("P")}");
            OUT.WriteLine($"Full cycle format: {p4.ToString("FC")}");
            OUT.WriteLine($"Short cycle format: {p4.ToString("SC")}");
        }
    }
}