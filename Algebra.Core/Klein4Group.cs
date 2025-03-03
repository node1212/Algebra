namespace Algebra.Core
{
    public class Klein4Group : Group<char>
    {
        private static readonly CayleyTable<char> _cayleyTable = new(new char[,]
        {
            { 'e', 'a', 'b', 'c' },
            { 'a', 'e', 'c', 'b' },
            { 'b', 'c', 'e', 'a' },
            { 'c', 'b', 'a', 'e' }
        }, 'e', 'a', 'b', 'c');

        public Klein4Group() : base(_cayleyTable) { }
    }
}