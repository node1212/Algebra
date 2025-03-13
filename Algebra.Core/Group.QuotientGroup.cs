using System.Collections.Immutable;

namespace Algebra.Core
{
	partial class GroupBase<TE, TS>
	{
		public bool HasNormalSubgroup(params TE[] elements) => HasNormalSubgroup(elements.ToImmutableHashSet());

		public bool HasNormalSubgroup(GroupBase<TE, TS> candidate) => HasNormalSubgroup(candidate.Elements);

		private bool HasNormalSubgroup(ImmutableHashSet<TE> candidate) => HasSubgroup(candidate) &&
			(from g in Elements
			 from n in candidate
			 select candidate.Contains(Op(Op(g, n), Inverse(g)))).Always();

		public Coset<TE> GetCoset(TE a, CosetType type, params TE[] subgroup) => GetCoset(a, type, subgroup.ToImmutableHashSet());

		public Coset<TE> GetCoset(TE a, CosetType type, GroupBase<TE, TS> subgroup) => GetCoset(a, type, subgroup.Elements);

		private Coset<TE> GetCoset(TE a, CosetType type, ImmutableHashSet<TE> subgroup)
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
				? new(a, subgroup.Select(h => Op(a, h)))
				: new(a, subgroup.Select(h => Op(h, a)));
		}

        public int GetSubgroupIndex(params TE[] subgroup) => GetSubgroupIndex(subgroup.ToImmutableHashSet());

        public int GetSubgroupIndex(GroupBase<TE, TS> subgroup) => GetSubgroupIndex(subgroup.Elements);

        private int GetSubgroupIndex(ImmutableHashSet<TE> subgroup)
        {
            if (!HasSubgroup(subgroup))
            {
                throw new ArgumentException("Given elements are not a subgroup of this group", nameof(subgroup));
            }
            return Order / subgroup.Count;
        }

        public Group<Coset<TE>> GetQuotientGroup(params TE[] normalSubgroup) => GetQuotientGroup(normalSubgroup.ToImmutableHashSet());

		public Group<Coset<TE>> GetQuotientGroup(GroupBase<TE, TS> normalSubgroup) => GetQuotientGroup(normalSubgroup.Elements);

		private Group<Coset<TE>> GetQuotientGroup(ImmutableHashSet<TE> normalSubgroup)
		{
			if (!HasNormalSubgroup(normalSubgroup))
			{
				throw new ArgumentException("Given elements are not a normal subgroup of this group", nameof(normalSubgroup));
			}
			var cosets = Elements
				.Select(e => GetCoset(e, CosetType.Left, normalSubgroup))
				.Distinct()
				.ToArray();
			var cayleyTable = new CayleyTable<Coset<TE>>((a, b) =>
			{
				var opResult = Op(a.Element, b.Element);
				return new Coset<TE>(opResult, normalSubgroup.Select(e => Op(opResult, e)));
			}, cosets);
			return new Group<Coset<TE>>(cayleyTable);
		}
	}
}