using System.Collections.Generic;
using NUnit.Framework;

namespace Griddy.Test
{
    public class GridChunkTest
    {
        private GridChunk<bool> boolchunk;
        
        [Test]
        public void TestIsEmpty()
        {
            boolchunk = new GridChunk<bool>();

            boolchunk[0, 0] = true;
            
            Assert.IsFalse(boolchunk.IsEmpty);

            boolchunk[0, 0] = false;
            
            Assert.IsFalse(!boolchunk.IsEmpty);
        }

        [Test]
        public void TestValueEquality()
        {
            var chunk1 = new GridChunk<int>();
            var chunk2 = new GridChunk<int>();

            chunk1[0, 13] = -42;
            chunk2[0, 13] = -42;
            
            Assert.AreEqual(chunk1, chunk2, "The chunks should be equal.");
        }

        [Test]
        public void TestValueEquality2()
        {
            var refthing = new ReferenceTypeTestThing(5);
            
            var chunk1 = new GridChunk<ReferenceTypeTestThing>();
            var chunk2 = new GridChunk<ReferenceTypeTestThing>();

            chunk1[12, 4] = refthing;
            chunk2[12, 4] = refthing;
            
            Assert.AreEqual(chunk1, chunk2, "The chunks should be equal.");
        }

        [Test]
        public void TestValueEquality3()
        {
            var refthing = new ReferenceTypeTestThing(5);
            var otherthing = new ReferenceTypeTestThing(5);
            
            var chunk1 = new GridChunk<ReferenceTypeTestThing>();
            var chunk2 = new GridChunk<ReferenceTypeTestThing>();

            chunk1[12, 4] = refthing;
            chunk2[12, 4] = otherthing;
            
            Assert.AreNotEqual(chunk1, chunk2, "The chunks should not be equal.");
        }

        [Test]
        public void TestClearValueType()
        {
            var chunk = new GridChunk<int>();

            for (int x = 1; x < GridChunk<int>.ChunkSize; x++)
            {
                for (int y = 1; y < GridChunk<int>.ChunkSize; y++)
                {
                    chunk[x - 1, y - 1] = x * y;
                }
            }
            
            
            chunk.Clear();

            foreach (var value in chunk)
            {
                Assert.AreEqual(0, value);
            }
        }
        
        [Test]
        public void TestClearReferenceType()
        {
            var chunk = new GridChunk<ReferenceTypeTestThing>();

            for (int x = 1; x < GridChunk<int>.ChunkSize; x++)
            {
                for (int y = 1; y < GridChunk<int>.ChunkSize; y++)
                {
                    chunk[x - 1, y - 1] = new ReferenceTypeTestThing(1);
                }
            }
            
            
            chunk.Clear();

            foreach (var value in chunk)
            {
                Assert.AreEqual(null, value);
            }
        }

        [Test]
        public void TestContains()
        {
            var chunk = new GridChunk<int>();
            chunk[32, 32] = 42;
            
            Assert.AreEqual(true, chunk.Contains(42));
            Assert.AreEqual(true, chunk.Contains(0));
            Assert.AreEqual(false, chunk.Contains(33));
        }
    }
}