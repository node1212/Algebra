namespace Algebra.Core
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
    }
}