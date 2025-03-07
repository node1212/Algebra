namespace Algebra.Core
{
    public class CayleyTable<T> where T : IEquatable<T>
    {
        private readonly T[] _header;
        private readonly Dictionary<T, int> _headerInverse = [];
        private readonly T[,] _table;
        private readonly List<(NeutralElementType Type, T Element)> _identities;

        public CayleyTable(Func<T, T, T> func, params T[] header)
            : this(func, null, header) { }

        public CayleyTable(T[,] table, params T[] header)
            : this(null, table, header) { }

        private CayleyTable(Func<T, T, T> func, T[,] table, params T[] header)
        {
            _header = header;
            _table = table ?? (new T[_header.Length, _header.Length]);
            if (func != null)
            {
                Fill(func);
            }
            if (_header.Length != _table.GetLength(0) || _header.Length != _table.GetLength(1))
            {
                throw new ArgumentException("Table dimensions do not match header length.");
            }
            for (var i = 0; i < header.Length; i++)
            {
                _headerInverse[header[i]] = i;
            }
            _identities = [.. GetIdentities()];

            void Fill(Func<T, T, T> func)
            {
                for (var i = 0; i < Order; i++)
                {
                    for (var j = 0; j < Order; j++)
                    {
                        _table[i, j] = func(_header[i], _header[j]);
                    }
                }
            }
        }

        public T[] Header => _header;

        public int Order => _header.Length;

        #region Identity
        public IEnumerable<(NeutralElementType Type, T Element)> GetIdentities()
        {
            var identities = new List<(NeutralElementType, T)>();
            var leftIdentities = GetQuery(_table.GetRows(), NeutralElementType.Left).ToArray();
            var rightIdentities = GetQuery(_table.GetColumns(), NeutralElementType.Right).ToArray();

            switch (leftIdentities.Length, rightIdentities.Length)
            {
                case (0, 0): // it's ok in magmas and semigroups, but not in monoids and groups, no need to throw an exception
                    break;
                case ( > 0, 0): // only left identities
                    identities.AddRange(leftIdentities);
                    break;
                case (0, > 0): // only right identities
                    identities.AddRange(rightIdentities);
                    break;
                case (1, 1): // one left and one right identity = two-sided identity, not added as left or right identity
                    identities.Add((NeutralElementType.TwoSided, leftIdentities.Single().Element));
                    break;
                default: // more than one left and right identities, it's impossible
                    throw new InvalidOperationException("This is impossible, check the code");
            }

            foreach (var identity in identities)
            {
                yield return identity;
            }

            IEnumerable<(NeutralElementType Type, T Element)> GetQuery(
                IEnumerable<IEnumerable<T>> rowsOrColumns, NeutralElementType identityType) => rowsOrColumns
                    .Select((column, k) => (column, k))
                    .Where(x => x.column.SequenceEqual(_header))
                    .Select(x => (identityType, _header[x.k]));
        }

        public T Identity => _identities.Single(x => x.Type == NeutralElementType.TwoSided).Element;
        #endregion

        #region Inverse
        public IEnumerable<(InverseElementType, T)> GetInverses(T element, T neutralElement, Func<InverseElementType, bool> filter = null)
        {
            if (!_header.Contains(element))
            {
                throw new ArgumentException($"Element {element} does not belong to the table");
            }
            filter ??= _ => true;
            foreach (var probe in _header)
            {
                var isLeftInverse = this[probe, element].Equals(neutralElement);
                var isRightInverse = this[element, probe].Equals(neutralElement);
                if (isLeftInverse && isRightInverse && filter(InverseElementType.TwoSided))
                {
                    yield return (InverseElementType.TwoSided, probe);
                }
                else if (isLeftInverse && filter(InverseElementType.Left))
                {
                    yield return (InverseElementType.Left, probe);
                }
                else if (isRightInverse && filter(InverseElementType.Right))
                {
                    yield return (InverseElementType.Right, probe);
                }
            }
        }

        public IEnumerable<(InverseElementType Type, T Element)> GetInverses(T element) => GetInverses(element, Identity);

        public T GetInverse(T element) => GetInverses(element).Single(e => e.Type == InverseElementType.TwoSided).Element;
        #endregion

        public T this[T left, T right] => _table[_headerInverse[left], _headerInverse[right]];

        public void Print(Action<object> writer = null, Func<string, string> converter = null)
        {
            writer ??= Console.Write;
            converter ??= s => s;
            for (var i = 0; i < Order; i++)
            {
                for (var j = 0; j < Order; j++)
                {
                    writer(converter(_table[i, j].ToString()));
                    writer(' ');
                }
                writer(Environment.NewLine);
            }
        }
    }
}
