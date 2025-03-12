using System.Collections;
using System.Collections.Immutable;

namespace Algebra.Core
{
    public readonly struct Coset<T>(T element, IEnumerable<T> elements) : IEquatable<Coset<T>>
    {
        public T Element { get; } = element;
        public ImmutableHashSet<T> Elements { get; } = elements.ToImmutableHashSet();

        public bool Equals(Coset<T> other) => Elements.SetEquals(other.Elements);

        public override bool Equals(object obj) => obj is Coset<T> coset && Equals(coset);

        public override int GetHashCode() => ((IStructuralEquatable)Elements.ToArray()).GetHashCode(EqualityComparer<T>.Default);

        public static bool operator ==(Coset<T> left, Coset<T> right) => left.Equals(right);

        public static bool operator !=(Coset<T> left, Coset<T> right) => !(left == right);
    }
}