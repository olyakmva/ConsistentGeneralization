using MapDataLib;
using System.Linq;
using GridLib;
using System.Collections.Generic;
using GeomObjectsLib;

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
        private const double IntersectionObjWeight = 300; 
        public const int IntersectionCellWeight =50;
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
           // двигаться по точкам объекта. Если ячейка = один объект,
           // то получить среднюю точку, остальные пометить как удаляемые
            int i = 0;
            var stack = new Stack<MapPoint>();
            Cell prevCell=null;
            while (i < pointList.Count-1)
            {
                MapPoint point = pointList[i];
                var next = pointList[i+1];
                var startIndex = i;
                var cellList = grid.GetCellsBetweenPoints(point,next).ToList();
                if(cellList.Count>2)
                {
                    pointList.Insert(i+1, new MapPoint((point.X+next.X)/2, (point.Y+next.Y)/2,objId,RemoveWeight));
                    continue;
                }
                var currentCell = cellList[0];
                if(currentCell== prevCell)
                {
                    i++;
                    continue;
                }
                var cellPoints = new List<MapPoint>{point};
                if(i!=0  && point.Weight!=IntersectionObjWeight) 
                        point.Weight=RemoveWeight;
                 i++;
                if(cellList.Count==1)
                { // обе точки находятся в одной ячейке
                   
                    while (i< pointList.Count &&  currentCell.IsIn(pointList[i]))
                    {
                        cellPoints.Add(pointList[i]);
                        pointList[i].Weight=RemoveWeight;
                        i++; 
                    }
                    if(i<pointList.Count) 
                        next=pointList[i];// точкa не в этой ячейке
                }
                int lastIndex=i-1;
                var lastThisCellPoint = pointList[lastIndex];
                if( currentCell.State==CellState.SeveralObjects && currentCell.Intersections!=null)
                { 
                    var interPointsList = currentCell.Intersections.GetIntersectionPoints(objId);
                    if(interPointsList.Count>0)
                    {
                       interPointsList.Sort();
                       if (point.CompareTo (next)<0)
                                interPointsList.Reverse();
                       foreach( var inpnt in interPointsList)
                       {
                           var ind = cellPoints.FindIndex(c=> c.Equals(inpnt));
                           if (ind>-1)
                                cellPoints[ind].Weight = IntersectionObjWeight;
                           else
                           {  
                                var j = pointList.FindIndex(h=> h.Equals(inpnt));
                                if( j>-1)
                                    pointList[j].Weight=IntersectionObjWeight;
                                else
                                {
                                    for( var k= startIndex; k< lastIndex; k++)
                                    {
                                        if( pointList[k].CompareTo(inpnt)*
                                            pointList[k+1].CompareTo(inpnt) <0)
                                        {
                                            pointList.Insert(k+1,inpnt);
                                            pointList[k+1].Weight= IntersectionObjWeight;
                                            lastIndex++;
                                            break;
                                        }
                                    }                                   
                                }
                           }
                       }
                       if(stack.Count >0)
                       {
                           stack.Pop();
                       }                          
                    }
                }
                var pts =currentCell.MapPoints[objId];
                if( lastIndex>= pointList.Count-1)
                    break;
                var line = new Line(lastThisCellPoint, next);
                MapPoint p= pts.FindLast(t=> (line.GetSign(t)==0)&& t.Weight==IntersectionCellWeight );
                if(p== null)
                {
                    string msg =$"не найдена точка пересечения с ячейкой {currentCell} objId={objId}";
                    ErrorLog.WriteToLogFile(msg);
                    p= lastThisCellPoint;
                }
                if( stack.Count > 0)
                {
                    var p0 = stack.Pop();
                    var middlepnt = new MapPoint((p0.X+p.X)/2, (p0.Y+p.Y)/2,objId,LiWeight);
                    pointList.Insert(lastIndex+1, middlepnt);
                    i++;
                }
                stack.Push(p);
                prevCell=currentCell;
                i= pointList.FindIndex(t=> t.CompareTo(next)==0);
            }                
               
            pointList.RemoveAll(p=>p.Weight==RemoveWeight);
        }
    }
}
