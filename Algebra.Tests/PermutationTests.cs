using Algebra.Core.Permutations.T4;

namespace Algebra.Tests
{
    public class PermutationTests
    {
        [Fact]
        public void PermutationProperties_ShouldBe_Printable()
        {
            var permutations = PermutationOf3Char.Generate();
            foreach (var p3 in permutations)
            {
                Console.WriteLine($"{p3.ToString("P")} = {p3.ToString("FC")}");
                Console.WriteLine($"Even: {p3.IsEven}");
                Console.WriteLine($"Odd: {p3.IsOdd}");
                Console.WriteLine($"Involution: {p3.IsInvolution}");
                Console.WriteLine($"Derangement: {p3.IsDerangement}");
                Console.WriteLine($"Cyclic: {p3.IsCyclic}");
                Console.WriteLine($"Strictly cyclic: {p3.IsStrictlyCyclic}");
                Console.WriteLine($"Transposition: {p3.IsTransposition}");
                Console.WriteLine();
            }
        }

        [Fact]
        public void Permutation_ShouldBe_Formattable()
        {
            var p4 = new PermutationOf4Char('b', 'd', 'c', 'a');
            Console.WriteLine($"No format: {p4}");
            Console.WriteLine($"Permutation format: {p4.ToString("P")}");
            Console.WriteLine($"Full cycle format: {p4.ToString("FC")}");
            Console.WriteLine($"Short cycle format: {p4.ToString("SC")}");
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
            var rho2 = PermutationOf3Int.Parse("(132)").ToString();  // 3 1 2
            var sigma1 = PermutationOf3Int.Parse("(12)").ToString(); // 2 1 3
            var sigma2 = PermutationOf3Int.Parse("(23)").ToString(); // 1 3 2
            var sigma3 = PermutationOf3Int.Parse("(13)").ToString(); // 3 2 1

            Assert.Equal("(1 2 3)", e);
            Assert.Equal("(2 3 1)", rho1);
            Assert.Equal("(3 1 2)", rho2);
            Assert.Equal("(2 1 3)", sigma1);
            Assert.Equal("(1 3 2)", sigma2);
            Assert.Equal("(3 2 1)", sigma3);
        }
    }
}