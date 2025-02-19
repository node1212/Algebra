namespace Algebra.Core.Permutations
{
    internal static class FromCharConverter
    {
        public static Func<char, int> ToInt32 = c => int.Parse(c.ToString());
        public static Func<char, char> ToChar = c => c;
    }
}
