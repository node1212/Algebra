using System.Collections;
using System.Text.RegularExpressions;

namespace Algebra.Core.Permutations
{
    public abstract partial class PermutationBase<TPermutation, TElement>
        : IEquatable<TPermutation>, IFormattable
            where TPermutation : PermutationBase<TPermutation, TElement>
            where TElement : IEquatable<TElement>, IComparable<TElement>
    {
        #region CTOR & verification
        protected PermutationBase(TElement[] allowedValues, TElement[] elements)
        {
            Verify(allowedValues, elements);
            Array.Sort(allowedValues);
            for (var i = 0; i < allowedValues.Length; i++)
            {
                Elements[allowedValues[i]] = elements[i];
            }
            Length = allowedValues.Length;
        }

        private static void Verify(TElement[] allowedValues, TElement[] elements)
        {
            if (allowedValues.Length != elements.Length)
            {
                throw new ArgumentException($"{nameof(allowedValues)} and {nameof(elements)} must have the same length");
            }
            if (!allowedValues.HasOnlyDistinctElements())
            {
                throw new ArgumentException($"{nameof(allowedValues)} are not distinct");
            }
            if (!elements.HasOnlyDistinctElements())
            {
                throw new ArgumentException($"{nameof(elements)} are not distinct");
            }
        }
        #endregion

        protected SortedDictionary<TElement, TElement> Elements { get; private set; } = [];

        #region Public properties
        public int Length { get; private set; }

        public bool IsIdentity => Elements.All(kvp => kvp.Key.Equals(kvp.Value));

        public bool IsInvolution => Multiply((TPermutation)this, (TPermutation)this).IsIdentity;

        public bool IsDerangement => Support.Count() == Length;

        public bool IsFixedPoint(TElement element) => Elements[element].Equals(element);

        public IEnumerable<TElement> Support => Elements.Keys.Where(key => !IsFixedPoint(key));

        public int InversionsCount
        {
            get
            {
                var result = 0;
                var elements = Elements.Values.ToArray();
                for (var i = 0; i < elements.Length - 1; i++)
                {
                    for (var j = i + 1; j < elements.Length; j++)
                    {
                        if (elements[i].CompareTo(elements[j]) > 0)
                        {
                            result++;
                        }
                    }
                }
                return result;
            }
        }

        public bool IsEven => int.IsEvenInteger(InversionsCount);

        public bool IsOdd => int.IsOddInteger(InversionsCount);

        public IEnumerable<TElement[]> Cycles
        {
            get
            {
                var processed = new List<TElement>();
                foreach (var element in Elements.Keys)
                {
                    if (processed.Contains(element))
                    {
                        continue;
                    }
                    var result = new List<TElement>();
                    var current = element;
                    do
                    {
                        result.Add(current);
                        processed.Add(current);
                        current = Elements[current];
                    } while (!current.Equals(element));
                    yield return result.ToArray();
                }
            }
        }

        public IEnumerable<TElement[]> NonTrivialCycles => Cycles.Where(c => c.Length > 1);

        public bool IsCyclic => NonTrivialCycles.Count() == 1;

        public bool IsStrictlyCyclic
        {
            get
            {
                var nonTrivialCycles = NonTrivialCycles.ToArray();
                return nonTrivialCycles.Length == 1 && nonTrivialCycles[0].Length == Length;
            }
        }

        public bool IsTransposition
        {
            get
            {
                var nonTrivialCycles = NonTrivialCycles.ToArray();
                return nonTrivialCycles.Length == 1 && nonTrivialCycles[0].Length == 2;
            }
        }
        #endregion

        #region Public methods
        public void Transpose(TElement left, TElement right) =>
            (Elements[left], Elements[right]) = (Elements[right], Elements[left]);
        #endregion

        #region Equality
        public override bool Equals(object obj) => Equals(obj as TPermutation);

        public bool Equals(TPermutation other)
        {
            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Elements.OrderBy(e => e.Key).SequenceEqual(
                other.Elements.OrderBy(e => e.Key));
        }

        public override int GetHashCode() => ((IStructuralEquatable)Elements.ToArray()).GetHashCode(
            EqualityComparer<KeyValuePair<TElement, TElement>>.Default);

        protected static bool EqualityOperator(TPermutation left, TPermutation right) =>
            left is null ? right is null : left.Equals(right);

        protected static bool NonEqualityOperator(TPermutation left, TPermutation right) => !(left == right);
        #endregion

        #region Operations
        private static TPermutation CreateEmpty(TPermutation @base)
        {
            var result = (TPermutation)@base.MemberwiseClone();
            result.Elements = [];
            return result;
        }

        protected static TPermutation Multiply(TPermutation left, TPermutation right)
        {
            var result = CreateEmpty(left);
            foreach (var kvp in left.Elements)
            {
                result.Elements[kvp.Key] = left.Elements[right.Elements[kvp.Key]];
            }
            return result;
        }

        protected static TPermutation Inverse(TPermutation value)
        {
            var result = CreateEmpty(value);
            foreach (var kvp in value.Elements)
            {
                result.Elements[kvp.Key] = value.Elements[value.Elements[kvp.Key]];
            }
            return result;
        }
        #endregion

        #region Generation
        protected static TPermutation[] Generate(TPermutation @base, Func<TElement[], TPermutation> fabric)
        {
            var result = new List<TPermutation>();
            GenerateRecursively(@base.Elements.Count, [.. @base.Elements.Keys], result, fabric);
            return [.. result];
        }

        /// <summary>
        /// <see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm">Heap's algorithm</see>
        /// </summary>
        private static void GenerateRecursively(
            int k, TElement[] array, List<TPermutation> result, Func<TElement[], TPermutation> fabric)
        {
            if (k == 1)
            {
                result.Add(fabric(array));
            }
            else
            {
                GenerateRecursively(k - 1, array, result, fabric);
                for (var i = 0; i < k - 1; i++)
                {
                    if (int.IsEvenInteger(k))
                    {
                        (array[i], array[k - 1]) = (array[k - 1], array[i]);
                    }
                    else
                    {
                        (array[0], array[k - 1]) = (array[k - 1], array[0]);
                    }
                    GenerateRecursively(k - 1, array, result, fabric);
                }
            }
        }
        #endregion

        #region Parsing
        [GeneratedRegex(@"\((\d+)\)", RegexOptions.Compiled)]
        private static partial Regex CycleRegex();

        protected static TPermutation Parse(string s, Func<char, TElement> converter, TPermutation identity)
        {
            var matches = CycleRegex().Matches(s);
            if (matches.Count == 0 || matches.Any(m => !m.Success))
            {
                throw new FormatException($"\"{s}\" cannot be parsed as a \"{typeof(TPermutation).Name}\"");
            }
            var result = CreateEmpty(identity);
            foreach (var value in matches.Select(m => m.Groups[1].Value))
            {
                for (var i = 0; i < value.Length; i++)
                {
                    result.Elements[converter(value[i])] = i < value.Length - 1
                        ? converter(value[i + 1])
                        : converter(value[0]);
                }
            }
            foreach (var missing in identity.Elements.Keys.Except(result.Elements.Keys))
            {
                result.Elements[missing] = missing;
            }
            return result;
        }

        protected static bool TryParse(string s, Func<char, TElement> converter, TPermutation identity, out TPermutation result)
        {
            try
            {
                result = Parse(s, converter, identity);
            }
            catch (FormatException)
            {
                result = default;
                return false;
            }
            return true;
        }
        #endregion

        #region Formatting
        public override string ToString() => $"({string.Join(' ', Elements.Values)})";

        /// <summary>
        /// Possible formats:
        /// <list type="bullet">
        /// <item><description>P - permutation format (default): (2 4 3 1)</description></item> 
        /// <item><description>FC - full cycle format: (124)(3)</description></item>
        /// <item><description>SC - short cycle format: (124)</description></item>
        /// </list>
        /// </summary>
        /// <param name="format">P (default), FC or SC</param>
        /// <param name="formatProvider">not used</param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public virtual string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "P";
            }
            return format.ToUpperInvariant() switch
            {
                "P" => ToString(),
                "FC" => string.Concat(Cycles.Select(elements => $"({string.Concat(elements)})")),
                "SC" => string.Concat(Cycles.Where(c => c.Length > 1).Select(elements => $"({string.Concat(elements)})")),
                _ => throw new FormatException($"The \"{format}\" format string is not supported"),
            };
        }
        #endregion
    }
}