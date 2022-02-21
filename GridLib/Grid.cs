using System;
using System.Collections.Generic;
using MapDataLib;

namespace GridLib
{
    public class Grid
    {
        public Cell[,] Cells;
        public double DetailSize { get; set; }
        public double CellSize { get; set; }
        public Dictionary<int, List<Cell>> ObjDictionary;
        private const int EmptyCellLevel = -5;
        public Grid(Map map, double cellSize, double detail)
        {
            CellSize = cellSize;
            DetailSize = detail;
            int maxLevel = 1;
            var x = CellSize;
            while (x / 2 > DetailSize)
            {
                maxLevel++;
                x /= 2;
            }

            ObjDictionary = new Dictionary<int, List< Cell>>();
            int n =(int) Math.Ceiling((map.Xmax - map.Xmin) / cellSize)+1; 
            int m = (int)Math.Ceiling((map.Ymax - map.Ymin) / cellSize)+1;
            Cells = new Cell[n, m];
            for(var i=0; i<n; i++)
            for (var j = 0; j < m; j++)
            {
                Cells[i, j] = new Cell()
                {
                    LowerLeftPoint = new MapPoint() {X = map.Xmin + i * cellSize, Y = map.Ymin + j * cellSize},
                    Level = EmptyCellLevel,
                    Size = CellSize
                };
            }


            foreach (var mapObj in map)
            {
                foreach (var pointList in mapObj.Vertexes)
                {
                    foreach (var point in pointList.Value)
                    {
                        int i = (int) Math.Truncate((point.X - map.Xmin) / CellSize);
                        int j = (int) Math.Truncate((point.Y - map.Ymin) / CellSize);
                        if (Cells[i, j].Level== EmptyCellLevel)
                        {
                            Cells[i, j].ObjectId = point.Id;
                            Cells[i, j].Level = maxLevel;
                            Cells[i,j].MapPoints.Add(pointList.Key, new List<MapPoint>(new []{point}));
                            if (ObjDictionary.ContainsKey(pointList.Key))
                            {
                                ObjDictionary[pointList.Key].Add(Cells[i, j]); 
                            }
                        }
                        else
                        {
                            if (Cells[i, j].ObjectId == pointList.Key)
                            {
                                Cells[i, j].MapPoints[pointList.Key].Add(point);
                                continue;
                            }
                            Cells[i, j].Add(point);
                        }
                    }
                }
            }
        }
    }
}
