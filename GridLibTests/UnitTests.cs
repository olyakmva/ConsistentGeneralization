using System;
using System.Collections.Generic;
using System.Linq;
using GridLib;
using MapDataLib;
using Xunit;

namespace GridLibTests
{
    public class UnitTests
    {
        [Fact]
        public void ProperUpperLeftPointInCell()
        {
            Map map = new Map();
            var md1 = new MapData(GeometryType.Line);
            md1.Vertexes.Add(1, new List<MapPoint>()
            {
                new MapPoint(2,2,1,1),
                new MapPoint(3,5,1,1),
                new MapPoint(5,7,1,1),
                new MapPoint(5,5,1,1),
                new MapPoint(7,5,1,1),
                new MapPoint(9,5,1,1)
            });
            map.Add(md1);
            int cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            var lowerPoint = new MapPoint(map.Xmin, map.Ymin, 1, 1);
            for (var i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    lowerPoint.X = map.Xmin+ i*grid.CellSize;
                    lowerPoint.Y = map.Ymin+ j * grid.CellSize;
                    Assert.Equal(lowerPoint, grid.Cells[i,j].LowerLeftPoint);
                }
            }
        }

        [Fact]
        public void ProperNumberOfCells()
        {
            Map map = new Map();
            var md1 = new MapData(GeometryType.Line);
            md1.Vertexes.Add(1,new List<MapPoint>()
            {
                new MapPoint(2,2,1,1),
                new MapPoint(3,5,1,1),
                new MapPoint(5,7,1,1),
                new MapPoint(5,5,1,1),
                new MapPoint(7,5,1,1),
                new MapPoint(9,5,1,1)
            });
            map.Add(md1);
            var md2 = new MapData(GeometryType.Line);
            md2.Vertexes.Add(2, new List<MapPoint>
            {
                new MapPoint(7,7,2,1),
                new MapPoint(6.2,4.2,2,1),
                new MapPoint(7,3,2,1),
                new MapPoint(9,5.5,2,1),
                new MapPoint(10,8,2,1)
            });
            map.Add(md2);
            var grid = new Grid(map, 2, 0.5);
            var numberCellChilds = grid.Cells[2, 1].GetAllCells().Count();
            Assert.Equal(7,numberCellChilds);
        }
        [Fact]
        public void ProperNumberOfCellsWithOneLevel()
        {
            var map = new Map();
            var md1 = new MapData(GeometryType.Line);
            const int objId = 1;
            const int objWeight = 1;
            md1.Vertexes.Add(objId, new List<MapPoint>()
            {
                new MapPoint(2,2,objId,objWeight),
                new MapPoint(2.2, 3,objId,objWeight),
                new MapPoint(3,5,objId,objWeight),
                new MapPoint(5,7,objId,objWeight),
                new MapPoint(5,5,objId,objWeight),
                new MapPoint(7,5,objId,objWeight),
                new MapPoint(9,5,objId,objWeight)
            });
            map.Add(md1);
            double cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            int n = (int)Math.Ceiling((map.Xmax - map.Xmin) / cellSize) + 1;
            int m = (int)Math.Ceiling((map.Ymax - map.Ymin) / cellSize) + 1;
            Assert.Equal(n*m,grid.Cells.Length);
            int count = 0;
            for (int i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (grid.Cells[i, j].MapPoints.Count == 0) continue;
                    count++;
                    Assert.Equal(objId, grid.Cells[i, j].ObjectId);
                    Assert.Equal(2,grid.Cells[i, j].Level);
                }
            }
            Assert.Equal(6,count);
        }

        [Fact]
        public void ProperGridForThreeMapObjects()
        {
            Map map = new Map();
            var md1 = new MapData(GeometryType.Line); 
            int objId = 1;
            const int objWeight = 1;
            md1.Vertexes.Add(objId, new List<MapPoint>()
            {
                new MapPoint(0,0,objId,objWeight),
                new MapPoint(2,2,objId,objWeight),
                new MapPoint(3,3,objId,objWeight),
                new MapPoint(4.2,3.8,objId,objWeight),
                new MapPoint(5.5,4.3,objId,objWeight),
                new MapPoint(6.1,3.7,objId,objWeight),
                new MapPoint(5,1,objId,objWeight)
            });
            map.Add(md1);
            objId = 2;
            var md2 = new MapData(GeometryType.Line);
            md2.Vertexes.Add(objId, new List<MapPoint>
            {
                new MapPoint(1,5,objId,objWeight),
                new MapPoint(2,3,objId,objWeight),
                new MapPoint(3,1,objId,objWeight),
                new MapPoint(5,2.8,objId,objWeight),
                new MapPoint(4.5,5.3,objId,objWeight)
            });
            map.Add(md2);
            objId = 3;
            var md3 = new MapData(GeometryType.Line);
            md3.Vertexes.Add(objId, new List<MapPoint>
            {
                new MapPoint(1.3,0.5,objId,objWeight),
                new MapPoint(1.5,2.3,objId,objWeight),
                new MapPoint(5.4,5.3,objId,objWeight),
                new MapPoint(7,4.9,objId,objWeight),
                new MapPoint(7.5,2.1,objId,objWeight)
            });
            map.Add(md3);
            double cellSize = 4;
            var grid = new Grid(map, cellSize, 1);
            var numberCellChilds = grid.Cells[0, 0].GetAllCells().Count();
            Assert.Equal(16, numberCellChilds);
            numberCellChilds = grid.Cells[1, 0].GetAllCells().Count();
            Assert.Equal(10, numberCellChilds);
        }

    }
}
