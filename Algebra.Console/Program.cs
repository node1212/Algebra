using Algebra.Core.Permutations;
using Algebra.Core;

using OUT = System.Console;

namespace Algebra.Console
{
    internal class Program
    {
        private static void Main()
        {
            var permutations = Permutation3.Generate();
            var cayleyTable = new CayleyTable<Permutation3>(permutations);
            cayleyTable.Fill((e1, e2) => e1 * e2);
            cayleyTable.Print();
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

            //var p4 = new Permutation4char('b', 'd', 'c', 'a');
            //OUT.WriteLine($"No format: {p4}");
            //OUT.WriteLine($"Permutation format: {p4.ToString("P")}");
            //OUT.WriteLine($"Full cycle format: {p4.ToString("FC")}");
            //OUT.WriteLine($"Short cycle format: {p4.ToString("SC")}");
        }
    }
}