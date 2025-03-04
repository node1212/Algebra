using System.Numerics;

namespace Algebra.Core
{
    public abstract class Group<T> : Monoid<T> where T : IEquatable<T>
    {
        public Group() { }

        public Group(CayleyTable<T> cayleyTable) : base(cayleyTable) { }

        public override bool IsValid => base.IsValid && HasInverseElement;

        protected virtual T Inverse(T element) => _cayleyTable.GetInverse(element);

        private bool HasInverseElement =>
            Elements.All(a => Elements.Contains(Inverse(a), EqualityComparer));

        public bool HasSubgroup(params T[] elements) =>
            Elements.All(a => elements.Contains(Op(a, Inverse(a)), EqualityComparer));
    }

    public abstract class NonCayleyGroupBase<T>(params T[] elements) : Group<T> where T : IEquatable<T>
    {
        protected override T[] Elements => elements;
    }

    public class AdditiveGroup<T>(params T[] elements) : NonCayleyGroupBase<T>(elements) where T :
        IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IUnaryNegationOperators<T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left + right;

        public override T Identity => T.AdditiveIdentity;

        protected override T Inverse(T element) => -element;
    }

    public class MultiplicativeGroup<T>(params T[] elements) : NonCayleyGroupBase<T>(elements) where T :
        IMultiplyOperators<T, T, T>, IMultiplicativeIdentity<T, T>, IInversionOperator<T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left * right;

        public override T Identity => T.MultiplicativeIdentity;

        protected override T Inverse(T element) => ~element;
    }
}