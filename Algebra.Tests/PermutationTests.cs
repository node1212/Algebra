using Algebra.Core.Groups;
using Algebra.Core.Permutations.T4;
using FluentAssertions;

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
            (S3Int.rho1 * S3Int.rho1).Should().Be(S3Int.rho2);
            (S3Int.sigma1 * S3Int.rho1).Should().Be(S3Int.sigma2);
            (S3Int.sigma1 * S3Int.rho1 * S3Int.rho1).Should().Be(S3Int.sigma3);
            (S3Int.sigma1 * S3Int.sigma1).Should().Be(S3Int.e);
            (S3Int.rho1 * S3Int.sigma1).Should().Be(S3Int.sigma1 * S3Int.rho1 * S3Int.rho1);
        }

        [Fact]
        public void Parsing_Should_Work()
        {
            PermutationOf3Int.Parse("(1)(2)(3)").Should().Be(S3Int.e);      // 1 2 3
            PermutationOf3Int.Parse("(123)").Should().Be(S3Int.rho1);    // 2 3 1
            PermutationOf3Int.Parse("(132)").Should().Be(S3Int.rho2);    // 3 1 2
            PermutationOf3Int.Parse("(12)").Should().Be(S3Int.sigma1); // 2 1 3
            PermutationOf3Int.Parse("(23)").Should().Be(S3Int.sigma2); // 1 3 2
            PermutationOf3Int.Parse("(13)").Should().Be(S3Int.sigma3); // 3 2 1
        }
    }
}