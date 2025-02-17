namespace Algebra.Core.Permutations
{
    public static class IEnumerableExtensions
    {
        public static bool HasOnlyDistinctElements<T>(this IEnumerable<T> elements) =>
            elements.Distinct().Count() == elements.Count();
    }
}
