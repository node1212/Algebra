using System.Numerics;

namespace Algebra.Core
{
    public abstract class Semigroup<T>(params T[] elements) : Magma<T>(elements) where T:
        IEquatable<T>
    {
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