using System.Numerics;
using Algebra.Core.Strategies;

namespace Algebra.Core
{
    public abstract class MagmaBase<TE, TS> : Algebra<TE>
        where TE : IEquatable<TE>
        where TS : IOperationStrategy<TE>
    {
        protected readonly CayleyTable<TE> _cayleyTable;
        protected TS _strategy;

        public MagmaBase(CayleyTable<TE> cayleyTable, TS strategy)
        {
            _strategy = strategy;
            _cayleyTable = cayleyTable;
        }

        public MagmaBase(TE[] elements, TS strategy)
        {
            _strategy = strategy;
            _cayleyTable = new CayleyTable<TE>(Op, elements);
        }

        protected override HashSet<TE> Elements => [.. _cayleyTable.Header];

        protected override TE Op(TE left, TE right) => _strategy.Op(left, right);

        public override bool IsValid => IsClosed;
    }

    public class Magma<TE>(CayleyTable<TE> cayleyTable) :
        MagmaBase<TE, IOperationStrategy<TE>>(cayleyTable, Strategy.CayleyTable(cayleyTable))
        where TE : IEquatable<TE>
    { }

    public class AdditiveMagma<TE>(params TE[] elements) :
        MagmaBase<TE, IOperationStrategy<TE>>(elements, Strategy.Additive<TE>())
        where TE : IAdditionOperators<TE, TE, TE>, IEquatable<TE>
    { }

    public class MultiplicativeMagma<TE>(params TE[] elements) :
        MagmaBase<TE, IOperationStrategy<TE>>(elements, Strategy.Multiplicative<TE>())
        where TE : IMultiplyOperators<TE, TE, TE>, IEquatable<TE>
    { }
}