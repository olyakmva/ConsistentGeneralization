using System;
using System.Collections.Generic;
using AlgorithmsLibrary;
using MapDataLib;

namespace GridLib
{
    public class Grid
    {
        public Cell[,] Cells;
        public double DetailSize { get; set; }
        public double CellSize { get; set; }
        public Dictionary<int, List<Cell>> ObjDictionary;
        private Map _map;

        public Grid(Map map, double cellSize, double detail)
        {
            _map = map;
            CellSize = cellSize;
            DetailSize = detail;
            int maxLevel =  CalculateMaxLevel();
            InitCells( maxLevel);

            var needToDropList = new List<Cell>();
            ObjDictionary = new Dictionary<int, List<Cell>>();
           
            foreach (var mapObj in map)
            {
                foreach (var pointList in mapObj.MapObjDictionary)
                {
                    for (var k = 0; k < pointList.Value.Count; k++)
                    {
                        var point = pointList.Value[k];
                        var (i, j) = GetGridIndexes(point);                     
                        if (Cells[i, j].State == CellState.EmptyCell)
                        {
                            Cells[i, j].ObjectIdList.Add(pointList.Key);
                            Cells[i, j].Level = maxLevel;
                            Cells[i, j].State = CellState.OneObject;
                            Cells[i, j].MapPoints.Add(pointList.Key, new List<MapPoint>(new[] {point}));
                            ModifyObjDictionary(pointList.Key, Cells[i, j]);
                        }
                        else
                        {
                            if (Cells[i, j].State == CellState.OneObject)
                            {
                                if (Cells[i, j].ObjectIdList.Contains(pointList.Key))
                                {
                                    Cells[i, j].MapPoints[pointList.Key].Add(point);
                                }
                                else
                                {
                                    Cells[i, j].ObjectIdList.Add(pointList.Key);
                                    Cells[i, j].State = CellState.SeveralObjects;
                                    needToDropList.Add(Cells[i, j]);
                                    if (Cells[i, j].MapPoints.ContainsKey(pointList.Key))
                                    {
                                        Cells[i, j].MapPoints[pointList.Key].Add(point);
                                    }
                                    else
                                    {
                                        Cells[i, j].MapPoints.Add(pointList.Key, new List<MapPoint>(new[] {point}));
                                        ModifyObjDictionary(pointList.Key, Cells[i, j]);
                                    }
                                }
                            }
                            else
                            {
                                if (!Cells[i, j].ObjectIdList.Contains(pointList.Key))
                                {
                                    Cells[i, j].ObjectIdList.Add(pointList.Key);
                                }
                                if (Cells[i, j].MapPoints.ContainsKey(pointList.Key))
                                {
                                    Cells[i, j].MapPoints[pointList.Key].Add(point);
                                }
                                else
                                {
                                    needToDropList.Add(Cells[i, j]);
                                    Cells[i, j].MapPoints.Add(pointList.Key, new List<MapPoint>(new[] {point}));
                                    ModifyObjDictionary(pointList.Key, Cells[i, j]);
                                }
                            }
                        }

                        if (k >= pointList.Value.Count - 1) continue;
                        var nextPoint = pointList.Value[k + 1];
                        var (ind1, ind2) = GetGridIndexes(nextPoint);
                        
                        if (Math.Abs(i - ind1) + Math.Abs(j - ind2) >= 2)
                        {
                            var minI = Math.Min(i, ind1);
                            var maxI = Math.Max(i, ind1);
                            var minJ = Math.Min(j, ind2);
                            var maxJ = Math.Max(j, ind2);
                            Line line = new Line(point, nextPoint);
                            for (var i1 = minI; i1 <= maxI; i1++)
                            for (var j1 = minJ; j1 <= maxJ; j1++)
                            {
                                if (Cells[i1, j1].ObjectIdList.Contains(point.Id))
                                    continue;
                                if (i1 == i && j1 == j || i1 == ind1 && j1 == ind2)
                                    continue;
                                if (!Cells[i1, j1].HasCommonPoint(line))
                                    continue;
                                ModifyObjDictionary(point.Id, Cells[i1, j1]);
                                Cells[i1, j1].ObjectIdList.Add(point.Id);
                                if (!Cells[i1, j1].MapPoints.ContainsKey(point.Id))
                                    Cells[i1, j1].MapPoints.Add(point.Id, new List<MapPoint>());
                                if (Cells[i1, j1].State == CellState.EmptyCell)
                                    Cells[i1, j1].State = CellState.OneObject;
                                else if (Cells[i1, j1].State == CellState.OneObject)
                                {
                                    Cells[i1, j1].State = CellState.SeveralObjects;
                                    needToDropList.Add(Cells[i1, j1]);
                                }
                            }

                        }
                    }
                }
            }
            // разбиение на более низких уровнях
            for (int i = maxLevel - 1; i >= 0; i--)
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
                                cell.Add(pointList[k]);
                            }
                            if (k < pointList.Count - 1)
                            {
                                var nextPoint = pointList[k + 1];
                                if (cell.HasCommonPoint(pointList[k], nextPoint))
                                {
                                    cell.Add(pointList[k], nextPoint, pointList[k].Id);
                                }

                            }
                        }
                        //заменить большую ячейку на маленькую в objDictionary
                        ObjDictionary[objId].Remove(cell);
                        ObjDictionary[objId].AddRange(cell.GetChildrenCellsWithObject(objId));
                    }
                    // создать новый лист ячеек, подлежащих разбиению
                    var dropCells = cell.GetChildrenWithManyObjects();
                    if (dropCells != null)
                        listForNextLevel.AddRange(dropCells);
                }
                needToDropList = listForNextLevel;
            }
        }

        private void InitCells(  int maxLevel)
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
                    Level = maxLevel,
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

        private void ModifyObjDictionary(int objId, Cell cell)
        {
            if (ObjDictionary.ContainsKey(objId))
            {
                ObjDictionary[objId].Add(cell);
            }
            else ObjDictionary.Add(objId, new List<Cell>(new[] {cell}));
        }

        private (int, int) GetGridIndexes(MapPoint point)
        {
            int ind1 = (int)Math.Truncate((point.X - _map.Xmin) / CellSize);
            int ind2 = (int)Math.Truncate((point.Y - _map.Ymin) / CellSize);
            return (ind1, ind2);
        }
    }
}
