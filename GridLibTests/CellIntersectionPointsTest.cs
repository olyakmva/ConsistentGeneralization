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
            var map = OneObjMap(out var id);
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            Assert.Equal(4, grid.Cells[0,0].MapPoints[id].Count);
            Assert.Equal(3, grid.Cells[2, 0].MapPoints[id].Count);
        }
        [Fact]
        public void AddPointsToUpperLevel2()
        {
            var map = OneObjMap(out var id);
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            Assert.Equal(4, grid.Cells[1, 0].MapPoints[id].Count);
            Assert.Equal(2, grid.Cells[1, 1].MapPoints[id].Count);
        }
        private Map OneObjMap(out int id)
        {
            Map map = new Map();
            var mapLineObj = new MapData(GeometryType.Line);
            id = 1;
            var weight = 1;
            mapLineObj.MapObjDictionary.Add(1, new List<MapPoint>(new[]
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
    }
}
