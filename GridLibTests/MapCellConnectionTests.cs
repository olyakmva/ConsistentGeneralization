using System.Collections.Generic;
using GridLib;
using MapDataLib;
using Xunit;
using System.Linq;

namespace GridLibTests
{
    public class MapCellConnectionTests
    {
        [Fact]
        public void OneLineHaveProperCells()
        {
            int id = 1;
            var map = OneObjMap(id);
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            var pointList=map.MapLayers[0].MapObjDictionary[id];
            var cellsList = grid.GetCellsBetweenPoints(pointList[0],pointList[1]).ToList();
            Assert.Single(cellsList);
            cellsList = grid.GetCellsBetweenPoints(pointList[2],pointList[3]).ToList();
            Assert.Equal(3,cellsList.Count);
            
        }
        [Fact]
        public void RightNumberOfCellsBetweenTwoPoints()
        {
            int id = 1;
            var map = OneObjMap(id);
            id = 2;
            map.Add(SomeObj(id));
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            id = 1;
            var pointList=map.MapLayers[0].MapObjDictionary[id];
            var cellsList = grid.GetCellsBetweenPoints(pointList[1],pointList[2]).ToList();
            Assert.Equal(5,cellsList.Count);
            cellsList = grid.GetCellsBetweenPoints(pointList[2],pointList[3]).ToList();
            Assert.Equal(8,cellsList.Count);
            cellsList = grid.GetCellsBetweenPoints(pointList[3],pointList[4]).ToList();
            Assert.Equal(5,cellsList.Count);

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
