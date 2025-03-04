using System.Numerics;

namespace Algebra.Core
{
    public class Semigroup<T> : Magma<T> where T : IEquatable<T>
    {
        public Semigroup(CayleyTable<T> cayleyTable) : base(cayleyTable) { }

        public Semigroup(T[] elements) : base(elements) { }

        public override bool IsValid => IsClosed && IsAssociative;
    }

    public class AdditiveSemigroup<T>(params T[] elements) : Semigroup<T>(elements) where T :
        IAdditionOperators<T, T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left + right;
    }

    public class MultiplicativeSemigroup<T>(params T[] elements) : Semigroup<T>(elements) where T :
        IMultiplyOperators<T, T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left * right;
    }
}