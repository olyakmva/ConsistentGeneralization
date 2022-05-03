using Xunit;
using GridLib;
using MapDataLib;
using System.Collections.Generic;
using AlgorithmsLibrary;

namespace AlgorithmsLibTests
{
    public class GenLiOpenshowTest
    {
        [Fact]
        public void CanSimplifyOneObj()
        {
            int id = 1;
            var map = OneObjMap(id);
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            var algm = new GenericLiOpenshowAlgm();
            algm.Run(map,grid);
            Assert.Equal(4,map.MapLayers[0].MapObjDictionary[id].Count);

        }
        [Fact]
        public void CanSimplifyTwoObj()
        {
            int id = 1;
            var map = OneObjMap(id);
            map.Add(SomeObj());
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            var algm = new GenericLiOpenshowAlgm();
            algm.Run(map,grid);
            Assert.Equal(2, map.MapLayers.Count);
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
                new MapPoint(1.25, 0.25, id, weight),
                new MapPoint(0.25, 1.45, id, weight),
                new MapPoint(2.51, 1.48, id, weight),
                new MapPoint(4.4, 0.2, id, weight)
            }));

            return mapLineObj;
        }
    }
}
