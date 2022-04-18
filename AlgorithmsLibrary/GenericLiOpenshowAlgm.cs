using MapDataLib;
using System;
using GridLib;
using System.Collections.Generic;

namespace AlgorithmsLibrary
{
    /// <summary>
    /// Обобщенный алгоритм Ли-Опеншоу: на карту наброшена сетка 
    /// и оставляются только одна точка в ячейке сетки
    /// </summary>
    public class GenericLiOpenshowAlgm:ISimplificationAlgm
    {
        private const double LiWeight = 200;
     
        public SimplificationAlgmParameters Options { get; set; }
        public Grid grid;

        public GenericLiOpenshowAlgm()
        {

        }
        public void Run(Map map, GridLib.Grid grid)
        {
            foreach( var mapData in map.MapLayers)
            {
                foreach(var mapObj in mapData.MapObjDictionary)
                {
                    Run(mapObj.Key, mapObj.Value,grid);
                }
            }
        }
        internal void Run(int objId, List<MapPoint> pointList, Grid grid)
        {
            // обработка случая малого количества точек


            // получить список ячеек сетки для объекта
            var objCells = grid.ObjDictionary[objId];
            // двигаться по точкам объекта. Если ячейка = один объект,
            // то получить среднюю точку, остальные пометить как удаляемые

            // если в ячейке несколько объектов, то учесть их состояние
        }
    //    private void ProcessingObjectWithSmallNumberOfPoints(List<MapPoint> points)
    //    {

    //        switch (points.Count )
    //        {
    //            case 0:
    //                return;
    //            case 1:
    //                ProcessOneEdge(points, 0);
    //                return;
    //            case 2:
    //                var distance23 = points[points.Count-1].DistanceToVertex(points[ 1]);
    //                var distance12 = points[0].DistanceToVertex(points[ 1]);
    //                if (Math.Min(distance12, distance23) > _cellSize)
    //                {
    //                    return;
    //                }
    //                if (distance12 < _cellSize && distance23 < _cellSize)
    //                {
    //                    points.RemoveAt( 1);
    //                    ProcessOneEdge(points, 0, ref points.Count-1);
    //                }
    //                else if (distance12 < _cellSize)
    //                {
    //                    int k =  1;
    //                    ProcessOneEdge(points, 0, ref k);
    //                }
    //                else
    //                {
    //                    ProcessOneEdge(points, 0 + 1, ref points.Count-1);
    //                }
    //                return;
    //        }
    //    }
    //    private void ProcessOneEdge(List<MapPoint> vertices, int start)
    //    {
    //        var distance = vertices[start+1].DistanceToVertex(vertices[start]);
    //        if (distance > _cellSize) return;
    //        if (start == 0 && last < vertices.Count - 1)
    //        {
    //            vertices.RemoveAt(last);
    //            vertices[start].Weight = LiWeight;
    //            last--;
    //        }
    //        else if (start > 0 && last == vertices.Count - 1)
    //        {
    //            vertices.RemoveAt(start);
    //            last--;
    //        }
    //        else if (start == 0 && last == vertices.Count - 1)
    //        {
    //        }
    //        else
    //        {
    //            vertices[start].X = (vertices[start].X + vertices[last].X) / 2;
    //            vertices[start].Y = (vertices[start].Y + vertices[last].Y) / 2;
    //            vertices.RemoveAt(last);
    //            last--;
    //        }
    //    }

    }
}
