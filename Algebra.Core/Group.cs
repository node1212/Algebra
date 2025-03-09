using System.Collections.Immutable;
using System.Numerics;
using Algebra.Core.Strategies;

namespace Algebra.Core
{
    public abstract class GroupBase<TE, TS> : MonoidBase<TE, TS>
        where TE : IEquatable<TE>
        where TS : IInverseStrategy<TE>
    {
        public GroupBase(CayleyTable<TE> cayleyTable, TS strategy) : base(cayleyTable, strategy) { }

        public GroupBase(TE[] elements, TS strategy) : base(elements, strategy) { }

        public override bool IsValid() => base.IsValid() && HasInverseElement();

        protected virtual TE Inverse(TE element) => _cayleyTable.GetInverse(element);

        private bool HasInverseElement() =>
            Elements.All(a => Elements.Contains(Inverse(a)));

        public bool HasSubgroup(params TE[] elements) => HasSubgroup(elements.ToImmutableHashSet());

        private bool HasSubgroup(ImmutableHashSet<TE> elements)
        {
            if (!elements.IsSubsetOf(Elements))
            {
                return false;
            }
            return (from a in elements
                    from b in elements
                    select elements.Contains(Op(a, Inverse(b)))).Always();
        }

        public bool IsSubgroupOf(GroupBase<TE, TS> other) => other.HasSubgroup(Elements);

        public bool HasTrivialSubgroup(params TE[] elements) =>
            elements.SetEquals([Identity]) || elements.SetEquals(Elements);

        public IEnumerable<TE> GetCoset(TE a, CosetType type, params TE[] subgroup)
        {
            if (!Elements.Contains(a))
            {
                throw new ArgumentException($"Element {a} does not belong to the group", nameof(a));
            }
            if (!HasSubgroup(subgroup))
            {
                throw new ArgumentException("Given elements are not a subgroup of this group", nameof(subgroup));
            }
            return type == CosetType.Left
                ? subgroup.Select(h => Op(a, h))
                : subgroup.Select(h => Op(h, a));
        }

        public IEnumerable<IEnumerable<TE>> FindNonTrivialSubgroups()
        {
            var elements = Elements.Where(e => !e.Equals(Identity));
            for (var i = 1; i < Order - 1; i++)
            {
                var combinations = elements.GetCombinations(i);
                foreach (var combination in combinations)
                {
                    var candidate = new List<TE>(i + 1) { Identity };
                    candidate.AddRange(combination);
                    if (HasSubgroup(candidate.ToImmutableHashSet()))
                    {
                        yield return candidate;
                    }
                }
            }
        }

        // TODO: normal subgroup, factor group
    }

    public class Group<TE>(CayleyTable<TE> cayleyTable) :
        GroupBase<TE, IInverseStrategy<TE>>(cayleyTable, Strategy.CayleyTableInverse(cayleyTable))
        where TE : IAdditionOperators<TE, TE, TE>, IAdditiveIdentity<TE, TE>, IUnaryNegationOperators<TE, TE>, IEquatable<TE>
    { }

    public class AdditiveGroup<TE>(params TE[] elements) :
        GroupBase<TE, IInverseStrategy<TE>>(elements, Strategy.AdditiveInverse<TE>())
        where TE : IAdditionOperators<TE, TE, TE>, IAdditiveIdentity<TE, TE>, IUnaryNegationOperators<TE, TE>, IEquatable<TE>
    { }

    public class MultiplicativeGroup<TE>(params TE[] elements) :
        GroupBase<TE, IInverseStrategy<TE>>(elements, Strategy.MultiplicativeInverse<TE>())
        where TE : IMultiplyOperators<TE, TE, TE>, IMultiplicativeIdentity<TE, TE>, IInversionOperator<TE, TE>, IEquatable<TE>
    { }
}