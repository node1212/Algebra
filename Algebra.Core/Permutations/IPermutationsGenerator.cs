namespace Algebra.Core.Permutations
{
    public interface IPermutationsGenerator<TPermutation, TElement>
        where TPermutation : PermutationBase<TPermutation, TElement>
        where TElement : IEquatable<TElement>, IComparable<TElement>
    {
        static abstract TPermutation[] Generate();
    }
}