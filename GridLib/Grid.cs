﻿using System;
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
        private int _maxLevel;

        public Grid(Map map, double cellSize, double detail)
        {
            _map = map;
            CellSize = cellSize;
            DetailSize = detail;
             _maxLevel =  CalculateMaxLevel();
            InitCells();

            var needToDropList = new List<Cell>();
            ObjDictionary = new Dictionary<int, List<Cell>>();
           
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
                        ModifyObjDictionary(mapObj.Key, Cells[i, j]);
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
                            ModifyObjDictionary(point.Id, Cells[i1, j1]);
                            Cells[i1, j1].AddLineIntersectionPoints(point, nextPoint);
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
                        //заменить большую ячейку на маленькую в objDictionary
                        var cellIndex=ObjDictionary[objId].FindIndex(c=>c.Equals(cell));
                        ObjDictionary[objId].Remove(cell);
                        foreach(var child in cell.GetChildrenCellsWithThisObject(objId))
                            ObjDictionary[objId].Insert(cellIndex,child);  
                    }
                    // создать новый лист ячеек, подлежащих разбиению
                    var dropCells = cell.GetChildrenWithManyObjects();
                    if (dropCells != null)
                        listForNextLevel.AddRange(dropCells);
                }
                needToDropList = listForNextLevel;
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
                    ModifyObjDictionary(mapObj.Key, Cells[i, j]);
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

        private void ModifyObjDictionary(int objId, Cell cell)
        {
            if (ObjDictionary.ContainsKey(objId)  )
            {
                if(!ObjDictionary[objId].Contains(cell))
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
