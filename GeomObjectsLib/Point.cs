using System;

namespace GeomObjectsLib
{
    [Serializable]
    public class Point
    {
         public double X { set; get; }
         public double Y { set; get; }
        public double DistanceToVertex(Point v)
        {
                return Math.Sqrt(Math.Pow(X - v.X, 2) + Math.Pow(Y - v.Y, 2));
        }
    }
}
