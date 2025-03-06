using System.Numerics;

namespace Algebra.Core.Strategies
{
    public class MultiplicativeStrategy<TE> : IOperationStrategy<TE> where TE : IMultiplyOperators<TE, TE, TE>
    {
        public TE Op(TE left, TE right) => left * right;
    }

    public class MultiplicativeIdentityStrategy<TE> : MultiplicativeStrategy<TE>, IIdentityStrategy<TE> where TE : IMultiplyOperators<TE, TE, TE>, IMultiplicativeIdentity<TE, TE>
    {
        public TE Identity => TE.MultiplicativeIdentity;
    }

    public class MultiplicativeInverseStrategy<TE> : MultiplicativeIdentityStrategy<TE>, IInverseStrategy<TE> where TE : IMultiplyOperators<TE, TE, TE>, IMultiplicativeIdentity<TE, TE>, IInversionOperator<TE, TE>
    {
        public TE Inverse(TE element) => ~element;
    }
}
