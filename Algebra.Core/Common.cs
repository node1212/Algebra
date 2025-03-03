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

    public static class ArrayExtensions
    {
        public static IEnumerable<T> GetRow<T>(this T[,] array, int row)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                yield return array[row, j];
            }
        }

        public static IEnumerable<IEnumerable<T>> GetRows<T>(this T[,] array)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                yield return array.GetRow(i);
            }
        }

        public static IEnumerable<T> GetColumn<T>(this T[,] array, int column)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                yield return array[i, column];
            }
        }

        public static IEnumerable<IEnumerable<T>> GetColumns<T>(this T[,] array)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                yield return array.GetColumn(j);
            }
        }
    }
}