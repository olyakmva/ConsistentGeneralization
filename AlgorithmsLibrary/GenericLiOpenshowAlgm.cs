using MapDataLib;
using System.Linq;
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
        private const double RemoveWeight =100;
        public SimplificationAlgmParameters Options { get; set; }
        public Grid grid;

        public GenericLiOpenshowAlgm()
        {
        }
        public void Run(Map map, Grid grid)
        {
            foreach( var mapData in map.MapLayers)
            {
                if(mapData.Geometry == GeometryType.Point ||
                    mapData.Geometry == GeometryType.MultiPoint ||
                    mapData.Geometry == GeometryType.Unspecified)
                    continue;
                foreach(var mapObj in mapData.MapObjDictionary)
                {
                    Run(mapObj.Key, mapObj.Value, grid);
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
            int i = 0;
            while (i < pointList.Count)
            {
                MapPoint point = pointList[i];
                var cell = grid.GetCell(objId,point);
                if(cell.State == CellState.OneObject)
                {
                    point.Weight = RemoveWeight;
                    while (i< pointList.Count && cell.IsIn(pointList[i]))
                    {
                        pointList[i].Weight=RemoveWeight;
                        i++;
                    }
                    var first =cell.MapPoints[objId][0];
                    var last = cell.MapPoints[objId].Last();
                    var middle = new MapPoint((first.X +last.X)/2, (first.Y+last.Y)/2,objId,LiWeight);
                    pointList.Insert(i-1,middle); 
                    i++;
                }
                else // несколько объектов в ячейке
                {
                    point.Weight = RemoveWeight;
                    while (i< pointList.Count && cell.IsIn(pointList[i]))
                    {
                        pointList[i].Weight=RemoveWeight;
                        i++;
                    }
                     
                    var index = cell.Intersections.mapDatas.FindIndex(t=>t.Id==objId);
                    var list=cell.Intersections.ModelOfNineIntersections[index];
                    foreach(var matrix in list)
                    {
                        if(matrix.PointIntesection!= null)
                        { 
                            var newPoint =  new MapPoint( matrix.PointIntesection.X,
                                matrix.PointIntesection.Y, objId, LiWeight);
                            pointList.Insert(i-1, newPoint); i++;//!!!
                        }

                    }
                }
            }               
        }
    }
}
