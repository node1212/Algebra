using System.Numerics;

namespace Algebra.Core
{
    public class Monoid<T> : Semigroup<T> where T : IEquatable<T>
    {
        public Monoid() { }

        public Monoid(CayleyTable<T> cayleyTable) : base(cayleyTable) { }

        public override bool IsValid => base.IsValid & HasNeutralElement;

        public virtual T Identity => _cayleyTable.Identity;

        private bool HasNeutralElement =>
            Elements.All(a => EqualityComparer.Equals(Op(Identity, a), a) && EqualityComparer.Equals(Op(a, Identity), a));
    }

    public abstract class NonCayleyMonoidBase<T>(params T[] elements) : Monoid<T> where T: IEquatable<T>
    {
        protected override T[] Elements => elements;
    }

    public class AdditiveMonoid<T>(params T[] elements) : NonCayleyMonoidBase<T>(elements) where T :
        IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left + right;

        public override T Identity => T.AdditiveIdentity;
    }

    public class MultiplicativeMonoid<T>(params T[] elements) : NonCayleyMonoidBase<T>(elements) where T :
        IMultiplyOperators<T, T, T>, IMultiplicativeIdentity<T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left * right;

        public override T Identity => T.MultiplicativeIdentity;
    }
}