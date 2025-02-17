namespace Algebra.Core
{
    public class CayleyTable<T> where T : IEquatable<T>
    {
        private readonly T[] _headerDirect;
        private readonly Dictionary<T, int> _headerInverse = [];
        private readonly T[,] _table;

        public CayleyTable(params T[] header)
            : this(new T[header.Length, header.Length], header) { }

        public CayleyTable(T[,] table, params T[] header)
        {
            _headerDirect = header;
            _table = table;
            for (var i = 0; i < header.Length; i++)
            {
                _headerInverse[header[i]] = i;
            }
        }

        public int Order => _headerDirect.Length;

        public void Fill(Func<T, T, T> sumOrProductFunc)
        {
            for (var i = 0; i < Order; i++)
            {
                for (var j = 0; j < Order; j++)
                {
                    _table[i, j] = sumOrProductFunc(_headerDirect[i], _headerDirect[j]);
                }
            }
        }

        public T this[T left, T right] => _table[_headerInverse[left], _headerInverse[right]];

        public void Print(Action<object> writer = null)
        {
            writer ??= Console.Write;
            for (var i = 0; i < Order; i++)
            {
                for (var j = 0; j < Order; j++)
                {
                    writer(_table[i, j]);
                    writer(' ');
                }
                writer(Environment.NewLine);
            }
        }
    }
}
