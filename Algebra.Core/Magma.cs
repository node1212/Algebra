﻿using System.Numerics;

namespace Algebra.Core
{
    public class Magma<T> : Algebra<T> where T : IEquatable<T>
    {
        protected readonly CayleyTable<T> _cayleyTable;

        public Magma(CayleyTable<T> cayleyTable) => _cayleyTable = cayleyTable;

        public Magma(T[] elements) => _cayleyTable = new CayleyTable<T>(Op, elements);

        protected override T[] Elements => _cayleyTable.Header;

        protected override T Op(T left, T right) => _cayleyTable[left, right];

        public override bool IsValid => IsClosed;
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