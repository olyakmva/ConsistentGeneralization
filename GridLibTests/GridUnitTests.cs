using System;
using System.Collections.Generic;
using System.Linq;
using GridLib;
using MapDataLib;
using Xunit;

namespace GridLibTests
{
    public class GridUnitTests
    {
        [Fact]
        public void ProperUpperLeftPointInCell()
        {
            Map map = new Map();
            var md1 = new MapData(GeometryType.Line);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(2, 2, 1, 1),
                new MapPoint(3, 5, 1, 1),
                new MapPoint(5, 7, 1, 1),
                new MapPoint(5, 5, 1, 1),
                new MapPoint(7, 5, 1, 1),
                new MapPoint(9, 5, 1, 1)
            });
            map.Add(md1);
            int cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            var lowerPoint = new MapPoint(map.Xmin, map.Ymin, 1, 1);
            for (var i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    lowerPoint.X = map.Xmin + i * grid.CellSize;
                    lowerPoint.Y = map.Ymin + j * grid.CellSize;
                    Assert.Equal(lowerPoint, grid.Cells[i, j].LowerLeftPoint);
                }
            }
        }

        [Fact]
        public void ProperNumberOfNotEmptyCellsOnMaxLevel()
        {
            Map map = CreateMap();
            int cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            int singleObjCellCount = 0;
            int manyObjCellCount = 0;
            for (var i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (grid.Cells[i, j].State == CellState.OneObject)
                        singleObjCellCount++;
                    if (grid.Cells[i, j].State == CellState.SeveralObjects)
                        manyObjCellCount++;
                }
            }

            Assert.Equal(3, singleObjCellCount);
            Assert.Equal(3, manyObjCellCount);
        }

        [Fact]
        public void ProperNumberOfCellsWithOneLevel()
        {
            var map = new Map();
            var md1 = new MapData(GeometryType.Line);
            const int objId = 1;
            const int objWeight = 1;
            md1.MapObjDictionary.Add(objId, new List<MapPoint>()
            {
                new MapPoint(2, 2, objId, objWeight),
                new MapPoint(2.2, 3, objId, objWeight),
                new MapPoint(3, 5, objId, objWeight),
                new MapPoint(5, 7, objId, objWeight),
                new MapPoint(5, 5, objId, objWeight),
                new MapPoint(7, 5, objId, objWeight),
                new MapPoint(9, 5, objId, objWeight)
            });
            map.Add(md1);
            double cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            int n = (int) Math.Ceiling((map.Xmax - map.Xmin) / cellSize) + 1;
            int m = (int) Math.Ceiling((map.Ymax - map.Ymin) / cellSize) + 1;
            Assert.Equal(n * m, grid.Cells.Length);
            int count = 0;
            for (int i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (grid.Cells[i, j].MapPoints.Count == 0) continue;
                    count++;
                    Assert.Contains(objId, grid.Cells[i, j].ObjectIdList);
                    Assert.Equal(2, grid.Cells[i, j].Level);
                }
            }

            Assert.Equal(6, count);
        }

        [Fact]
        public void CellWithTwoObjHasChildren()
        {
            var map = CreateMap();
            int cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            int manyObjChildCount = 0;
            for (var i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (grid.Cells[i, j].State == CellState.SeveralObjects)
                    {
                        foreach (var child in grid.Cells[i, j].Children)
                        {
                            Assert.Equal(grid.Cells[i, j].Level, child.Level + 1);
                            Assert.True(Math.Abs(grid.Cells[i, j].Size - child.Size * 2) < 0.001);
                            if (child.State == CellState.SeveralObjects)
                                manyObjChildCount++;
                        }
                    }
                }
            }

            Assert.Equal(5, manyObjChildCount);
        }

        [Fact]
        public void CellWithTwoObjHasChildrenOnLowLevel()
        {
            var map = CreateMap();
            int cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            int childLowLevelCount = 0;
            for (var i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (grid.Cells[i, j].State == CellState.SeveralObjects)
                    {
                        foreach (var child in grid.Cells[i, j].Children)
                        {
                            if (child.State == CellState.SeveralObjects)
                            {
                                foreach (var lowChild in child.Children)
                                {
                                    Assert.Equal(child.Level, lowChild.Level + 1);
                                    Assert.True(Math.Abs(child.Size - lowChild.Size * 2) < 0.001);
                                    if (lowChild.ObjectIdList.Count > 0)
                                        childLowLevelCount++;
                                }
                            }
                        }
                    }
                }
            }

            Assert.Equal(16, childLowLevelCount);

        }

        private static Map CreateMap()
        {
            Map map = new Map();
            var md1 = new MapData(GeometryType.Line);
            int objId = 1;
            const int objWeight = 1;
            md1.MapObjDictionary.Add(objId, new List<MapPoint>()
            {
                new MapPoint(0, 0, objId, objWeight),
                new MapPoint(1, 3, objId, objWeight),
                new MapPoint(3.5, 1.5, objId, objWeight),
                new MapPoint(5.5, 0.5, objId, objWeight),
                new MapPoint(5, 1.5, objId, objWeight)
            });
            map.Add(md1);
            var md2 = new MapData(GeometryType.Line);
            objId = 2;
            md2.MapObjDictionary.Add(objId, new List<MapPoint>
            {
                new MapPoint(5, 3, objId, objWeight),
                new MapPoint(4.5, 1.5, objId, objWeight),
                new MapPoint(4.4, 0.5, objId, objWeight),
                new MapPoint(2.5, 1, objId, objWeight),
                new MapPoint(3.5, 2.5, objId, objWeight),
                new MapPoint(2.9, 2.8, objId, objWeight)
            });
            map.Add(md2);
            return map;
        }

        [Fact]
        public void ProperGridForThreeMapObjects()
        {
            Map map = new Map();
            var md1 = new MapData(GeometryType.Line);
            int objId = 1;
            const int objWeight = 1;
            md1.MapObjDictionary.Add(objId, new List<MapPoint>()
            {
                new MapPoint(0, 0, objId, objWeight),
                new MapPoint(2, 1.9, objId, objWeight),
                new MapPoint(3, 3, objId, objWeight),
                new MapPoint(4.2, 3.8, objId, objWeight),
                new MapPoint(5.5, 4.3, objId, objWeight),
                new MapPoint(6.1, 3.7, objId, objWeight),
                new MapPoint(5, 1, objId, objWeight)
            });
            map.Add(md1);
            objId = 2;
            var md2 = new MapData(GeometryType.Line);
            md2.MapObjDictionary.Add(objId, new List<MapPoint>
            {
                new MapPoint(1, 5, objId, objWeight),
                new MapPoint(2, 3, objId, objWeight),
                new MapPoint(3, 1, objId, objWeight),
                new MapPoint(5, 2.8, objId, objWeight),
                new MapPoint(4.5, 5.3, objId, objWeight)
            });
            map.Add(md2);
            objId = 3;
            var md3 = new MapData(GeometryType.Line);
            md3.MapObjDictionary.Add(objId, new List<MapPoint>
            {
                new MapPoint(1.3, 0.5, objId, objWeight),
                new MapPoint(1.5, 2.3, objId, objWeight),
                new MapPoint(5.4, 5.3, objId, objWeight),
                new MapPoint(7, 4.9, objId, objWeight),
                new MapPoint(7.5, 2.1, objId, objWeight)
            });
            map.Add(md3);
            double cellSize = 4;
            var grid = new Grid(map, cellSize, 1);
            var numberCellChilds = grid.Cells[0, 0].GetAllCells().Count();
            Assert.Equal(16, numberCellChilds);
            numberCellChilds = grid.Cells[1, 0].GetAllCells().Count();
            Assert.Equal(13, numberCellChilds);
        }

        [Fact]
        public void ProperGridForPointsAndLines()
        {
            Map map = new Map();
            var md3 = new MapData(GeometryType.Point);
            int objId = 4;
            int objWeight = 2;
            md3.MapObjDictionary.Add(objId, new List<MapPoint>
            {
                new MapPoint(0.8, 1.25, objId, objWeight),
                new MapPoint(1.5, 1.0, objId, objWeight),
                new MapPoint(0.5, 0.5, objId, objWeight)
            });
            map.Add(md3);
            var md1 = new MapData(GeometryType.Line);
            objId = 1;
            objWeight = 1;
            md1.MapObjDictionary.Add(objId, new List<MapPoint>
            {
                new MapPoint(0, 0, objId, objWeight),
                new MapPoint(1, 3, objId, objWeight),
                new MapPoint(3.5, 1.5, objId, objWeight)
            });
            map.Add(md1);


            int cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            int oneObjCount = 0;
            int severalObjCount = 0;
            for (var i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (grid.Cells[i, j].State != CellState.SeveralObjects) continue;
                    foreach (var child in grid.Cells[i, j].Children) // дети
                    {
                        if (child.State == CellState.SeveralObjects) 
                            severalObjCount++;
                        if (child.State == CellState.OneObject)
                            oneObjCount++;
                    }
                }
            }
            Assert.Equal(1, oneObjCount);
            Assert.Equal(2, severalObjCount);
        }

        [Fact]
        public void ProperGridForPoints()
        {
            Map map = new Map();
            var md3 = new MapData(GeometryType.Point);
            int objId = 4;
            int objWeight = 2;
            md3.MapObjDictionary.Add(objId, new List<MapPoint>
            {
                new MapPoint(0.8, 1.25, objId, objWeight),
                new MapPoint(1.5, 1.0, objId, objWeight),
                new MapPoint(2.5, 2.5, objId, objWeight),
                new MapPoint(4.75, 0.2, objId, objWeight),
                new MapPoint(0.5, 0.5, objId, objWeight)
            });
            map.Add(md3);
            int cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            int oneObjCount = 0;
            int severalObjCount = 0;
            for (var i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (grid.Cells[i, j].ObjectIdList.Contains(objId))
                    {
                        oneObjCount++;
                    }

                    if (grid.Cells[i, j].State == CellState.SeveralObjects)
                        severalObjCount++;
                }
            }
            Assert.Equal(3, oneObjCount);
            Assert.Equal(0, severalObjCount);
        }
        [Fact]
        public void ProperObjDictionaryForPointsAndLines()
        {
            Map map = new Map();
            var md3 = new MapData(GeometryType.Point);
            int objId = 4;
            int objWeight = 2;
            md3.MapObjDictionary.Add(objId, new List<MapPoint>
            {
                new MapPoint(0.8, 1.25, objId, objWeight),
                new MapPoint(1.5, 1.0, objId, objWeight),
                new MapPoint(0.5, 0.5, objId, objWeight)
            });
            map.Add(md3);
            var md1 = new MapData(GeometryType.Line);
            objId = 1;
            objWeight = 1;
            md1.MapObjDictionary.Add(objId, new List<MapPoint>
            {
                new MapPoint(0, 0, objId, objWeight),
                new MapPoint(1, 3, objId, objWeight),
                new MapPoint(3.5, 1.5, objId, objWeight)
            });
            map.Add(md1);
            int cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            
            int oneObjCount = 0;
            int otherObjCount = 0;
            for (var i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (grid.Cells[i, j].State == CellState.EmptyCell) continue;
                    oneObjCount +=grid.Cells[i, j].GetAllChildCellsWithObject(1).Count();
                    otherObjCount+= grid.Cells[i, j].GetAllChildCellsWithObject(4).Count();

                }
            }
            Assert.Equal(grid.ObjDictionary[1].Count, oneObjCount);
            Assert.Equal(grid.ObjDictionary[4].Count, otherObjCount);
        }

        [Fact]
        public void AdditionalPointsIfLineIntersectCell()
        {
            Map map = new Map();

            var md1 = new MapData(GeometryType.Line);
            var objId = 1;
            var objWeight = 1;
            md1.MapObjDictionary.Add(objId, new List<MapPoint>
            {
                new MapPoint(0, 0, objId, objWeight),
                new MapPoint(1, 3, objId, objWeight),
                new MapPoint(3.5, 1.5, objId, objWeight),
                new MapPoint(5.5, 0.5, objId, objWeight)
            });
            map.Add(md1);
            int cellSize = 2;
            var grid = new Grid(map, cellSize, 0.5);
            Assert.Equal(2, grid.Cells[1, 1].MapPoints[objId].Count);
            Assert.Equal(3, grid.Cells[0, 1].MapPoints[objId].Count);
            Assert.Equal(2,grid.Cells[0, 0].MapPoints[objId].Count);
            Assert.Equal(3, grid.Cells[1, 0].MapPoints[objId].Count);
        }
        [Fact]
        public void CanSearchCellByMapobjIdAndPoint()
        {
            Map map = CreateMap();
            int cellSize = 2;
            var detail =0.5;
            var grid = new Grid(map, cellSize, detail);
            var cell = grid.GetCell(2,new MapPoint(4.5,1.5,2,1));
            Assert.Equal(detail, cell.Size);
        }
    }
}

