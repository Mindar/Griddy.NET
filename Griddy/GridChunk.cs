using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Griddy
{
    public class GridChunk<T> : IEnumerable<T>
    {
        public const uint ChunkSize = 64U;

        private T[] _cells = new T[ChunkSize * ChunkSize];

        public bool IsEmpty
        {
            get
            {
                foreach (var thing in _cells)
                {
                    if (!thing.Equals(default(T)))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public T this[int col, int row]
        {
            get { return _cells[col + ChunkSize * row]; }
            set { _cells[col + ChunkSize * row] = value; }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>) _cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _cells = new T[ChunkSize * ChunkSize];
        }

        public bool Contains(T item)
        {
            return _cells.Contains(item);
        }
        
        public static bool operator ==(GridChunk<T> first, object second)
        {
            return first.Equals(second);
        }
        
        public static bool operator !=(GridChunk<T> first, object second)
        {
            return !first.Equals(second);
        }
        
        public static bool operator ==(object first, GridChunk<T> second)
        {
            return second.Equals(first);
        }
        
        public static bool operator !=(object first, GridChunk<T> second)
        {
            return !second.Equals(first);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GridChunk<T>)) return false;
            var other = (GridChunk<T>) obj;

            for (int row = 0; row < ChunkSize; row++)
            {
                for (int col = 0; col < ChunkSize; col++)
                {
                    if (!other[col, row].Equals(this[col, row]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _cells.GetHashCode();
        }
    }
}