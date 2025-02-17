using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Algebra.Core.Permutations
{
    public class Permutation4(params char[] elements) :
        PermutationBase<Permutation4, char>(AllowedValues, elements),
        IEqualityOperators<Permutation4, Permutation4, bool>,
        IMultiplyOperators<Permutation4, Permutation4, Permutation4>,
        IHasAllowedValues<char>,
        IMultiplicativeIdentity<Permutation4, Permutation4>,
        IInversionOperator<Permutation4, Permutation4>,
        IPermutationsGenerator<Permutation4, char>,
        IParsable<Permutation4>
    {
        public static bool operator ==(Permutation4 left, Permutation4 right) => EqualityOperator(left, right);

        public static bool operator !=(Permutation4 left, Permutation4 right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static Permutation4 operator *(Permutation4 left, Permutation4 right) => Multiply(left, right);

        public static char[] AllowedValues => ['a', 'b', 'c', 'd'];

        public static Permutation4 MultiplicativeIdentity => new(AllowedValues);

        public static Permutation4 operator ~(Permutation4 value) => Inverse(value);

        public static Permutation4[] Generate() => Generate(MultiplicativeIdentity, e => new Permutation4(e));

        public static Permutation4 Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToChar, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out Permutation4 result) =>
            TryParse(s, FromCharConverter.ToChar, MultiplicativeIdentity, out result);
    }
}