using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Algebra.Core.Permutations
{
    public class PermutationOf2Char(params char[] elements) :
        PermutationBase<PermutationOf2Char, char>(AllowedValues, elements),
        IEqualityOperators<PermutationOf2Char, PermutationOf2Char, bool>,
        IMultiplyOperators<PermutationOf2Char, PermutationOf2Char, PermutationOf2Char>,
        IHasAllowedValues<char>,
        IMultiplicativeIdentity<PermutationOf2Char, PermutationOf2Char>,
        IInversionOperator<PermutationOf2Char, PermutationOf2Char>,
        IPermutationsGenerator<PermutationOf2Char, char>,
        IParsable<PermutationOf2Char>
    {
        public static bool operator ==(PermutationOf2Char left, PermutationOf2Char right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf2Char left, PermutationOf2Char right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf2Char operator *(PermutationOf2Char left, PermutationOf2Char right) => Multiply(left, right);

        public static char[] AllowedValues => ['a', 'b'];

        public static PermutationOf2Char MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf2Char operator ~(PermutationOf2Char value) => Inverse(value);

        public static PermutationOf2Char[] Generate() => Generate(MultiplicativeIdentity, e => new PermutationOf2Char(e));

        public static PermutationOf2Char Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToChar, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf2Char result) =>
            TryParse(s, FromCharConverter.ToChar, MultiplicativeIdentity, out result);
    }
}