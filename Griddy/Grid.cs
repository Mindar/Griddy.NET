using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Griddy
{
    public class Grid<T> : IEnumerable<T>
    {
        private Dictionary<(int x, int y), GridChunk<T>> chunks = new Dictionary<(int x, int y), GridChunk<T>>();

        public int ChunkCount
        {
            get { return chunks.Count; }
        }

        public Rectangle ComputeApproxBounds()
        {
            int minX = Int32.MaxValue;
            int maxX = Int32.MinValue;
            
            int minY = Int32.MaxValue;
            int maxY = Int32.MinValue;

            foreach (var key in chunks.Keys)
            {
                minX = key.x < minX ? key.x : minX;
                minY = key.y < minY ? key.y : minY;
                
                maxX = key.x > maxX ? key.x : maxX;
                maxY = key.y > maxY ? key.y : maxY;
            }

            int rectX = minX * (int) GridChunk<T>.ChunkSize;
            int rectY = minY * (int) GridChunk<T>.ChunkSize;
            int rectWidth = (maxX - minX) * (int) GridChunk<T>.ChunkSize;
            int rectHeight = (maxY - minY) * (int) GridChunk<T>.ChunkSize;


            return new Rectangle(rectX, rectY, rectWidth, rectHeight);
        }

        public T this[int col, int row]
        {
            get { return GetCell(col, row); }
            set { SetCell(col, row, value); }
        }

        public void SetCell(int col, int row, T thing)
        {
            // Calculate the position of the chunk itself and the position of the thing inside the chunk
            int chunkCol = Math.DivRem(col, (int) GridChunk<T>.ChunkSize, out int chunkColOffset);
            int chunkRow = Math.DivRem(row, (int) GridChunk<T>.ChunkSize, out int chunkRowOffset);
            var key = (chunkCol, chunkRow);

            // if there is no chunk at the position, we create one
            if (!chunks.ContainsKey(key))
            {
                chunks.Add(key, new GridChunk<T>());
            }
            
            // Sanitize negative indicies
            chunkColOffset = chunkColOffset >= 0 ? chunkColOffset : (int) GridChunk<T>.ChunkSize + chunkColOffset;
            chunkRowOffset = chunkRowOffset >= 0 ? chunkRowOffset : (int) GridChunk<T>.ChunkSize + chunkRowOffset;
                
            // get the chunk to work with it
            var chunk = chunks[key];
            chunk[chunkColOffset, chunkRowOffset] = thing;

            // If the thing was the default value for T, this operation could create an empty chunk.
            // To prevent empty chunks from existing, we perform some cleanup here.
            if (EqualityComparer<T>.Default.Equals(thing, default) && chunk.IsEmpty)
            {
                chunks.Remove(key);
            }
        }

        public T GetCell(int col, int row)
        {
            // Calculate the position of the chunk itself and the position of the thing inside the chunk
            int chunkCol = Math.DivRem(col, (int) GridChunk<T>.ChunkSize, out int chunkColOffset);
            int chunkRow = Math.DivRem(row, (int) GridChunk<T>.ChunkSize, out int chunkRowOffset);
            var key = (chunkCol, chunkRow);
            
            // Sanitize negative indicies
            chunkColOffset = chunkColOffset >= 0 ? chunkColOffset : (int) GridChunk<T>.ChunkSize + chunkColOffset;
            chunkRowOffset = chunkRowOffset >= 0 ? chunkRowOffset : (int) GridChunk<T>.ChunkSize + chunkRowOffset;

            // if there is no chunk at the position, we return the default value for whatever type we got 
            if (!chunks.ContainsKey(key))
            {
                return default;
            }
                
            // otherwise return whatever the chunk stores for that position
           var chunk = chunks[key];
           return chunk[chunkColOffset, chunkRowOffset];
        }

        public void Clear()
        {
            chunks.Clear();
        }
        

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var chunk in chunks.Values)
            {
                foreach (var thing in chunk)
                {
                    yield return thing;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}