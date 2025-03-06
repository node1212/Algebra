using System.Numerics;

namespace Algebra.Core.Strategies
{
    public class AdditiveStrategy<TE> : IOperationStrategy<TE> where TE : IAdditionOperators<TE, TE, TE>
    {
        public TE Op(TE left, TE right) => left + right;
    }

    public class AdditiveIdentityStrategy<TE> : AdditiveStrategy<TE>, IIdentityStrategy<TE> where TE : IAdditionOperators<TE, TE, TE>, IAdditiveIdentity<TE, TE>
    {
        public TE Identity => TE.AdditiveIdentity;
    }

    public class AdditiveInverseStrategy<TE> : AdditiveIdentityStrategy<TE>, IInverseStrategy<TE> where TE : IAdditionOperators<TE, TE, TE>, IAdditiveIdentity<TE, TE>, IUnaryNegationOperators<TE, TE>
    {
        public TE Inverse(TE element) => -element;
    }
}
