using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Algebra.Core.Permutations
{
    public class Permutation3(params int[] elements) :
        PermutationBase<Permutation3, int>(AllowedValues, elements),
        IEqualityOperators<Permutation3, Permutation3, bool>,
        IMultiplyOperators<Permutation3, Permutation3, Permutation3>,
        IHasAllowedValues<int>,
        IMultiplicativeIdentity<Permutation3, Permutation3>,
        IInversionOperator<Permutation3, Permutation3>,
        IPermutationsGenerator<Permutation3, int>,
        IParsable<Permutation3>
    {
        public static bool operator ==(Permutation3 left, Permutation3 right) => EqualityOperator(left, right);

        public static bool operator !=(Permutation3 left, Permutation3 right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static Permutation3 operator *(Permutation3 left, Permutation3 right) => Multiply(left, right);

        public static int[] AllowedValues => [1, 2, 3];

        public static Permutation3 MultiplicativeIdentity => new(AllowedValues);

        public static Permutation3 operator ~(Permutation3 value) => Inverse(value);

        public static Permutation3[] Generate() => Generate(MultiplicativeIdentity, e => new Permutation3(e));

        public static Permutation3 Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToInt, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out Permutation3 result) =>
            TryParse(s, FromCharConverter.ToInt, MultiplicativeIdentity, out result);

        public override string ToString() => base.ToString() switch
        {
            "(1 2 3)" => "e ",
            "(2 3 1)" => "ρ₁",
            "(3 1 2)" => "ρ₂",
            "(2 1 3)" => "σ₁",
            "(1 3 2)" => "σ₂",
            "(3 2 1)" => "σ₃",
            _ => throw new InvalidOperationException("Should not be here"),
        };
    }
}