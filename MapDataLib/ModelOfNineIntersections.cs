﻿using System;
using System.Linq;
using System.Runtime.Serialization;

namespace MapDataLib
{
    public class ModelOfNineIntersections
    {
        public bool[,] matrixofnineintersections;
        MapObjItem MapObjItemLine;
        MapObjItem MapObjItemColumn;
        public ModelOfNineIntersections()
        {
            matrixofnineintersections = new bool[3, 3];
        }

        public ModelOfNineIntersections(MapObjItem mapDataLine, MapObjItem mapDataColumn)
        {
            matrixofnineintersections = new bool[3, 3];
            this.MapObjItemLine = mapDataLine;
            this.MapObjItemColumn = mapDataColumn;
            CalculatingIntersections();
        }
        #region TypesOfIntersections
        //Объекты равны
        // T*F 
        // **F
        // FF*
        public bool ObjectsAreEquals()
        {
            if (matrixofnineintersections[0, 0] == true &&
                matrixofnineintersections[0, 2] == false &&
                matrixofnineintersections[1, 2] == false &&
                matrixofnineintersections[2, 0] == false &&
                matrixofnineintersections[2, 1] == false)
                return true;
            else
            {
                return false;
            }
        }

        //Объекты не пересекаются
        // FF* 
        // FF*
        // ***
        public bool ObjectsAreDisjoint()
        {
            if (matrixofnineintersections[0, 0] == false &&
                matrixofnineintersections[0, 1] == false &&
                matrixofnineintersections[1, 0] == false &&
                matrixofnineintersections[1, 1] == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Объекты касаются
        // FT*        F**        F**   
        // ***   OR   T**   OR   *T*
        // ***        ***        ***
        public bool ObjectsAreMeets()
        {
            if (matrixofnineintersections[0, 0] == false && (
                matrixofnineintersections[0, 1] == true ||
                matrixofnineintersections[1, 0] == true ||
                matrixofnineintersections[1, 1] == true))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Объект содержится в другом
        // T**
        // ***
        // FF*

        public bool ObjectsAreContains()
        {
            if (matrixofnineintersections[0, 0] == true &&
               matrixofnineintersections[2, 1] == false &&
               matrixofnineintersections[2, 0] == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Объект покрывает другой
        // T**        *T*        ***        ***
        // ***   OR   ***   OR   T**   OR   *T*
        // FF*        FF*        FF*        FF*
        public bool ObjectsAreCovers()
        {
            if (matrixofnineintersections[2, 0] == false &&
               matrixofnineintersections[2, 1] == false && (
               matrixofnineintersections[0, 0] == true ||
               matrixofnineintersections[0, 1] == true ||
               matrixofnineintersections[1, 0] == true ||
               matrixofnineintersections[1, 1] == true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //объекты пересекаются
        // T**        *T*        ***        ***
        // ***   OR   ***   OR   T**   OR   *T*
        // ***        ***        ***        ***
        public bool ObjectsAreIntersects()
        {
            if (matrixofnineintersections[0, 0] == true ||
               matrixofnineintersections[0, 1] == true ||
               matrixofnineintersections[1, 0] == true ||
               matrixofnineintersections[1, 1] == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //1 объект в другом
        // T*F    
        // **F   
        // *** 
        public bool ObjectsAreWithin()
        {
            if (matrixofnineintersections[0, 0] == true &&
               matrixofnineintersections[0, 2] == false &&
               matrixofnineintersections[1, 2] == false)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //объекты пересекаются
        // T*F        *TF        **F        **F
        // **F   OR   **F   OR   T*F   OR   *TF
        // ***        ***        ***        ***
        public bool ObjectsAreCoveredBy()
        {
            if ((matrixofnineintersections[0, 0] == true
                || matrixofnineintersections[0, 1] == true
                || matrixofnineintersections[1, 0] == true
                || matrixofnineintersections[1, 1] == true)
                && (matrixofnineintersections[0, 2] == false &&
             matrixofnineintersections[1, 2] == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        public void CalculatingIntersections()
        {
            switch (MapObjItemLine.Geometry)
            {
                case GeometryType.Point when MapObjItemColumn.Geometry == GeometryType.Point:
                    PointPoint();
                    break;
                case GeometryType.Point when MapObjItemColumn.Geometry == GeometryType.Line:
                    PointLine();
                    break;
                case GeometryType.Point when MapObjItemColumn.Geometry == GeometryType.Polygon:
                    PointPolygon();
                    break;
                case GeometryType.Line when MapObjItemColumn.Geometry == GeometryType.Point:
                    LinePoint();
                    break;
                case GeometryType.Line when MapObjItemColumn.Geometry == GeometryType.Line:
                    LineLine();
                    break;
                case GeometryType.Line when MapObjItemColumn.Geometry == GeometryType.Polygon:
                    LinePolygon();
                    break;
                case GeometryType.Polygon when MapObjItemColumn.Geometry == GeometryType.Point:
                    PolygonPoint();
                    break;
                case GeometryType.Polygon when MapObjItemColumn.Geometry == GeometryType.Line:
                    PolygonLine();
                    break;
                case GeometryType.Polygon when MapObjItemColumn.Geometry == GeometryType.Polygon:
                    PolygonPolygon();
                    break;
            }
        }

        //Когда происходит проверка пересечения точек, они либо совпадают, либо не пересекаются

        #region PointWithOther
        public void PointPoint()
        {
            bool flag = false;

            var x1 = MapObjItemLine.Points[0].X;
            var y1 = MapObjItemLine.Points[0].Y;

            var x2 = MapObjItemColumn.Points[0].X;
            var y2 = MapObjItemColumn.Points[0].Y;
            if (x1 == x2 && y1 == y2)
            {
                flag = true;
            }

            if (flag)
            {
                matrixofnineintersections[0, 0] = true;
            }

        }
        public void PointLine()
        {
            bool flag = false;

            var x = MapObjItemLine.Points[0].X;
            var y = MapObjItemLine.Points[0].Y;

            for (int i = 1; i < MapObjItemColumn.Points.Count; i++)
            {
                double x1 = MapObjItemColumn.Points[i].X;
                double x2 = MapObjItemColumn.Points[i - 1].X;
                double y1 = MapObjItemColumn.Points[i].Y;
                double y2 = MapObjItemColumn.Points[i - 1].Y;

                double minx = Math.Min(x1, x2);
                double miny = Math.Min(y1, y2);

                double maxx = Math.Max(x1, x2);
                double maxy = Math.Max(y1, y2);

                if ((x - x1) / (x2 - x1) - (y - y1) / (y2 - y1) < double.Epsilon && x >= minx && x <= maxx && y >= miny && y <= maxy)
                {
                    flag = true;
                }
                if (((x - x1) * (y2 - y1)) == ((y - y1) * (x2 - x1)) && x >= minx && x <= maxx && y >= miny && y <= maxy)
                {
                    flag = true;
                }
            }

            if (flag)
            {
                matrixofnineintersections[0, 0] = true;
                matrixofnineintersections[2, 0] = true;
            }
        }
        public void PointPolygon()
        {
            bool flag = false;

            var x = MapObjItemLine.Points[0].X;
            var y = MapObjItemLine.Points[0].Y;

            for (int i = 1; i < MapObjItemColumn.Points.Count; i++)
            {
                double x1 = MapObjItemColumn.Points[i].X;
                double x2 = MapObjItemColumn.Points[i - 1].X;
                double y1 = MapObjItemColumn.Points[i].Y;
                double y2 = MapObjItemColumn.Points[i - 1].Y;

                double minx = Math.Min(x1, x2);
                double miny = Math.Min(y1, y2);

                double maxx = Math.Max(x1, x2);
                double maxy = Math.Max(y1, y2);

                //if ((x - x1) / (x2 - x1) - (y - y1) / (y2 - y1) < double.Epsilon && x >= minx && x <= maxx && y >= miny && y <= maxy)
                //{
                //    flag = true;
                //}
                if (((x - x1) * (y2 - y1)) == ((y - y1) * (x2 - x1)) && x >= minx && x <= maxx && y >= miny && y <= maxy)
                {
                    flag = true;
                }
            }
            if (flag == true)
            {
                matrixofnineintersections[1, 1] = true;
                matrixofnineintersections[2, 0] = true;
                return;
            }
            int j = MapObjItemColumn.Points.Count - 1;
            for (int i = 0; i < MapObjItemColumn.Points.Count; i++)
            {
                if ((MapObjItemColumn.Points[i].Y < y && MapObjItemColumn.Points[j].Y >= y || MapObjItemColumn.Points[j].Y < y && MapObjItemColumn.Points[i].Y >= y) &&
                     (MapObjItemColumn.Points[i].X + (y - MapObjItemColumn.Points[i].Y) / (MapObjItemColumn.Points[j].Y - MapObjItemColumn.Points[i].Y) * (MapObjItemColumn.Points[j].X - MapObjItemColumn.Points[i].X) < x))
                {
                    flag = !flag;
                }
                j = i;
            }

            if (flag)
            {
                matrixofnineintersections[0, 0] = true;
                matrixofnineintersections[2, 0] = true;
            }
        }
        #endregion

        #region LineWithOther
        public void LinePoint()
        {
            bool flag = false;

            var x = MapObjItemColumn.Points[0].X;
            var y = MapObjItemColumn.Points[0].Y;

            for (int i = 1; i < MapObjItemLine.Points.Count; i++)
            {
                double x1 = MapObjItemLine.Points[i].X;
                double x2 = MapObjItemLine.Points[i - 1].X;
                double y1 = MapObjItemLine.Points[i].Y;
                double y2 = MapObjItemLine.Points[i - 1].Y;

                double minx = Math.Min(x1, x2);
                double miny = Math.Min(y1, y2);

                double maxx = Math.Max(x1, x2);
                double maxy = Math.Max(y1, y2);

                //if ((x - x1) / (x2 - x1) - (y - y1) / (y2 - y1) < double.Epsilon && x >= minx && x <= maxx && y >= miny && y <= maxy)
                //{
                //    flag = true;
                //}
                if (((x - x1) * (y2 - y1)) == ((y - y1) * (x2 - x1)) && x >= minx && x <= maxx && y >= miny && y <= maxy)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                matrixofnineintersections[0, 0] = true;
                matrixofnineintersections[0, 2] = true;
            }
        }
        public void LineLine()
        {

            for (int i = 1; i < MapObjItemLine.Points.Count; i++)
            {
                var a1 = MapObjItemLine.Points[i - 1].X;
                var b1 = MapObjItemLine.Points[i - 1].Y;
                var a2 = MapObjItemLine.Points[i].X;
                var b2 = MapObjItemLine.Points[i].Y;
                MapPoint mapPoint1 = new MapPoint(a1, b1, 1, 1);
                MapPoint mapPoint2 = new MapPoint(a2, b2, 1, 1);
                LineMode l1 = new LineMode(mapPoint1, mapPoint2);

                for (int j = 1; j < MapObjItemColumn.Points.Count; j++)
                {
                    double x1 = MapObjItemColumn.Points[j].X;
                    double x2 = MapObjItemColumn.Points[j - 1].X;
                    double y1 = MapObjItemColumn.Points[j].Y;
                    double y2 = MapObjItemColumn.Points[j - 1].Y;
                    MapPoint mapPoint3 = new MapPoint(x1, y1, 1, 1);
                    MapPoint mapPoint4 = new MapPoint(x2, y2, 1, 1);
                    LineMode l2 = new LineMode(mapPoint3, mapPoint4);
                    var point = l1.GetIntersectionPoint(l2);

                    var maxx1 = Math.Max(a1, a2);
                    var minx1 = Math.Min(a1, a2);
                    var maxy1 = Math.Max(b1, b2);
                    var miny1 = Math.Min(b1, b2);

                    var maxx2 = Math.Max(x1, x2);
                    var minx2 = Math.Min(x1, x2);
                    var maxy2 = Math.Max(y1, y2);
                    var miny2 = Math.Min(y1, y2);
                    if (point != null)
                    {
                        if (point.X < maxx1 && point.X < maxx2 &&
                        point.X > minx1 && point.X > minx2 &&
                        point.Y < maxy1 && point.Y < maxy2 &&
                        point.Y > miny1 && point.Y > miny2)
                        {
                            var x = point.X;
                            var y = point.Y;
                            Vector vector1 = new Vector(a1 - x, b1 - y);
                            Vector vector2 = new Vector(x1 - x, y1 - y);
                            Vector vector3 = new Vector(x2 - x, y2 - y);
                            Vector vector4 = new Vector(a2 - x, b2 - y);

                            var q1 = Vector.AngleOfVectors(vector2, vector1);
                            var q2 = Vector.AngleOfVectors(vector3, vector1);
                            var q3 = Vector.AngleOfVectors(vector4, vector1);

                            var p1 = Math.Max(q2, q1);
                            var p2 = Math.Min(q1, q2);
                            if (q3 > p2 && q3 < p1)
                            {
                                matrixofnineintersections[0, 0] = true;
                                matrixofnineintersections[0, 2] = true;
                                matrixofnineintersections[2, 0] = true;
                                matrixofnineintersections[2, 2] = true;
                                return;
                            }
                            else
                            {
                                matrixofnineintersections[1, 1] = true;
                                matrixofnineintersections[0, 2] = true;
                                matrixofnineintersections[2, 0] = true;
                                return;
                            }

                        }
                        else if (point.X == a1 && point.Y == b1 || point.X == a2 && point.Y == b2 || point.X == x1 && point.Y == y1 || point.X == x2 && point.Y == y2)
                        {
                            if (i != MapObjItemLine.Points.Count - 1)
                            {
                                var tmpx = MapObjItemLine.Points[i + 1].X;
                                var tmpy = MapObjItemLine.Points[i + 1].Y;
                                var x = point.X;
                                var y = point.Y;
                                Vector vector1 = new Vector(a1 - x, b1 - y);
                                Vector vector2 = new Vector(x1 - x, y1 - y);
                                Vector vector3 = new Vector(x2 - x, y2 - y);
                                Vector vector4 = new Vector(tmpx - x, tmpy - y);

                                var q1 = Vector.AngleOfVectors(vector2, vector1);
                                var q2 = Vector.AngleOfVectors(vector3, vector1);
                                var q3 = Vector.AngleOfVectors(vector4, vector1);

                                var p1 = Math.Max(q2, q1);
                                var p2 = Math.Min(q1, q2);
                                if (q3 > p2 && q3 < p1)
                                {
                                    matrixofnineintersections[0, 0] = true;
                                    matrixofnineintersections[0, 2] = true;
                                    matrixofnineintersections[2, 0] = true;
                                    matrixofnineintersections[2, 2] = true;
                                    return;
                                }
                                else
                                {
                                    matrixofnineintersections[1, 1] = true;
                                    matrixofnineintersections[0, 2] = true;
                                    matrixofnineintersections[2, 0] = true;
                                    return;
                                }
                            }
                            else if (j != MapObjItemColumn.Points.Count - 1)
                            {
                                var tmpx = MapObjItemColumn.Points[j + 1].X;
                                var tmpy = MapObjItemColumn.Points[j + 1].Y;
                                var x = point.X;
                                var y = point.Y;
                                Vector vector1 = new Vector(a1 - x, b1 - y);
                                Vector vector2 = new Vector(a2 - x, b2 - y);
                                Vector vector3 = new Vector(x2 - x, y2 - y);
                                Vector vector4 = new Vector(tmpx - x, tmpy - y);

                                var q1 = Vector.AngleOfVectors(vector2, vector1);
                                var q2 = Vector.AngleOfVectors(vector3, vector1);
                                var q3 = Vector.AngleOfVectors(vector4, vector1);

                                var p1 = Math.Max(q2, q1);
                                var p2 = Math.Min(q1, q2);
                                if ((q3 >0 && p2>0 && q3<180 && p2<180) || (q3>180 && p2>180 && q3<360 && p2<360))
                                {
                                    matrixofnineintersections[1, 1] = true;
                                    matrixofnineintersections[0, 2] = true;
                                    matrixofnineintersections[2, 0] = true;
                                    return; 
                                }
                                else
                                {
                                    matrixofnineintersections[0, 0] = true;
                                    matrixofnineintersections[0, 2] = true;
                                    matrixofnineintersections[2, 0] = true;
                                    matrixofnineintersections[2, 2] = true;
                                    return;
                                }
                            }
                            else
                            {
                                matrixofnineintersections[1, 1] = true;
                                matrixofnineintersections[0, 2] = true;
                                matrixofnineintersections[2, 0] = true;
                                return;
                            }
                        }
                    }
                }
            }
        }
        public void LinePolygon()
        {

        }
        #endregion

        #region PolygonWithOther
        public void PolygonPoint()
        {
            bool flag = false;

            var x = MapObjItemColumn.Points[0].X;
            var y = MapObjItemColumn.Points[0].Y;

            for (int i = 1; i < MapObjItemLine.Points.Count; i++)
            {
                double x1 = MapObjItemLine.Points[i].X;
                double x2 = MapObjItemLine.Points[i - 1].X;
                double y1 = MapObjItemLine.Points[i].Y;
                double y2 = MapObjItemLine.Points[i - 1].Y;

                double minx = Math.Min(x1, x2);
                double miny = Math.Min(y1, y2);

                double maxx = Math.Max(x1, x2);
                double maxy = Math.Max(y1, y2);

                //if ((x - x1) / (x2 - x1) - (y - y1) / (y2 - y1) < double.Epsilon && x >= minx && x <= maxx && y >= miny && y <= maxy)
                //{
                //    flag = true;
                //}
                if (((x - x1) * (y2 - y1)) == ((y - y1) * (x2 - x1)) && x >= minx && x <= maxx && y >= miny && y <= maxy)
                {
                    flag = true;
                }
            }
            if (flag == true)
            {
                matrixofnineintersections[1, 1] = true;
                matrixofnineintersections[0, 2] = true;
                return;
            }
            int j = MapObjItemLine.Points.Count - 1;
            for (int i = 0; i < MapObjItemLine.Points.Count; i++)
            {
                if ((MapObjItemLine.Points[i].Y < y && MapObjItemLine.Points[j].Y >= y || MapObjItemLine.Points[j].Y < y && MapObjItemLine.Points[i].Y >= y) &&
                     (MapObjItemLine.Points[i].X + (y - MapObjItemLine.Points[i].Y) / (MapObjItemLine.Points[j].Y - MapObjItemLine.Points[i].Y) * (MapObjItemLine.Points[j].X - MapObjItemLine.Points[i].X) < x))
                {
                    flag = !flag;
                }
                j = i;
            }

            if (flag)
            {
                matrixofnineintersections[0, 0] = true;
                matrixofnineintersections[0, 2] = true;
            }
        }
        public void PolygonLine()
        {

        }
        public void PolygonPolygon()
        {

        }
        #endregion
    }

    #region Line
    [Serializable]
    public class LineCoefEqualsZeroException : Exception
    {
        public LineCoefEqualsZeroException(string message) : base(message)
        { }

        protected LineCoefEqualsZeroException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }
    }
    public class LineMode
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public LineMode(MapPoint v1, MapPoint v2)
        {
            A = v2.Y - v1.Y;
            B = (v1.X - v2.X);
            C = v1.Y * (-1) * B - v1.X * A;
            if (Math.Abs(A) < double.Epsilon && Math.Abs(B) < double.Epsilon && Math.Abs(C) < double.Epsilon)
            {
                throw new LineCoefEqualsZeroException(" коэффициенты уравнения прямой= нулю");
            }
        }
        public MapPoint GetIntersectionPoint(LineMode otherLine)
        {
            var delta = A * otherLine.B - B * otherLine.A;
            if (Math.Abs(delta) < double.Epsilon)
                return null;
            var delta1 = (-1 * C * otherLine.B + B * otherLine.C);
            var delta2 = A * otherLine.C * (-1) + otherLine.A * C;
            return new MapPoint(delta1 / delta, delta2 / delta, 1, 1);
        }

    }
    #endregion

    #region Vector
    struct Vector
    {
        public double X { get; }
        public double Y { get; }

        public Vector(double x, double y)
        {
            X = x; Y = y;
        }

        public static readonly Vector Reference = new Vector(1, 0);

        public static double AngleOfReference(Vector v)
            => NormalizeAngle(Math.Atan2(v.Y, v.X) / Math.PI * 180);

        public static double AngleOfVectors(Vector first, Vector second)
            => NormalizeAngle(AngleOfReference(first) - AngleOfReference(second));

        private static double NormalizeAngle(double angle)
        {
            bool CheckBottom(double a) => a >= 0;
            bool CheckTop(double a) => a < 360;

            double turn = CheckBottom(angle) ? -360 : 360;
            while (!(CheckBottom(angle) && CheckTop(angle))) angle += turn;
            return angle;
        }
    }
    #endregion

}
