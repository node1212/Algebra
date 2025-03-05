namespace Algebra.Core
{
    public abstract class Algebra<T> where T : IEquatable<T>
    {
        protected abstract T[] Elements { get; }

        protected abstract T Op(T left, T right);

        protected EqualityComparer<T> EqualityComparer { get; } = EqualityComparer<T>.Default;

        public int Order => Elements.Length;

        /// <summary>
        /// Chained: <code>Elements
        /// .SelectMany(x => Elements, (a, b) => Elements.Contains(Op(a, b), EqualityComparer))
        /// .All(c => c)</code>
        /// </summary>
        public bool IsClosed =>
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

        public bool IsIsomorphicTo<TOther>(Algebra<TOther> other, Func<T, TOther> bijection) where TOther : IEquatable<TOther> =>
            (from a in Elements
             from b in Elements
             select bijection(Op(a, b)).Equals(other.Op(bijection(a), bijection(b)))).All(e => e);

        public abstract bool IsValid { get; }
    }
}