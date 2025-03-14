﻿using System.Collections.Immutable;

namespace Algebra.Core
{
    public interface IInversionOperator<TSelf, TResult>
        where TSelf : IInversionOperator<TSelf, TResult>
    {
        static abstract TResult operator ~(TSelf value);
    }

    public enum NeutralElementType
    {
        Left,
        Right,
        TwoSided
    }

    public enum InverseElementType
    {
        Left,
        Right,
        TwoSided
    }

    public enum CosetType
    {
        Left,
        Right
    }

    public static class ArrayExtensions
    {
        public static IEnumerable<T> GetRow<T>(
            this T[,] array, int row, bool reverseElements = false)
        {
            if (reverseElements)
            {
                for (var j = array.GetLength(1) - 1; j >= 0; j--)
                {
                    yield return array[row, j];
                }
            }
            else
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    yield return array[row, j];
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> GetRows<T>(
            this T[,] array, bool reverseRows = false, bool reverseElements = false)
        {
            if (reverseRows)
            {
                for (var i = array.GetLength(0) - 1; i >= 0; i--)
                {
                    yield return array.GetRow(i, reverseElements);
                }
            }
            else
            {
                for (var i = 0; i < array.GetLength(0); i++)
                {
                    yield return array.GetRow(i, reverseElements);
                }
            }
        }

        public static IEnumerable<T> GetColumn<T>(
            this T[,] array, int column, bool reverseElements = false)
        {
            if (reverseElements)
            {
                for (var i = array.GetLength(0) - 1; i >= 0; i--)
                {
                    yield return array[i, column];
                }
            }
            else
            {
                for (var i = 0; i < array.GetLength(0); i++)
                {
                    yield return array[i, column];
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> GetColumns<T>(
            this T[,] array, bool reverseColumns = false, bool reverseElements = false)
        {
            if (reverseColumns)
            {
                for (var j = array.GetLength(1) - 1; j >= 0; j--)
                {
                    yield return array.GetColumn(j, reverseElements);
                }
            }
            else
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    yield return array.GetColumn(j, reverseElements);
                }
            }
        }
    }

    public static class IEnumerableExtensions
    {
        public static bool Always(this IEnumerable<bool> source) => source.All(v => v);

        public static List<ImmutableHashSet<T>> GetCombinationsWithIdentity<T>(this IEnumerable<T> elements, int combinationSize, T identity)
        {
            var result = new List<ImmutableHashSet<T>>();
            RecursiveRoutine([.. elements], combinationSize, 0, [], identity, result);
            return result;

            static void RecursiveRoutine(
                T[] elements,
                int combinationSize,
                int start,
                List<T> currentCombination,
                T identity,
                List<ImmutableHashSet<T>> result)
            {
                if (currentCombination.Count == combinationSize)
                {
                    var tempSet = new HashSet<T> { identity };
                    tempSet.UnionWith(currentCombination);
                    result.Add(tempSet.ToImmutableHashSet());
                    return;
                }
                for (int i = start; i < elements.Length; i++)
                {
                    currentCombination.Add(elements[i]);
                    RecursiveRoutine(elements, combinationSize, i + 1, currentCombination, identity, result);
                    currentCombination.RemoveAt(currentCombination.Count - 1);
                }
            }
        }
    }

    public static class Int32Extensions
    {
        public static bool IsPrime(this int n)
        {
            if (n < 2)
            {
                return false;
            }
            if (n == 2 || n == 3)
            {
                return true;
            }
            if (n % 2 == 0 || n % 3 == 0)
            {
                return false;
            }
            for (var i = 5; i * i <= n; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}