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
    }
}