using System.Numerics;

namespace Algebra.Core
{
    public abstract class Group<T>(params T[] elements) : Monoid<T>(elements) where T :
        IEquatable<T>
    {
        protected abstract T Inverse(T element);

        public override bool IsValid => base.IsValid && HasInverseElement;

        private bool HasInverseElement
        {
            get
            {
                foreach (var a in Elements)
                {
                    if (!Elements.Contains(Inverse(a), EqualityComparer))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool IsSubgroup(params T[] elements)
        {
            for (var i = 0; i < elements.Length; i++)
            {
                for (var j = 0; j < elements.Length; j++)
                {
                    if (!elements.Contains(Op(elements[i], Inverse(elements[j])), EqualityComparer))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    public class AdditiveGroup<T>(params T[] elements) : Group<T>(elements) where T :
        IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IUnaryNegationOperators<T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left + right;

        public override T Identity => T.AdditiveIdentity;

        protected override T Inverse(T element) => -element;
    }

    public class MultiplicativeGroup<T>(params T[] elements) : Group<T>(elements) where T :
        IMultiplyOperators<T, T, T>, IMultiplicativeIdentity<T, T>, IInversionOperator<T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left * right;

        public override T Identity => T.MultiplicativeIdentity;

        protected override T Inverse(T element) => ~element;
    }
}