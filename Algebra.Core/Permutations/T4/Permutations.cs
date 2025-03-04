using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Algebra.Core.Permutations.T4
{
    public class PermutationOf3Int(params int[] elements) :
        PermutationBase<PermutationOf3Int, int>(AllowedValues, elements),
        IEqualityOperators<PermutationOf3Int, PermutationOf3Int, bool>,
        IMultiplyOperators<PermutationOf3Int, PermutationOf3Int, PermutationOf3Int>,
        IHasAllowedValues<int>,
        IMultiplicativeIdentity<PermutationOf3Int, PermutationOf3Int>,
        IInversionOperator<PermutationOf3Int, PermutationOf3Int>,
        IPermutationsGenerator<PermutationOf3Int, int>,
        IParsable<PermutationOf3Int>
    {
        public static bool operator ==(PermutationOf3Int left, PermutationOf3Int right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf3Int left, PermutationOf3Int right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf3Int operator *(PermutationOf3Int left, PermutationOf3Int right) => Multiply(left, right);

        public static int[] AllowedValues => [1, 2, 3];

        public static PermutationOf3Int MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf3Int operator ~(PermutationOf3Int value) => Inverse(value);

        public static PermutationOf3Int[] Generate() => Generate(MultiplicativeIdentity, e => new PermutationOf3Int(e));

        public static PermutationOf3Int Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToInt32, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf3Int result) =>
            TryParse(s, FromCharConverter.ToInt32, MultiplicativeIdentity, out result);
    }

    public class PermutationOf4Int(params int[] elements) :
        PermutationBase<PermutationOf4Int, int>(AllowedValues, elements),
        IEqualityOperators<PermutationOf4Int, PermutationOf4Int, bool>,
        IMultiplyOperators<PermutationOf4Int, PermutationOf4Int, PermutationOf4Int>,
        IHasAllowedValues<int>,
        IMultiplicativeIdentity<PermutationOf4Int, PermutationOf4Int>,
        IInversionOperator<PermutationOf4Int, PermutationOf4Int>,
        IPermutationsGenerator<PermutationOf4Int, int>,
        IParsable<PermutationOf4Int>
    {
        public static bool operator ==(PermutationOf4Int left, PermutationOf4Int right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf4Int left, PermutationOf4Int right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf4Int operator *(PermutationOf4Int left, PermutationOf4Int right) => Multiply(left, right);

        public static int[] AllowedValues => [1, 2, 3, 4];

        public static PermutationOf4Int MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf4Int operator ~(PermutationOf4Int value) => Inverse(value);

        public static PermutationOf4Int[] Generate() => Generate(MultiplicativeIdentity, e => new PermutationOf4Int(e));

        public static PermutationOf4Int Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToInt32, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf4Int result) =>
            TryParse(s, FromCharConverter.ToInt32, MultiplicativeIdentity, out result);
    }

    public class PermutationOf5Int(params int[] elements) :
        PermutationBase<PermutationOf5Int, int>(AllowedValues, elements),
        IEqualityOperators<PermutationOf5Int, PermutationOf5Int, bool>,
        IMultiplyOperators<PermutationOf5Int, PermutationOf5Int, PermutationOf5Int>,
        IHasAllowedValues<int>,
        IMultiplicativeIdentity<PermutationOf5Int, PermutationOf5Int>,
        IInversionOperator<PermutationOf5Int, PermutationOf5Int>,
        IPermutationsGenerator<PermutationOf5Int, int>,
        IParsable<PermutationOf5Int>
    {
        public static bool operator ==(PermutationOf5Int left, PermutationOf5Int right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf5Int left, PermutationOf5Int right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf5Int operator *(PermutationOf5Int left, PermutationOf5Int right) => Multiply(left, right);

        public static int[] AllowedValues => [1, 2, 3, 4, 5];

        public static PermutationOf5Int MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf5Int operator ~(PermutationOf5Int value) => Inverse(value);

        public static PermutationOf5Int[] Generate() => Generate(MultiplicativeIdentity, e => new PermutationOf5Int(e));

        public static PermutationOf5Int Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToInt32, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf5Int result) =>
            TryParse(s, FromCharConverter.ToInt32, MultiplicativeIdentity, out result);
    }

    public class PermutationOf6Int(params int[] elements) :
        PermutationBase<PermutationOf6Int, int>(AllowedValues, elements),
        IEqualityOperators<PermutationOf6Int, PermutationOf6Int, bool>,
        IMultiplyOperators<PermutationOf6Int, PermutationOf6Int, PermutationOf6Int>,
        IHasAllowedValues<int>,
        IMultiplicativeIdentity<PermutationOf6Int, PermutationOf6Int>,
        IInversionOperator<PermutationOf6Int, PermutationOf6Int>,
        IParsable<PermutationOf6Int>
    {
        public static bool operator ==(PermutationOf6Int left, PermutationOf6Int right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf6Int left, PermutationOf6Int right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf6Int operator *(PermutationOf6Int left, PermutationOf6Int right) => Multiply(left, right);

        public static int[] AllowedValues => [1, 2, 3, 4, 5, 6];

        public static PermutationOf6Int MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf6Int operator ~(PermutationOf6Int value) => Inverse(value);

        public static PermutationOf6Int Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToInt32, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf6Int result) =>
            TryParse(s, FromCharConverter.ToInt32, MultiplicativeIdentity, out result);
    }

    public class PermutationOf7Int(params int[] elements) :
        PermutationBase<PermutationOf7Int, int>(AllowedValues, elements),
        IEqualityOperators<PermutationOf7Int, PermutationOf7Int, bool>,
        IMultiplyOperators<PermutationOf7Int, PermutationOf7Int, PermutationOf7Int>,
        IHasAllowedValues<int>,
        IMultiplicativeIdentity<PermutationOf7Int, PermutationOf7Int>,
        IInversionOperator<PermutationOf7Int, PermutationOf7Int>,
        IParsable<PermutationOf7Int>
    {
        public static bool operator ==(PermutationOf7Int left, PermutationOf7Int right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf7Int left, PermutationOf7Int right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf7Int operator *(PermutationOf7Int left, PermutationOf7Int right) => Multiply(left, right);

        public static int[] AllowedValues => [1, 2, 3, 4, 5, 6, 7];

        public static PermutationOf7Int MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf7Int operator ~(PermutationOf7Int value) => Inverse(value);

        public static PermutationOf7Int Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToInt32, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf7Int result) =>
            TryParse(s, FromCharConverter.ToInt32, MultiplicativeIdentity, out result);
    }

    public class PermutationOf8Int(params int[] elements) :
        PermutationBase<PermutationOf8Int, int>(AllowedValues, elements),
        IEqualityOperators<PermutationOf8Int, PermutationOf8Int, bool>,
        IMultiplyOperators<PermutationOf8Int, PermutationOf8Int, PermutationOf8Int>,
        IHasAllowedValues<int>,
        IMultiplicativeIdentity<PermutationOf8Int, PermutationOf8Int>,
        IInversionOperator<PermutationOf8Int, PermutationOf8Int>,
        IParsable<PermutationOf8Int>
    {
        public static bool operator ==(PermutationOf8Int left, PermutationOf8Int right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf8Int left, PermutationOf8Int right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf8Int operator *(PermutationOf8Int left, PermutationOf8Int right) => Multiply(left, right);

        public static int[] AllowedValues => [1, 2, 3, 4, 5, 6, 7, 8];

        public static PermutationOf8Int MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf8Int operator ~(PermutationOf8Int value) => Inverse(value);

        public static PermutationOf8Int Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToInt32, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf8Int result) =>
            TryParse(s, FromCharConverter.ToInt32, MultiplicativeIdentity, out result);
    }

    public class PermutationOf9Int(params int[] elements) :
        PermutationBase<PermutationOf9Int, int>(AllowedValues, elements),
        IEqualityOperators<PermutationOf9Int, PermutationOf9Int, bool>,
        IMultiplyOperators<PermutationOf9Int, PermutationOf9Int, PermutationOf9Int>,
        IHasAllowedValues<int>,
        IMultiplicativeIdentity<PermutationOf9Int, PermutationOf9Int>,
        IInversionOperator<PermutationOf9Int, PermutationOf9Int>,
        IParsable<PermutationOf9Int>
    {
        public static bool operator ==(PermutationOf9Int left, PermutationOf9Int right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf9Int left, PermutationOf9Int right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf9Int operator *(PermutationOf9Int left, PermutationOf9Int right) => Multiply(left, right);

        public static int[] AllowedValues => [1, 2, 3, 4, 5, 6, 7, 8, 9];

        public static PermutationOf9Int MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf9Int operator ~(PermutationOf9Int value) => Inverse(value);

        public static PermutationOf9Int Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToInt32, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf9Int result) =>
            TryParse(s, FromCharConverter.ToInt32, MultiplicativeIdentity, out result);
    }

    public class PermutationOf3Char(params char[] elements) :
        PermutationBase<PermutationOf3Char, char>(AllowedValues, elements),
        IEqualityOperators<PermutationOf3Char, PermutationOf3Char, bool>,
        IMultiplyOperators<PermutationOf3Char, PermutationOf3Char, PermutationOf3Char>,
        IHasAllowedValues<char>,
        IMultiplicativeIdentity<PermutationOf3Char, PermutationOf3Char>,
        IInversionOperator<PermutationOf3Char, PermutationOf3Char>,
        IPermutationsGenerator<PermutationOf3Char, char>,
        IParsable<PermutationOf3Char>
    {
        public static bool operator ==(PermutationOf3Char left, PermutationOf3Char right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf3Char left, PermutationOf3Char right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf3Char operator *(PermutationOf3Char left, PermutationOf3Char right) => Multiply(left, right);

        public static char[] AllowedValues => ['a', 'b', 'c'];

        public static PermutationOf3Char MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf3Char operator ~(PermutationOf3Char value) => Inverse(value);

        public static PermutationOf3Char[] Generate() => Generate(MultiplicativeIdentity, e => new PermutationOf3Char(e));

        public static PermutationOf3Char Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToChar, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf3Char result) =>
            TryParse(s, FromCharConverter.ToChar, MultiplicativeIdentity, out result);
    }

    public class PermutationOf4Char(params char[] elements) :
        PermutationBase<PermutationOf4Char, char>(AllowedValues, elements),
        IEqualityOperators<PermutationOf4Char, PermutationOf4Char, bool>,
        IMultiplyOperators<PermutationOf4Char, PermutationOf4Char, PermutationOf4Char>,
        IHasAllowedValues<char>,
        IMultiplicativeIdentity<PermutationOf4Char, PermutationOf4Char>,
        IInversionOperator<PermutationOf4Char, PermutationOf4Char>,
        IPermutationsGenerator<PermutationOf4Char, char>,
        IParsable<PermutationOf4Char>
    {
        public static bool operator ==(PermutationOf4Char left, PermutationOf4Char right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf4Char left, PermutationOf4Char right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf4Char operator *(PermutationOf4Char left, PermutationOf4Char right) => Multiply(left, right);

        public static char[] AllowedValues => ['a', 'b', 'c', 'd'];

        public static PermutationOf4Char MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf4Char operator ~(PermutationOf4Char value) => Inverse(value);

        public static PermutationOf4Char[] Generate() => Generate(MultiplicativeIdentity, e => new PermutationOf4Char(e));

        public static PermutationOf4Char Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToChar, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf4Char result) =>
            TryParse(s, FromCharConverter.ToChar, MultiplicativeIdentity, out result);
    }

    public class PermutationOf5Char(params char[] elements) :
        PermutationBase<PermutationOf5Char, char>(AllowedValues, elements),
        IEqualityOperators<PermutationOf5Char, PermutationOf5Char, bool>,
        IMultiplyOperators<PermutationOf5Char, PermutationOf5Char, PermutationOf5Char>,
        IHasAllowedValues<char>,
        IMultiplicativeIdentity<PermutationOf5Char, PermutationOf5Char>,
        IInversionOperator<PermutationOf5Char, PermutationOf5Char>,
        IPermutationsGenerator<PermutationOf5Char, char>,
        IParsable<PermutationOf5Char>
    {
        public static bool operator ==(PermutationOf5Char left, PermutationOf5Char right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf5Char left, PermutationOf5Char right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf5Char operator *(PermutationOf5Char left, PermutationOf5Char right) => Multiply(left, right);

        public static char[] AllowedValues => ['a', 'b', 'c', 'd', 'e'];

        public static PermutationOf5Char MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf5Char operator ~(PermutationOf5Char value) => Inverse(value);

        public static PermutationOf5Char[] Generate() => Generate(MultiplicativeIdentity, e => new PermutationOf5Char(e));

        public static PermutationOf5Char Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToChar, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf5Char result) =>
            TryParse(s, FromCharConverter.ToChar, MultiplicativeIdentity, out result);
    }

    public class PermutationOf6Char(params char[] elements) :
        PermutationBase<PermutationOf6Char, char>(AllowedValues, elements),
        IEqualityOperators<PermutationOf6Char, PermutationOf6Char, bool>,
        IMultiplyOperators<PermutationOf6Char, PermutationOf6Char, PermutationOf6Char>,
        IHasAllowedValues<char>,
        IMultiplicativeIdentity<PermutationOf6Char, PermutationOf6Char>,
        IInversionOperator<PermutationOf6Char, PermutationOf6Char>,
        IParsable<PermutationOf6Char>
    {
        public static bool operator ==(PermutationOf6Char left, PermutationOf6Char right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf6Char left, PermutationOf6Char right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf6Char operator *(PermutationOf6Char left, PermutationOf6Char right) => Multiply(left, right);

        public static char[] AllowedValues => ['a', 'b', 'c', 'd', 'e', 'f'];

        public static PermutationOf6Char MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf6Char operator ~(PermutationOf6Char value) => Inverse(value);

        public static PermutationOf6Char Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToChar, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf6Char result) =>
            TryParse(s, FromCharConverter.ToChar, MultiplicativeIdentity, out result);
    }

    public class PermutationOf7Char(params char[] elements) :
        PermutationBase<PermutationOf7Char, char>(AllowedValues, elements),
        IEqualityOperators<PermutationOf7Char, PermutationOf7Char, bool>,
        IMultiplyOperators<PermutationOf7Char, PermutationOf7Char, PermutationOf7Char>,
        IHasAllowedValues<char>,
        IMultiplicativeIdentity<PermutationOf7Char, PermutationOf7Char>,
        IInversionOperator<PermutationOf7Char, PermutationOf7Char>,
        IParsable<PermutationOf7Char>
    {
        public static bool operator ==(PermutationOf7Char left, PermutationOf7Char right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf7Char left, PermutationOf7Char right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf7Char operator *(PermutationOf7Char left, PermutationOf7Char right) => Multiply(left, right);

        public static char[] AllowedValues => ['a', 'b', 'c', 'd', 'e', 'f', 'g'];

        public static PermutationOf7Char MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf7Char operator ~(PermutationOf7Char value) => Inverse(value);

        public static PermutationOf7Char Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToChar, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf7Char result) =>
            TryParse(s, FromCharConverter.ToChar, MultiplicativeIdentity, out result);
    }

    public class PermutationOf8Char(params char[] elements) :
        PermutationBase<PermutationOf8Char, char>(AllowedValues, elements),
        IEqualityOperators<PermutationOf8Char, PermutationOf8Char, bool>,
        IMultiplyOperators<PermutationOf8Char, PermutationOf8Char, PermutationOf8Char>,
        IHasAllowedValues<char>,
        IMultiplicativeIdentity<PermutationOf8Char, PermutationOf8Char>,
        IInversionOperator<PermutationOf8Char, PermutationOf8Char>,
        IParsable<PermutationOf8Char>
    {
        public static bool operator ==(PermutationOf8Char left, PermutationOf8Char right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf8Char left, PermutationOf8Char right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf8Char operator *(PermutationOf8Char left, PermutationOf8Char right) => Multiply(left, right);

        public static char[] AllowedValues => ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];

        public static PermutationOf8Char MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf8Char operator ~(PermutationOf8Char value) => Inverse(value);

        public static PermutationOf8Char Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToChar, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf8Char result) =>
            TryParse(s, FromCharConverter.ToChar, MultiplicativeIdentity, out result);
    }

    public class PermutationOf9Char(params char[] elements) :
        PermutationBase<PermutationOf9Char, char>(AllowedValues, elements),
        IEqualityOperators<PermutationOf9Char, PermutationOf9Char, bool>,
        IMultiplyOperators<PermutationOf9Char, PermutationOf9Char, PermutationOf9Char>,
        IHasAllowedValues<char>,
        IMultiplicativeIdentity<PermutationOf9Char, PermutationOf9Char>,
        IInversionOperator<PermutationOf9Char, PermutationOf9Char>,
        IParsable<PermutationOf9Char>
    {
        public static bool operator ==(PermutationOf9Char left, PermutationOf9Char right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf9Char left, PermutationOf9Char right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf9Char operator *(PermutationOf9Char left, PermutationOf9Char right) => Multiply(left, right);

        public static char[] AllowedValues => ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i'];

        public static PermutationOf9Char MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf9Char operator ~(PermutationOf9Char value) => Inverse(value);

        public static PermutationOf9Char Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToChar, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf9Char result) =>
            TryParse(s, FromCharConverter.ToChar, MultiplicativeIdentity, out result);
    }

    public class PermutationOf10Char(params char[] elements) :
        PermutationBase<PermutationOf10Char, char>(AllowedValues, elements),
        IEqualityOperators<PermutationOf10Char, PermutationOf10Char, bool>,
        IMultiplyOperators<PermutationOf10Char, PermutationOf10Char, PermutationOf10Char>,
        IHasAllowedValues<char>,
        IMultiplicativeIdentity<PermutationOf10Char, PermutationOf10Char>,
        IInversionOperator<PermutationOf10Char, PermutationOf10Char>,
        IParsable<PermutationOf10Char>
    {
        public static bool operator ==(PermutationOf10Char left, PermutationOf10Char right) => EqualityOperator(left, right);

        public static bool operator !=(PermutationOf10Char left, PermutationOf10Char right) => NonEqualityOperator(left, right);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static PermutationOf10Char operator *(PermutationOf10Char left, PermutationOf10Char right) => Multiply(left, right);

        public static char[] AllowedValues => ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j'];

        public static PermutationOf10Char MultiplicativeIdentity => new(AllowedValues);

        public static PermutationOf10Char operator ~(PermutationOf10Char value) => Inverse(value);

        public static PermutationOf10Char Parse(string s, IFormatProvider provider = null) =>
            Parse(s, FromCharConverter.ToChar, MultiplicativeIdentity);

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out PermutationOf10Char result) =>
            TryParse(s, FromCharConverter.ToChar, MultiplicativeIdentity, out result);
    }

}