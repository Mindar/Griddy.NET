using System;
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
                    if (EqualityComparer<T>.Default.Equals(thing, default))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public T this[int col, int row]
        {
            get
            {
                if(col < 0 || col > ChunkSize) throw new ArgumentOutOfRangeException(nameof(col), "Value was: " + col.ToString());
                if(row < 0 || row > ChunkSize) throw new ArgumentOutOfRangeException(nameof(row), "Value was: " + row.ToString());
                return _cells[col + ChunkSize * row];
            }
            set
            {
                if(col < 0 || col > ChunkSize) throw new ArgumentOutOfRangeException(nameof(col), "Value was: " + col.ToString());
                if(row < 0 || row > ChunkSize) throw new ArgumentOutOfRangeException(nameof(row), "Value was: " + row.ToString());
                _cells[col + ChunkSize * row] = value;
            }
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

        public override bool Equals(object obj)
        {
            if (!(obj is GridChunk<T>)) return false;
            var other = (GridChunk<T>) obj;

            for (int row = 0; row < ChunkSize; row++)
            {
                for (int col = 0; col < ChunkSize; col++)
                {
                    if (!EqualityComparer<T>.Default.Equals(other[col, row], this[col, row]))
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