using System.Collections.Immutable;

namespace Algebra.Core
{
    partial class GroupBase<TE, TS>
	{
		public bool HasSubgroup(params TE[] candidate) => HasSubgroup(candidate.ToImmutableHashSet());

		public bool HasSubgroup(GroupBase<TE, TS> candidate) => HasSubgroup(candidate.Elements);

		private bool HasSubgroup(ImmutableHashSet<TE> candidate)
		{
			if (!candidate.IsSubsetOf(Elements))
			{
				return false;
			}
			return (from a in candidate
					from b in candidate
					select candidate.Contains(Op(a, Inverse(b)))).Always();
		}

		public bool IsSubgroupOf(GroupBase<TE, TS> other) => other.HasSubgroup(Elements);

		public bool HasTrivialSubgroup(params TE[] elements) => HasTrivialSubgroup(elements.ToImmutableHashSet());

		public bool HasTrivialSubgroup(GroupBase<TE, TS> candidate) => HasTrivialSubgroup(candidate.Elements);

		private bool HasTrivialSubgroup(ImmutableHashSet<TE> candidate) =>
			(candidate.Count == 1 && candidate.Single().Equals(Identity)) || Elements.SetEquals(candidate);

		public IEnumerable<Group<TE>> FindNonTrivialSubgroups()
		{
			var elements = Elements.Where(e => !e.Equals(Identity));
			for (var i = 1; i < Order - 1; i++)
			{
				var combinations = elements.GetCombinationsWithIdentity(i, Identity);
				foreach (var candidate in combinations)
				{
					if (HasSubgroup(candidate))
					{
						var cayleyTable = new CayleyTable<TE>(Op, [.. candidate]);
						yield return new Group<TE>(cayleyTable);
					}
				}
			}
		}
	}
}
