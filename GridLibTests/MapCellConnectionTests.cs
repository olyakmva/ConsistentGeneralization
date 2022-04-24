using System.Collections.Generic;
using GridLib;
using MapDataLib;
using Xunit;

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
            var cellsList = grid.ObjDictionary[id];
            var  indexesList= new List<int>();
            foreach(var point in map.MapLayers[0].MapObjDictionary[id] )
            {
                int j = cellsList.FindIndex(c=>c.IsIn(point));
                indexesList.Add(j);
            }
            for(var i=0; i<indexesList.Count-1; i++)
            {
                Assert.True(indexesList[i]<=indexesList[i+1]);
            }           
        }
        [Fact]
        public void RightOrderCellsWhichContainsMapObj()
        {
            int id = 1;
            var map = OneObjMap(id);
            id = 2;
            map.Add(SomeObj(id));
            var cellSize = 2;
            var detail = 0.5;
            var grid = new Grid(map, cellSize, detail);
            id = 1;
            var cellsList = grid.ObjDictionary[id];

            var indexesList = new List<int>();
            foreach (var point in map.MapLayers[0].MapObjDictionary[id])
            {
                int j = cellsList.FindIndex(c => c.IsIn(point));
                indexesList.Add(j);
            }
            //for (var i = 0; i < indexesList.Count - 1; i++)
            //{
            //    Assert.True(indexesList[i] <= indexesList[i + 1]);
            //}
            id = 2;
            indexesList.Clear();
            cellsList = grid.ObjDictionary[id];
            foreach (var point in map.MapLayers[1].MapObjDictionary[id])
            {
                int j = cellsList.FindIndex(c => c.IsIn(point));
                indexesList.Add(j);
            }
            for (var i = 0; i < indexesList.Count - 1; i++)
            {
                Assert.True(indexesList[i] <= indexesList[i + 1]);
            }

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
