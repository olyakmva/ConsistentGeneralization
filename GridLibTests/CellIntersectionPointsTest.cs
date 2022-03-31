using System.Collections.Generic;
using GridLib;
using MapDataLib;
using Xunit;

namespace GridLibTests
{
    public class CellIntersectionPointsTest
    {
        [Fact]
        public void AddPointsToUpperLevel()
        {
            int id = 1;
            var map = OneObjMap(id);
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            Assert.Equal(4, grid.Cells[0,0].MapPoints[id].Count);
            Assert.Equal(3, grid.Cells[2, 0].MapPoints[id].Count);
        }
        [Fact]
        public void AddPointsToUpperLevel2()
        {
            int id = 1;
            var map = OneObjMap(id);
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            Assert.Equal(4, grid.Cells[1, 0].MapPoints[id].Count);
            Assert.Equal(2, grid.Cells[1, 1].MapPoints[id].Count);
        }
        [Fact]
        public void AddPointsToLevel2()
        {
            int id = 1;
            var map = OneObjMap(id);
            map.Add(SomeObj(2));
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            var cells00 = grid.Cells[0, 0].Children;
            Assert.Equal(3, cells00[0].MapPoints[id].Count);
            Assert.Equal(3, cells00[2].MapPoints[id].Count);
            Assert.Equal(2, cells00[3].MapPoints[id].Count);
            id = 2;
            Assert.Equal(2, cells00[0].MapPoints[id].Count);
            Assert.Equal(2, cells00[1].MapPoints[id].Count);
            Assert.Equal(2, cells00[2].MapPoints[id].Count);
            Assert.Equal(3, cells00[3].MapPoints[id].Count);

            var cells10 = grid.Cells[1, 0].Children;
            id=1;
            Assert.Equal(2, cells10[1].MapPoints[id].Count);
            Assert.Equal(2, cells10[2].MapPoints[id].Count);
            Assert.Equal(2, cells10[3].MapPoints[id].Count);
            id = 2;
            Assert.Equal(2, cells10[1].MapPoints[id].Count);
            Assert.Equal(2, cells10[2].MapPoints[id].Count);
            Assert.Equal(3, cells10[3].MapPoints[id].Count);
        }
        [Fact]
        public void AddPointsToLevelZero()
        {
            int id = 1;
            var map = OneObjMap(id);
            map.Add(SomeObj(2));
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            var cells00 = grid.Cells[0, 0].Children;
            var cells001 =cells00[2].Children;
            id=1;
            Assert.Equal(3, cells001[0].MapPoints[id].Count);
            Assert.Equal(2, cells001[1].MapPoints[id].Count);
            id=2;
            Assert.Equal(2, cells001[1].MapPoints[id].Count);

        }
        private Map OneObjMap( int id=1)
        {
            Map map = new Map();
            var mapLineObj = new MapData(GeometryType.Line);
            var weight = 1;
            mapLineObj.MapObjDictionary.Add(id, new List<MapPoint>(new[]
            {
                new MapPoint(0, 0, id, weight),
                new MapPoint(0.25, 0.5, id, weight),
                new MapPoint(1.25, 1.5, id, weight),
                new MapPoint(4.5, 0.5, id, weight),
                new MapPoint(3.25, 2.5, id, weight)
            }));
            map.Add(mapLineObj);
            return map;
        }
        private MapData SomeObj( int id=2)
        {
            var mapLineObj = new MapData(GeometryType.Line);
            var weight = 1;
            mapLineObj.MapObjDictionary.Add(id, new List<MapPoint>(new[]
            {
                new MapPoint(1.25, 0.5, id, weight),
                new MapPoint(0.25, 1.5, id, weight),
                new MapPoint(2.5, 1.5, id, weight),
                new MapPoint(4.5, 0.2, id, weight)
            }));

            return mapLineObj;
        }

    }
}
