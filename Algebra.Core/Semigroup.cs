using System.Numerics;

namespace Algebra.Core
{
    public class Semigroup<T> : Magma<T> where T : IEquatable<T>
    {
        public Semigroup() { }

        public Semigroup(CayleyTable<T> cayleyTable) : base(cayleyTable) { }

        public override bool IsValid => IsClosed && IsAssociative;
    }

    public abstract class NonCayleySemigroupBase<T>(params T[] elements) : Semigroup<T> where T : IEquatable<T>
    {
        protected override T[] Elements => elements;
    }

    public class AdditiveSemigroup<T>(params T[] elements) : NonCayleyMagmaBase<T>(elements) where T :
        IAdditionOperators<T, T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left + right;
    }

    public class MultiplicativeSemigroup<T>(params T[] elements) : NonCayleyMagmaBase<T>(elements) where T :
        IMultiplyOperators<T, T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left * right;
    }
}