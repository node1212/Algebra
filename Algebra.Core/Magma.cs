using System.Numerics;

namespace Algebra.Core
{
    public abstract class Magma<T>(params T[] elements) : ClosedSet<T>(elements) where T :
        IEquatable<T>
    { }

    public class AdditiveMagma<T>(params T[] elements) : Magma<T>(elements) where T :
        IAdditionOperators<T, T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left + right;
    }

    public class MultiplicativeMagma<T>(params T[] elements) : Magma<T>(elements) where T :
        IMultiplyOperators<T, T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left * right;
    }
}
