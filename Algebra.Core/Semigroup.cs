using System.Numerics;
using Algebra.Core.Strategies;

namespace Algebra.Core
{
    public abstract class SemigroupBase<TE, TS> : MagmaBase<TE, TS>
        where TE : IEquatable<TE>
        where TS : IOperationStrategy<TE>
    {
        public SemigroupBase(CayleyTable<TE> cayleyTable, TS strategy) : base(cayleyTable, strategy) { }

        public SemigroupBase(TE[] elements, TS strategy) : base(elements, strategy) { }

        public override bool IsValid() => IsClosed() && IsAssociative();
    }

    public class Semigroup<TE>(CayleyTable<TE> cayleyTable) :
        SemigroupBase<TE, IOperationStrategy<TE>>(cayleyTable, Strategy.CayleyTable(cayleyTable))
        where TE : IAdditionOperators<TE, TE, TE>, IEquatable<TE>
    { }

    public class AdditiveSemigroup<TE>(params TE[] elements) :
        SemigroupBase<TE, IOperationStrategy<TE>>(elements, Strategy.Additive<TE>())
        where TE : IAdditionOperators<TE, TE, TE>, IEquatable<TE>
    { }

    public class MultiplicativeSemigroup<TE>(params TE[] elements) :
        SemigroupBase<TE, IOperationStrategy<TE>>(elements, Strategy.Multiplicative<TE>())
        where TE : IMultiplyOperators<TE, TE, TE>, IEquatable<TE>
    { }
}