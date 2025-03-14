namespace Algebra.Core.Permutations
{
    public interface IPermutationsGenerator<TPermutation>
    {
        static abstract TPermutation[] Generate();
    }
}