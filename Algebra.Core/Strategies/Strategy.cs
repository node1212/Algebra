using System.Numerics;

namespace Algebra.Core.Strategies
{
    public static class Strategy
    {
        public static IOperationStrategy<TE> CayleyTable<TE>(CayleyTable<TE> cayleyTable)
            where TE : IEquatable<TE> =>
            new CayleyTableStrategy<TE>(cayleyTable);

        public static IOperationStrategy<TE> Additive<TE>()
            where TE : IAdditionOperators<TE, TE, TE> =>
            new AdditiveStrategy<TE>();

        public static IOperationStrategy<TE> Multiplicative<TE>()
            where TE : IMultiplyOperators<TE, TE, TE> =>
            new MultiplicativeStrategy<TE>();

        public static IIdentityStrategy<TE> CayleyTableIdentity<TE>(CayleyTable<TE> cayleyTable)
            where TE : IEquatable<TE> =>
            new CayleyTableIdentityStrategy<TE>(cayleyTable);

        public static IIdentityStrategy<TE> AdditiveIdentity<TE>()
            where TE : IAdditionOperators<TE, TE, TE>, IAdditiveIdentity<TE, TE> =>
            new AdditiveIdentityStrategy<TE>();

        public static IIdentityStrategy<TE> MultiplicativeIdentity<TE>()
            where TE : IMultiplyOperators<TE, TE, TE>, IMultiplicativeIdentity<TE, TE> =>
            new MultiplicativeIdentityStrategy<TE>();

        public static IInverseStrategy<TE> CayleyTableInverse<TE>(CayleyTable<TE> cayleyTable)
            where TE : IEquatable<TE> =>
            new CayleyTableInverseStrategy<TE>(cayleyTable);

        public static IInverseStrategy<TE> AdditiveInverse<TE>()
            where TE : IAdditionOperators<TE, TE, TE>, IAdditiveIdentity<TE, TE>, IUnaryNegationOperators<TE, TE> =>
            new AdditiveInverseStrategy<TE>();

        public static IInverseStrategy<TE> MultiplicativeInverse<TE>()
            where TE : IMultiplyOperators<TE, TE, TE>, IMultiplicativeIdentity<TE, TE>, IInversionOperator<TE, TE> =>
            new MultiplicativeInverseStrategy<TE>();
    }
}