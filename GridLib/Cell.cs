using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmsLibrary;
using MapDataLib;

namespace GridLib
{
    public class Cell
    {
        public List<int> ObjectIdList { get; set; }
        public MapPoint LowerLeftPoint { get; set; }
        public double Size { get; set; }
        public int Level { get; set; }
        public List<Cell> Children { get; set; }
        public CellState State { get;  set; }
        public Dictionary<int, List<MapPoint>> MapPoints { get; set; }

        public Cell()
        {
            MapPoints = new Dictionary<int, List<MapPoint>>();
            State = CellState.EmptyCell;
            ObjectIdList = new List<int>();
        }

        public void AddChildren()
        {
            if (Children != null) return;
            Children = new List<Cell>
            {
                new Cell
                {
                    Size = Size / 2, LowerLeftPoint = new MapPoint {X = LowerLeftPoint.X, Y = LowerLeftPoint.Y},
                    Level = Level - 1,
                    State = CellState.EmptyCell
                },
                new Cell
                {
                    Size = Size / 2, LowerLeftPoint = new MapPoint {X = LowerLeftPoint.X + Size / 2, Y = LowerLeftPoint.Y},
                    Level = Level - 1,
                    State = CellState.EmptyCell
                },
                new Cell
                {
                    Size = Size / 2, LowerLeftPoint = new MapPoint {X = LowerLeftPoint.X, Y = LowerLeftPoint.Y + Size / 2},
                    Level = Level - 1,
                    State = CellState.EmptyCell
                },
                new Cell
                {
                    Size = Size / 2, LowerLeftPoint = new MapPoint {X = LowerLeftPoint.X + Size / 2, Y = LowerLeftPoint.Y + Size / 2},
                    Level = Level - 1,
                    State = CellState.EmptyCell
                }
            };
        }

        public void Add(MapPoint point, int id)
        {
            if (!ObjectIdList.Contains(id))
            {
                ObjectIdList.Add(id);
            }
            if (MapPoints.ContainsKey(id))
            {
                if (!MapPoints[id].Contains(point))
                    MapPoints[id].Add(point);
            }
            else
            {
                MapPoints.Add(id, new List<MapPoint>(new[] {point}));
            }
            if (State == CellState.EmptyCell)
            {
                State = CellState.OneObject;
            }

            if (ObjectIdList.Count > 1)
                State = CellState.SeveralObjects;
        }

        public void AddToChildren(MapPoint point)
        {
            foreach (var childCell in Children.Where(childCell => childCell.IsIn(point)))
            {
                if (childCell.State == CellState.EmptyCell)
                {
                    childCell.MapPoints.Add(point.Id, new List<MapPoint>(new[] {point}));
                    childCell.ObjectIdList.Add(point.Id);
                    childCell.State = CellState.OneObject;
                }
                else 
                {
                    if (childCell.ObjectIdList.Contains(point.Id))
                    {
                        childCell.MapPoints[point.Id].Add(point);
                    }
                    else
                    {
                        childCell.MapPoints.Add(point.Id, new List<MapPoint>(new[] {point}));
                        childCell.ObjectIdList.Add(point.Id);
                        childCell.State = CellState.SeveralObjects;
                    }
                }
            }
        }

        public void AddToChildren(MapPoint point1, MapPoint point2, int id)
        {
            foreach (var childCell in Children.Where(childCell => childCell.HasCommonPoint(point1,point2)))
            {
                var (pnt1, pnt2) = childCell.GetIntersectionPoints(point1, point2);
                if (!childCell.MapPoints.ContainsKey(id))
                {
                    childCell.MapPoints.Add(id,
                        pnt2 != null ? new List<MapPoint> {pnt1, pnt2} : new List<MapPoint> {pnt1});
                }
                else
                {
                    int ind1 =childCell.MapPoints[id].FindIndex(p => p.CompareTo(pnt1) > 0);
                    if(ind1 >=0)
                        childCell.MapPoints[id].Insert(ind1,pnt1);
                    else childCell.MapPoints[id].Add(pnt1);
                    if (pnt2 != null)
                    {
                        int ind2 = childCell.MapPoints[id].FindIndex(p => p.CompareTo(pnt2) < 0);
                        if (ind2 >= 0)
                            childCell.MapPoints[id].Insert(ind2, pnt2);
                        else childCell.MapPoints[id].Add(pnt2);
                    }
                }

                if (!childCell.ObjectIdList.Contains(id))
                {
                    childCell.ObjectIdList.Add(id);
                }
                if (childCell.State == CellState.EmptyCell)
                {
                    childCell.State = CellState.OneObject;
                }
                else
                {
                    if (childCell.ObjectIdList.Count>1)
                        childCell.State = CellState.SeveralObjects;
                }
            }
        }

        public IEnumerable<Cell> GetChildrenCellsWithThisObject(int objId)
        {
            return Children.FindAll(c => c.ObjectIdList.Contains(objId));
        }
        public List<Cell> GetChildrenWithManyObjects()
        {
            return Children.FindAll(c => c.State == CellState.SeveralObjects);
        }

        public bool IsIn(MapPoint point)
        {
            if ((point.X >= LowerLeftPoint.X) &&
                (point.X < LowerLeftPoint.X + Size) &&
                point.Y >= LowerLeftPoint.Y &&
                point.Y < LowerLeftPoint.Y + Size)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Cell> GetAllCells()
        {
            var result = new List<Cell>();
            if (Children == null) 
                result.Add(this);
            else
            {
                foreach (var cell in Children)
                {
                    result.AddRange(cell.GetAllCells());
                }
            }
            return result;
        }

        public IEnumerable<Cell> GetAllChildCellsWithObject(int objId)
        {
            return GetAllCells().Where(t => t.ObjectIdList.Contains(objId));
        }

        public IEnumerable<MapPoint> GetPoints()
        {
            List<MapPoint> points = new List<MapPoint> {LowerLeftPoint};
            points.AddRange(new []
            {
                new MapPoint {X = LowerLeftPoint.X+Size, Y= LowerLeftPoint.Y},
                new MapPoint {X = LowerLeftPoint.X+Size, Y= LowerLeftPoint.Y+Size},
                new MapPoint {X = LowerLeftPoint.X, Y= LowerLeftPoint.Y+Size}
            });
            return points;
        }

        public bool HasCommonPoint(Line line)
        {
            var cellPoints = GetPoints();
            int count = cellPoints.Count(point => line.GetSign(point) > 0);
            return count != 0 && count != 4;
        }

        public bool HasCommonPoint(MapPoint point1, MapPoint point2)
        {
            var line = new Line(point1, point2);
            if (!HasCommonPoint(line)) return false;
            var minX = Math.Min(point1.X, point2.X);
            var maxX = Math.Max(point1.X, point2.X);
            var minY = Math.Min(point1.Y, point2.Y);
            var maxY = Math.Max(point1.Y, point2.Y);
            if ((minX <= LowerLeftPoint.X + Size && maxX >= LowerLeftPoint.X) &&
                (minY <= LowerLeftPoint.Y + Size && maxY >= LowerLeftPoint.Y))
            {
                return true;
            }
            return false;
        }

        public (MapPoint, MapPoint) GetIntersectionPoints(MapPoint point1, MapPoint point2)
        {
            var line = new Line(point1, point2);
            var cellPoints = GetPoints().ToArray();
            var ptsList = new List<MapPoint>();
            for (int i = 0; i < cellPoints.Length; i++)
            {
                if (line.GetSign(cellPoints[i]) * line.GetSign(cellPoints[(i + 1) % 4]) > 0) continue;
                if (line.GetSign(cellPoints[i]) == 0)
                {
                    ptsList.Add(cellPoints[i]);
                    continue;
                }
                if (line.GetSign(cellPoints[(i + 1) % 4]) == 0)
                {
                    ptsList.Add(cellPoints[(i + 1) % 4]);
                    continue;
                }
                if ((i % 2) == 0)
                {
                    var x = -1 * (line.B * cellPoints[i].Y + line.C) / line.A;
                    MapPoint p = new MapPoint(x, cellPoints[i].Y, cellPoints[i].Id, cellPoints[i].Weight + 10);
                    if ((p.CompareTo(point1) < 0 && p.CompareTo(point2) > 0) ||
                        (p.CompareTo(point1) > 0 && p.CompareTo(point2) < 0))
                        ptsList.Add(p);
                    
                }
                else
                {
                    var y = -1 * (line.A * cellPoints[i].X + line.C) / line.B;
                    MapPoint p = new MapPoint(cellPoints[i].X, y, cellPoints[i].Id, cellPoints[i].Weight + 10);
                    if ((p.CompareTo(point1) < 0 && p.CompareTo(point2) > 0) ||
                        (p.CompareTo(point1) > 0 && p.CompareTo(point2) < 0))
                        ptsList.Add(p);
                }
            }

            ptsList.Sort();
            if (ptsList.Count > 1)
                return (ptsList[0], ptsList[1]);
            if (ptsList.Count == 1)
                return (ptsList[0], null);
            throw new ApplicationException("No intersection points inside cell");
        }

    }
}
