namespace Algebra.Core.Groups
{
    public class Klein4Group : Group<char>
    {
        public Klein4Group() : base(
            new CayleyTable<char>(new char[,]
            {
                { 'e', 'a', 'b', 'c' },
                { 'a', 'e', 'c', 'b' },
                { 'b', 'c', 'e', 'a' },
                { 'c', 'b', 'a', 'e' }
            }, 'e', 'a', 'b', 'c'))
        { }

        public const char e = 'e';
        public const char a = 'a';
        public const char b = 'b';
        public const char c = 'c';
    }
}