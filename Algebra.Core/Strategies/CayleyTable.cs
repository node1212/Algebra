namespace Algebra.Core.Strategies
{
    public class CayleyTableStrategy<TE>(CayleyTable<TE> cayleyTable) : IOperationStrategy<TE> where TE : IEquatable<TE>
    {
        protected readonly CayleyTable<TE> _cayleyTable = cayleyTable;

        public TE Op(TE left, TE right) => _cayleyTable[left, right];
    }

    public class CayleyTableIdentityStrategy<TE>(CayleyTable<TE> cayleyTable) : CayleyTableStrategy<TE>(cayleyTable), IIdentityStrategy<TE> where TE : IEquatable<TE>
    {
        public TE Identity => _cayleyTable.Identity;
    }

    public class CayleyTableInverseStrategy<TE>(CayleyTable<TE> cayleyTable) : CayleyTableIdentityStrategy<TE>(cayleyTable), IInverseStrategy<TE> where TE : IEquatable<TE>
    {
        public TE Inverse(TE element) => _cayleyTable.GetInverse(element);
    }
}
