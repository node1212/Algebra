using System.Numerics;
using Algebra.Core.Strategies;

namespace Algebra.Core
{
    public abstract class MonoidBase<TE, TS> : SemigroupBase<TE, TS>
        where TE : IEquatable<TE>
        where TS : IIdentityStrategy<TE>
    {
        public MonoidBase(CayleyTable<TE> cayleyTable, TS strategy) : base(cayleyTable, strategy) { }

        public MonoidBase(TE[] elements, TS strategy) : base(elements, strategy) { }

        public override bool IsValid => base.IsValid & HasNeutralElement;

        public virtual TE Identity => _strategy.Identity;

        private bool HasNeutralElement =>
            Elements.All(a => Op(Identity, a).Equals(a) && Op(a, Identity).Equals(a));
    }

    public class Monoid<TE>(CayleyTable<TE> cayleyTable) :
        MonoidBase<TE, IIdentityStrategy<TE>>(cayleyTable, Strategy.CayleyTableIdentity(cayleyTable))
        where TE : IAdditionOperators<TE, TE, TE>, IEquatable<TE>
    { }

    public class AdditiveMonoid<TE>(params TE[] elements) :
        MonoidBase<TE, IIdentityStrategy<TE>>(elements, Strategy.AdditiveIdentity<TE>()) where TE :
        IAdditionOperators<TE, TE, TE>, IAdditiveIdentity<TE, TE>, IEquatable<TE>
    { }

    public class MultiplicativeMonoid<TE>(params TE[] elements) :
        MonoidBase<TE, IIdentityStrategy<TE>>(elements, Strategy.MultiplicativeIdentity<TE>()) where TE :
        IMultiplyOperators<TE, TE, TE>, IMultiplicativeIdentity<TE, TE>, IEquatable<TE>
    { }
}