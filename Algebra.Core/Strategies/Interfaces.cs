namespace Algebra.Core.Strategies
{
    public interface IOperationStrategy<TE>
    {
        TE Op(TE left, TE right);
    }

    public interface IIdentityStrategy<TE> : IOperationStrategy<TE>
    {
        TE Identity { get; }
    }

    public interface IInverseStrategy<TE> : IIdentityStrategy<TE>
    {
        TE Inverse(TE element);
    }
}
