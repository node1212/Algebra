namespace Algebra.Core.Permutations
{
    public interface IHasAllowedValues<TElement>
    {
        static TElement[] AllowedValues { get; }
    }
}
