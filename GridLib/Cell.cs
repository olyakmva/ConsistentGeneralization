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
            Children = new List<Cell>()
            {
                new Cell()
                {
                    Size = this.Size / 2, LowerLeftPoint = new MapPoint()
                        {X = this.LowerLeftPoint.X, Y = this.LowerLeftPoint.Y},
                    Level = this.Level - 1,
                    State = CellState.EmptyCell
                },
                new Cell()
                {
                    Size = this.Size / 2, LowerLeftPoint = new MapPoint()
                        {X = this.LowerLeftPoint.X + this.Size / 2, Y = this.LowerLeftPoint.Y},
                    Level = this.Level - 1,
                    State = CellState.EmptyCell
                },
                new Cell()
                {
                    Size = this.Size / 2, LowerLeftPoint = new MapPoint()
                        {X = this.LowerLeftPoint.X, Y = this.LowerLeftPoint.Y + this.Size / 2},
                    Level = this.Level - 1,
                    State = CellState.EmptyCell
                },
                new Cell()
                {
                    Size = this.Size / 2, LowerLeftPoint = new MapPoint()
                        {X = this.LowerLeftPoint.X + this.Size / 2, Y = this.LowerLeftPoint.Y + this.Size / 2},
                    Level = this.Level - 1,
                    State = CellState.EmptyCell
                }
            };
        }

        public void Add(MapPoint point)
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

        public void Add(Line line, int id)
        {
            foreach (var childCell in Children.Where(childCell => childCell.HasCommonPoint(line)))
            {
                if (childCell.State == CellState.EmptyCell)
                {
                    childCell.MapPoints.Add(id, new List<MapPoint>());
                    childCell.ObjectIdList.Add(id);
                    childCell.State = CellState.OneObject;
                }
                else
                {
                    if (childCell.ObjectIdList.Contains(id)) continue;
                    childCell.MapPoints.Add(id, new List<MapPoint>());
                    childCell.ObjectIdList.Add(id);
                    childCell.State = CellState.SeveralObjects;
                }
            }
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

        public IEnumerable<MapPoint> GetPoints()
        {
            List<MapPoint> points = new List<MapPoint> {LowerLeftPoint};
            points.AddRange(new []
            {
                new MapPoint(){X = LowerLeftPoint.X+Size, Y= LowerLeftPoint.Y},
                new MapPoint(){X = LowerLeftPoint.X+Size, Y= LowerLeftPoint.Y+Size},
                new MapPoint(){X = LowerLeftPoint.X, Y= LowerLeftPoint.Y+Size}
            });
            return points;
        }

        public bool HasCommonPoint(Line line)
        {
            var cellPoints = GetPoints();
            int count = 0;
            foreach (var point in cellPoints)
            {
                if (line.GetSign(point) > 0)
                    count++;
            }

            if (count == 0 || count == 4)
                return false;
            return true;
        }
    }
}
