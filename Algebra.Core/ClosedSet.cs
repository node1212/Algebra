namespace Algebra.Core
{
    public abstract class ClosedSet<T>(params T[] elements) where T :
        IEquatable<T>
    {
        public HashSet<T> Elements { get; private set; } = new HashSet<T>(elements);

        protected abstract T Op(T left, T right);

        protected EqualityComparer<T> EqualityComparer { get; } = EqualityComparer<T>.Default;

        public int Order => Elements.Count;

        protected bool IsClosed
        {
            get
            {
                var elements = Elements.ToArray();
                for (var i = 0; i < elements.Length; i++)
                {
                    for (var j = 0; j < elements.Length; j++)
                    {
                        if (!elements.Contains(Op(elements[i], elements[j]), EqualityComparer))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public bool IsCommutative
        {
            get
            {
                var elements = Elements.ToArray();
                for (var i = 0; i < elements.Length; i++)
                {
                    for (var j = 0; j < elements.Length; j++)
                    {
                        if (!EqualityComparer.Equals(
                            Op(elements[i], elements[j]),
                            Op(elements[j], elements[i])))
                        {
                            return false; 
                        }
                    }
                }
                return true;
            }
        }

        public bool IsAbelian => IsCommutative;

        public bool IsAssociative
        {
            get
            {
                var elements = Elements.ToArray();
                for (var i = 0; i < elements.Length; i++)
                {
                    for (var j = 0; j < elements.Length; j++)
                    {
                        for (var k = 0; k < elements.Length; k++)
                        {
                            if (!EqualityComparer.Equals(
                                Op(Op(elements[i], elements[j]), elements[k]),
                                Op(elements[i], Op(elements[j], elements[k]))))
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }

        public virtual bool IsValid => IsClosed;
    }
}