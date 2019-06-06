using System.Collections.Generic;
using NUnit.Framework;

namespace Griddy.Test
{
    public class GridTest
    {
        private Grid<bool> boolgrid;
        private Grid<ReferenceTypeTestThing> refgrid;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSetGetValueTypeRoundtrip()
        {
            boolgrid = new Grid<bool>();
            
            var values = new List<(int x, int y, bool val)>();
            values.Add((0, 0, true));
            values.Add((1, 1, true));
            values.Add((-1, 1, true));
            values.Add((1, -1, true));
            values.Add((-1, -1, true));
            values.Add((999999, 0, true));
            values.Add((-123456, 20, true));

            // Check true
            foreach (var item in values)
            {
                boolgrid.SetCell(item.x, item.y, item.val);
                
                Assert.AreEqual(item.val, boolgrid.GetCell(item.x, item.y));
            }

            // Check false
            foreach (var item in values)
            {
                boolgrid.SetCell(item.x, item.y, !item.val);
                
                Assert.AreEqual(!item.val, boolgrid.GetCell(item.x, item.y));
            }
        }

        [Test]
        public void TestSetGetReferenceTypeRoundtrip()
        {
            refgrid = new Grid<ReferenceTypeTestThing>();
            
            var values = new List<(int x, int y, ReferenceTypeTestThing val)>();
            values.Add((0, 0, new ReferenceTypeTestThing(1)));
            values.Add((1, 1, new ReferenceTypeTestThing(1)));
            values.Add((-1, 1, new ReferenceTypeTestThing(1)));
            values.Add((1, -1, new ReferenceTypeTestThing(1)));
            values.Add((-1, -1, new ReferenceTypeTestThing(1)));
            values.Add((999999, 0, new ReferenceTypeTestThing(1)));
            values.Add((-123456, 20, new ReferenceTypeTestThing(1)));

            // Check non-null
            foreach (var item in values)
            {
                refgrid.SetCell(item.x, item.y, item.val);
                
                Assert.NotNull(refgrid.GetCell(item.x, item.y));
                Assert.AreEqual(item.val, refgrid.GetCell(item.x, item.y));
            }

            // Check null
            foreach (var item in values)
            {
                refgrid.SetCell(item.x, item.y, null);
                
                Assert.Null(refgrid.GetCell(item.x, item.y));
            }
        }
    }
}