using System.Numerics;
using Algebra.Core.Strategies;

namespace Algebra.Core
{
    public abstract partial class GroupBase<TE, TS> : MonoidBase<TE, TS>
        where TE : IEquatable<TE>
        where TS : IInverseStrategy<TE>
    {
        public GroupBase(CayleyTable<TE> cayleyTable, TS strategy) : base(cayleyTable, strategy) { }

        public GroupBase(TE[] elements, TS strategy) : base(elements, strategy) { }

        public override bool IsValid() => base.IsValid() && HasInverseElement();

        protected virtual TE Inverse(TE element) => _cayleyTable.GetInverse(element);

        private bool HasInverseElement() =>
            Elements.All(a => Elements.Contains(Inverse(a)));
    }

    public class Group<TE>(CayleyTable<TE> cayleyTable) :
        GroupBase<TE, IInverseStrategy<TE>>(cayleyTable, Strategy.CayleyTableInverse(cayleyTable))
        where TE : IEquatable<TE>
    { }

    public class AdditiveGroup<TE>(params TE[] elements) :
        GroupBase<TE, IInverseStrategy<TE>>(elements, Strategy.AdditiveInverse<TE>())
        where TE : IAdditionOperators<TE, TE, TE>, IAdditiveIdentity<TE, TE>, IUnaryNegationOperators<TE, TE>, IEquatable<TE>
    { }

    public class MultiplicativeGroup<TE>(params TE[] elements) :
        GroupBase<TE, IInverseStrategy<TE>>(elements, Strategy.MultiplicativeInverse<TE>())
        where TE : IMultiplyOperators<TE, TE, TE>, IMultiplicativeIdentity<TE, TE>, IInversionOperator<TE, TE>, IEquatable<TE>
    { }
}