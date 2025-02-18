namespace Algebra.Core.Permutations
{
    internal static class FromCharConverter
    {
        public static Func<char, int> Toint = c => int.Parse(c.ToString());
        public static Func<char, char> Tochar = c => c;
    }
}
