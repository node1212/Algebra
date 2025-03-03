using System.Numerics;

namespace Algebra.Core
{
    public abstract class Magma<T> where T : IEquatable<T>
    {
        public HashSet<T> Elements { get; private set; }
        public CayleyTable<T> CayleyTable { get; private set; }

        public Magma(CayleyTable<T> cayleyTable)
        {
            Elements = [.. cayleyTable.Header];
            CayleyTable = cayleyTable;
        }

        protected Magma(params T[] elements)
        {
            Elements = [.. elements];
        }

        protected virtual T Op(T left, T right) => CayleyTable[left, right];

        protected EqualityComparer<T> EqualityComparer { get; } = EqualityComparer<T>.Default;

        public int Order => Elements.Count;

        /// <summary>
        /// Chained: <code>Elements
        /// .SelectMany(x => Elements, (a, b) => Elements.Contains(Op(a, b), EqualityComparer))
        /// .All(c => c)</code>
        /// </summary>
        protected bool IsClosed =>
            (from a in Elements
             from b in Elements
             select Elements.Contains(Op(a, b), EqualityComparer)).All(c => c);

        public bool IsCommutative =>
            (from a in Elements
             from b in Elements
             select EqualityComparer.Equals(Op(a, b), Op(b, a))).All(e => e);

        public bool IsAbelian => IsCommutative;

        /// <summary>
        /// Chained: <code>Elements
        /// .SelectMany(x => Elements, (a, b) => new { a, b })
        /// .SelectMany(x => Elements, (pair, c) => (pair.a, pair.b, c))
        /// .All(x => EqualityComparer.Equals(Op(Op(x.a, x.b), x.c), Op(x.a, Op(x.b, x.c))));</code>
        /// </summary>
        public bool IsAssociative =>
            (from a in Elements
             from b in Elements
             from c in Elements
             select EqualityComparer.Equals(Op(Op(a, b), c), Op(a, Op(b, c)))).All(e => e);

        public virtual bool IsValid => IsClosed;

        public bool IsIsomorphicTo<TOther>(Magma<TOther> other, Func<T, TOther> bijection) where TOther : IEquatable<TOther> =>
            (from a in Elements
             from b in Elements
             select bijection(Op(a, b)).Equals(other.Op(bijection(a), bijection(b)))).All(e => e);
    }

    public class AdditiveMagma<T>(params T[] elements) : Magma<T>(elements) where T :
        IAdditionOperators<T, T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left + right;
    }

    public class MultiplicativeMagma<T>(params T[] elements) : Magma<T>(elements) where T :
        IMultiplyOperators<T, T, T>, IEquatable<T>
    {
        protected override T Op(T left, T right) => left * right;
    }
}
