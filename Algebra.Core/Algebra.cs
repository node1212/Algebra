namespace Algebra.Core
{
    public abstract class Algebra<T> where T : IEquatable<T>
    {
        protected abstract HashSet<T> Elements { get; }

        protected abstract T Op(T left, T right);

        public int Order => Elements.Count;

        /// <summary>
        /// Chained: <code>Elements
        /// .SelectMany(x => Elements, (a, b) => Elements.Contains(Op(a, b)))
        /// .Always();</code>
        /// </summary>
        public bool IsClosed =>
            (from a in Elements
             from b in Elements
             select Elements.Contains(Op(a, b))).Always();

        public bool IsCommutative =>
            (from a in Elements
             from b in Elements
             select Op(a, b).Equals(Op(b, a))).Always();

        public bool IsAbelian => IsCommutative;

        /// <summary>
        /// Chained: <code>Elements
        /// .SelectMany(x => Elements, (a, b) => new { a, b })
        /// .SelectMany(x => Elements, (pair, c) => (pair.a, pair.b, c))
        /// .All(x => Op(Op(x.a, x.b), x.c).Equals(Op(x.a, Op(x.b, x.c))));</code>
        /// </summary>
        public bool IsAssociative =>
            (from a in Elements
             from b in Elements
             from c in Elements
             select (Op(Op(a, b), c).Equals(Op(a, Op(b, c))))).Always();

        public bool IsIsomorphicTo<TOther>(Algebra<TOther> other, Func<T, TOther> bijection) where TOther : IEquatable<TOther> =>
            (from a in Elements
             from b in Elements
             select bijection(Op(a, b)).Equals(other.Op(bijection(a), bijection(b)))).Always();

        public abstract bool IsValid { get; }
    }
}