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
        public void ProperUpperLeftPointinCell()
        {

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
            Assert.Equal(4,numberCellChilds);


        }
        [Fact]
        public void ProperNumberOfCellsWithOneLevel()
        {
            Map map = new Map();
            var md1 = new MapData(GeometryType.Line);
            md1.Vertexes.Add(1, new List<MapPoint>()
            {
                new MapPoint(2,2,1,1),
                new MapPoint(2.2, 3,1,1),
                new MapPoint(3,5,1,1),
                new MapPoint(5,7,1,1),
                new MapPoint(5,5,1,1),
                new MapPoint(7,5,1,1),
                new MapPoint(9,5,1,1)
            });
            map.Add(md1);
            var grid = new Grid(map, 2, 0.5);
            Assert.Equal(12,grid.Cells.Length);
            int count = 0;
            for (int i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (grid.Cells[i, j] == null) continue;
                    count++;
                    Assert.Equal(1,grid.Cells[i, j].ObjectId);
                    Assert.Equal(2,grid.Cells[i, j].Level);
                }
            }
            Assert.Equal(6,count);
        }
    }
}
