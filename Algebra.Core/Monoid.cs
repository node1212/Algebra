using System.Numerics;

namespace Algebra.Core
{
    public abstract class Monoid<T>(params T[] elements) : Semigroup<T>(elements) where T :
        IEquatable<T>
    {
        public abstract T Identity { get; }

        public override bool IsValid => base.IsValid & HasNeutralElement;

        private bool HasNeutralElement
        {
            get
            {
                foreach (var a in Elements)
                {
                    if (!EqualityComparer.Equals(Op(a, Identity), a) || !EqualityComparer.Equals(Op(Identity, a), a))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }

    public class AdditiveMonoid<T>(params T[] elements) : Monoid<T>(elements) where T :
        IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left + right;

        public override T Identity => T.AdditiveIdentity;
    }

    public class MultiplicativeMonoid<T>(params T[] elements) : Monoid<T>(elements) where T :
        IMultiplyOperators<T, T, T>, IMultiplicativeIdentity<T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left * right;

        public override T Identity => T.MultiplicativeIdentity;
    }
}