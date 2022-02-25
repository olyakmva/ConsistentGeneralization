using System.Collections.Generic;
using System.Linq;
using MapDataLib;


namespace GridLib
{
    public class Cell
    {
        public int ObjectId { get; set; }
        public MapPoint LowerLeftPoint { get; set; }
        public double Size { get; set; }
        public int Level { get; set; }
        public List<Cell> Children { get; set; }
        public CellState State { get;  set; }

        private const int SeveralObjectsInCell = -1;

        public Dictionary<int, List<MapPoint>> MapPoints { get; set; }

        public Cell()
        {
            MapPoints = new Dictionary<int, List<MapPoint>>();
            State = CellState.EmptyCell;
        }

        public void Add(MapPoint point)
        {
            if (Level == 0)
            {
                ObjectId = SeveralObjectsInCell;
                if(MapPoints.ContainsKey(point.Id))
                    MapPoints[point.Id].Add(point);
                else MapPoints.Add(point.Id, new List<MapPoint>(new []{point}));
                return;
            }
            if (Children == null )
            {
                Children = new List<Cell>()
                {
                    new Cell()
                    {
                        Size = this.Size / 2, LowerLeftPoint = new MapPoint()
                            {X = this.LowerLeftPoint.X, Y = this.LowerLeftPoint.Y},
                        Level = this.Level-1 
                    },
                    new Cell()
                    {
                        Size = this.Size / 2, LowerLeftPoint = new MapPoint()
                            {X = this.LowerLeftPoint.X + this.Size / 2, Y = this.LowerLeftPoint.Y},
                        Level = this.Level-1
                    },
                    new Cell()
                    {
                        Size = this.Size / 2, LowerLeftPoint = new MapPoint()
                            {X = this.LowerLeftPoint.X , Y = this.LowerLeftPoint.Y+ this.Size / 2},
                        Level = this.Level-1
                    },
                    new Cell()
                    {
                        Size = this.Size / 2, LowerLeftPoint = new MapPoint()
                            {X = this.LowerLeftPoint.X + this.Size / 2, Y = this.LowerLeftPoint.Y+ this.Size / 2},
                        Level = this.Level-1
                    }
                };
                foreach (var childCell in Children.Where(childCell => childCell.IsIn(point)))
                {
                    childCell.MapPoints.Add(point.Id, new List<MapPoint>(new []{point}));
                    childCell.ObjectId = point.Id;
                    break;
                }

                this.ObjectId = SeveralObjectsInCell;
                foreach (var pair in MapPoints)
                {
                    foreach (var pnt in pair.Value)
                    {
                        foreach (var childCell in Children.Where(childCell => childCell.IsIn(pnt)))
                        {
                            if (childCell.ObjectId == pnt.Id && childCell.MapPoints.ContainsKey(pnt.Id))
                            {
                                childCell.MapPoints[pnt.Id].Add(pnt);
                            }
                            else childCell.Add(pnt);
                        }
                    }
                }
            }
            else
            {
                foreach (var childCell in Children.Where(childCell => childCell.IsIn(point)))
                {
                    if (childCell.ObjectId == point.Id)
                    {
                        childCell.MapPoints[point.Id].Add(point);
                    }
                    else childCell.Add(point);
                }
            }
        }

        private bool IsIn(MapPoint point)
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
    }
}
