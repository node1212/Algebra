using System.Numerics;
using Algebra.Core.Permutations;
using Algebra.Core.Permutations.T4;

namespace Algebra.Core.Groups
{
    public static class SymmetricGroup
    {
        public static MultiplicativeGroup<TP> Create<TP>()
            where TP : IPermutationsGenerator<TP>, IMultiplyOperators<TP, TP, TP>, IMultiplicativeIdentity<TP, TP>, IInversionOperator<TP, TP>, IEquatable<TP>
        {
            return new MultiplicativeGroup<TP>(TP.Generate());
        }
    }

    public class S3Int : MultiplicativeGroup<PermutationOf3Int>
    {
        public S3Int() : base(e, rho1, rho2, sigma1, sigma2, sigma3) { }

        public static readonly PermutationOf3Int e = PermutationOf3Int.MultiplicativeIdentity;
        public static readonly PermutationOf3Int rho1 = new(2, 3, 1);   // (123)
        public static readonly PermutationOf3Int rho2 = new(3, 1, 2);   // (132)
        public static readonly PermutationOf3Int sigma1 = new(2, 1, 3); // (12)
        public static readonly PermutationOf3Int sigma2 = new(1, 3, 2); // (23)
        public static readonly PermutationOf3Int sigma3 = new(3, 2, 1); // (13)
    }

    public class S3Char : MultiplicativeGroup<PermutationOf3Char>
    {
        public S3Char() : base(e, rho1, rho2, sigma1, sigma2, sigma3) { }

        public static readonly PermutationOf3Char e = PermutationOf3Char.MultiplicativeIdentity;
        public static readonly PermutationOf3Char rho1 = new('b', 'c', 'a');
        public static readonly PermutationOf3Char rho2 = new('c', 'a', 'b');
        public static readonly PermutationOf3Char sigma1 = new('b', 'a', 'c');
        public static readonly PermutationOf3Char sigma2 = new('a', 'c', 'b');
        public static readonly PermutationOf3Char sigma3 = new('c', 'b', 'a');
    }
}