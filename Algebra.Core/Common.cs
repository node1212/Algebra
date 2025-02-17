namespace Algebra.Core
{
    public interface IInversionOperator<TSelf, TResult>
        where TSelf : IInversionOperator<TSelf, TResult>
    {
        static abstract TResult operator ~(TSelf value);
    }
}