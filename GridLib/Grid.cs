using System;
using System.Collections.Generic;
using System.Linq;
using GeomObjectsLib;
using MapDataLib;

namespace GridLib
{
    public class Grid
    {
        public Cell[,] Cells;
        public double DetailSize { get; set; }
        public double CellSize { get; set; }
        private readonly Map _map;
        private readonly int _maxLevel;

        public Grid(Map map, double cellSize, double detail)
        {
            _map = map;
            CellSize = cellSize;
            DetailSize = detail;
             _maxLevel =  CalculateMaxLevel();
            InitCells();

            var needToDropList = new List<Cell>();
                     
            foreach (var mapData in map)
            {
                if (mapData.Geometry == GeometryType.Point || mapData.Geometry == GeometryType.MultiPoint)
                {
                    BuildGridForPoints(mapData, needToDropList);
                    continue;
                }
                foreach (var mapObj in mapData.MapObjDictionary)
                {
                    var pointList = mapObj.Value;
                    for (var k = 0; k < pointList.Count; k++)
                    {
                        var point = pointList[k];
                        var (i, j) = GetGridIndexes(point); 
                        Cells[i,j].Add(point, mapObj.Key);
                       
                        if (Cells[i, j].State == CellState.SeveralObjects)
                        {
                            if(!needToDropList.Contains(Cells[i,j]))    
                                        needToDropList.Add(Cells[i, j]);
                        }
                        
                        if (k >= pointList.Count - 1) continue;
                        var nextPoint = pointList[k + 1];
                        var (ind1, ind2) = GetGridIndexes(nextPoint);

                        if (Math.Abs(i - ind1) + Math.Abs(j - ind2) == 0) continue;
                        var minI = Math.Min(i, ind1);
                        var maxI = Math.Max(i, ind1);
                        var minJ = Math.Min(j, ind2);
                        var maxJ = Math.Max(j, ind2);
                       
                        for (var i1 = minI; i1 <= maxI; i1++)
                        for (var j1 = minJ; j1 <= maxJ; j1++)
                        {
                            if (!Cells[i1, j1].HasCommonPoint(point, nextPoint))
                                continue;
                            Cells[i1, j1].AddLineIntersectionPoints(point, nextPoint);
                           //вставить точки в объект???
                            if (Cells[i1, j1].State == CellState.SeveralObjects)
                            {
                                if(!needToDropList.Contains(Cells[i1, j1]))
                                            needToDropList.Add(Cells[i1, j1]);
                            }
                        }
                    }
                }
            }
            // разбиение на более низких уровнях
            for (int i = _maxLevel - 1; i >= 0; i--)
            {
                var listForNextLevel = new List<Cell>();
                foreach (var cell in needToDropList)
                {
                    cell.AddChildren();
                    foreach (var objId in cell.ObjectIdList)
                    {
                        // найти слой по Id объекта 
                        var layer = map.GetObjById(objId);
                        if (layer == null)
                        {
                            ErrorLog.WriteToLogFile("нет слоя " + objId);
                            continue;
                        }
                        // найти точки, которые подходят этой ячейке
                        var pointList = layer.MapObjDictionary[objId];
                        for (var k = 0; k < pointList.Count; k++)
                        {
                            // добавить эти точки в дочернюю ячейку, изменить ее состояние
                            if (cell.IsIn(pointList[k]))
                            {
                                cell.AddToChildren(pointList[k]);
                            }
                            if ((layer.Geometry== GeometryType.Line || layer.Geometry== GeometryType.Polygon) 
                                && k < pointList.Count - 1)
                            {
                                var nextPoint = pointList[k + 1];
                                if (cell.HasCommonPoint(pointList[k], nextPoint))
                                {
                                    cell.AddToChildren(pointList[k], nextPoint, pointList[k].Id);
                                }
                            }
                        }                             
                    }
                    // создать новый лист ячеек, подлежащих разбиению
                    var dropCells = cell.GetChildrenWithManyObjects();
                    if (dropCells != null && dropCells.Count>0)
                        listForNextLevel.AddRange(dropCells);
                }
                needToDropList = listForNextLevel;
            }
            FillContainerOfIntersections();
        }
        private void FillContainerOfIntersections()
        {
            for (var i = 0; i < Cells.GetLength(0); i++)
            {
              for (var j = 0; j < Cells.GetLength(1); j++)
              {
                    Cells[i,j].FillContainerOfIntersections();
              } 
            }
        }
        private void BuildGridForPoints(MapData mapData, List<Cell>needToDropList)
        {
            foreach (var mapObj in mapData.MapObjDictionary)
            {
                var pointList = mapObj.Value;
                foreach (var point in pointList)
                {
                    var (i, j) = GetGridIndexes(point);
                    Cells[i, j].Add(point, mapObj.Key);
                    if (Cells[i, j].State == CellState.SeveralObjects)
                    {
                        if (!needToDropList.Contains(Cells[i, j]))
                            needToDropList.Add(Cells[i, j]);
                    }
                }
            }
        }

        private void InitCells()
        {
            int n = (int) Math.Ceiling((_map.Xmax - _map.Xmin) / CellSize) + 1;
            int m = (int) Math.Ceiling((_map.Ymax - _map.Ymin) / CellSize) + 1;
            Cells = new Cell[n, m];
            for (var i = 0; i < n; i++)
            for (var j = 0; j < m; j++)
            {
                Cells[i, j] = new Cell
                {
                    LowerLeftPoint = new MapPoint() {X = _map.Xmin + i * CellSize, Y = _map.Ymin + j * CellSize},
                    Level = _maxLevel,
                    Size = CellSize
                };
            }
        }

        private int CalculateMaxLevel()
        {
            int maxLevel = 1;
            var x = CellSize;
            while (x / 2 > DetailSize)
            {
                maxLevel++;
                x /= 2;
            }
            return maxLevel;
        }

        internal (int, int) GetGridIndexes(MapPoint point)
        {
            int ind1 = (int)Math.Truncate((point.X - _map.Xmin) / CellSize);
            int ind2 = (int)Math.Truncate((point.Y - _map.Ymin) / CellSize);
            return (ind1, ind2);
        }
        public Cell GetCell(int id, MapPoint point)
        {
            var (i,j) = GetGridIndexes(point);          
            var cells = Cells[i,j].GetAllChildCellsWithObject(id).ToList().FindAll(c=> c.IsIn(point));
            if( cells.Count == 1)
                return cells[0];
            if(cells.Count >=2)
            {
                cells = cells.OrderBy(c=> c.Level).ToList();
                return cells[0];
            }
            throw new ArgumentException($"не найдена ячейка для {point} c id {id}");
        }
        public IEnumerable<Cell> GetCellsBetweenPoints(MapPoint point1, MapPoint point2)
        {
            var list = new List<MapPoint>(){point1,point2 };
            int k=0;
            while( k<list.Count -1)
            {
                if(list[k].DistanceToVertex(list[k+1]) < DetailSize)
                { 
                    k++ ;  continue;
                }
                var p3 = new MapPoint((list[k].X+list[k+1].X)/2, (list[k].Y+list[k+1].Y)/2, list[k].Id, 9);
                list.Insert(k+1,p3);
            }

            var resultList = new List<Cell>();
            var (i,j) = GetGridIndexes(point1); 
                      
            var cells = Cells[i,j].GetAllChildCellsWithObject(point1.Id).ToList();
            var first = cells.Find(c=> c.IsIn(point1));
            resultList.Add(first);
            k=1;
            while(k< list.Count)
            {
                if( Cells[i,j].IsIn(list[k]))
                {   
                    if(!resultList[resultList.Count-1].IsIn(list[k]))
                    {
                        var nextCell= cells.Find(c=> c.IsIn(list[k]));
                        if(nextCell !=null && !resultList.Contains(nextCell))
                            resultList.Add(nextCell);                    
                    }
                     k++;
                }
                else
                {
                    (i,j) = GetGridIndexes(list[k]); 
                    cells = Cells[i,j].GetAllChildCellsWithObject(point1.Id).ToList();
                }
            }
            return resultList;
        }
    }
}
