using System.Numerics;
using K4GE = Algebra.Core.Klein4GroupElement;

namespace Algebra.Core
{
    public readonly struct Klein4GroupElement(int value) :
        IMultiplyOperators<K4GE, K4GE, K4GE>,
        IInversionOperator<K4GE, K4GE>,
        IMultiplicativeIdentity<K4GE, K4GE>,
        IEquatable<K4GE>
    {
        private readonly int _value = value;

        private static readonly Dictionary<int, string> StringValues = new() { { 0, "e" }, { 1, "a" }, { 2, "b" }, { 3, "c" } };

        public static readonly K4GE e = new(0);
        public static readonly K4GE a = new(1);
        public static readonly K4GE b = new(2);
        public static readonly K4GE c = new(3);

        private static readonly CayleyTable<K4GE> _cayleyTable = new(new K4GE[4, 4]
        {
            { e, a, b, c },
            { a, e, c, b },
            { b, c, e, a },
            { c, b, a, e }
        }, e, a, b, c);

        public static K4GE operator *(K4GE left, K4GE right) => _cayleyTable[left, right];

        public static K4GE operator ~(K4GE value) => value;

        public static K4GE MultiplicativeIdentity => e;

        public static bool operator ==(K4GE left, K4GE right) => left.Equals(right);

        public static bool operator !=(K4GE left, K4GE right) => !(left == right);

        public override bool Equals(object obj) => (obj is K4GE element) && Equals(element);

        public bool Equals(K4GE other) => _value.Equals(other._value);

        public override int GetHashCode() => _value.GetHashCode();

        public override string ToString() => StringValues[_value];
    }

    public class Klein4Group : MultiplicativeGroup<K4GE>
    {
        public Klein4Group() : base(K4GE.e, K4GE.a, K4GE.b, K4GE.c) { }
    }
}
