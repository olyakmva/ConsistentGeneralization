using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDataLib
{
    public class ModelOfNineIntersections
    {
        public bool[,] matrixofnineintersections;
        MapData mapDataLine;
        MapData mapDataColumn;
        public ModelOfNineIntersections()
        {
            matrixofnineintersections = new bool[3, 3];
        }
        public ModelOfNineIntersections(MapData mapDataLine, MapData mapDataColumn)
        {
            matrixofnineintersections = new bool[3, 3];
            this.mapDataLine = mapDataLine;
            this.mapDataColumn = mapDataColumn;
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
        #endregion

        public void CalculatingIntersections()
        {
            switch (mapDataLine.Geometry)
            {
                case GeometryType.Point when mapDataColumn.Geometry == GeometryType.Point:
                    PointPoint();
                    break;
                case GeometryType.Point when mapDataColumn.Geometry == GeometryType.Line:
                    PointLine();
                    break;
                case GeometryType.Point when mapDataColumn.Geometry == GeometryType.Polygon:
                    PointPolygon();
                    break;
                case GeometryType.Line when mapDataColumn.Geometry == GeometryType.Point:
                    LinePoint();
                    break;
                case GeometryType.Line when mapDataColumn.Geometry == GeometryType.Line:
                    LineLine();
                    break;
                case GeometryType.Line when mapDataColumn.Geometry == GeometryType.Polygon:
                    LinePolygon();
                    break;
                case GeometryType.Polygon when mapDataColumn.Geometry == GeometryType.Point:
                    PolygonPoint();
                    break;
                case GeometryType.Polygon when mapDataColumn.Geometry == GeometryType.Line:
                    PolygonLine();
                    break;
                case GeometryType.Polygon when mapDataColumn.Geometry == GeometryType.Polygon:
                    PolygonPolygon();
                    break;
            }
        }

        //Когда происходит проверка пересечения точек, они либо совпадают, либо не пересекаются
        public void PointPoint()
        {
            bool flag = false;

            var x1 = mapDataLine.MapObjDictionary.First().Value[0].X;
            var y1 = mapDataLine.MapObjDictionary.First().Value[0].Y;

            var x2 = mapDataColumn.MapObjDictionary.First().Value[0].X;
            var y2 = mapDataColumn.MapObjDictionary.First().Value[0].Y;

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

            var x = mapDataLine.MapObjDictionary.First().Value[0].X;
            var y = mapDataLine.MapObjDictionary.First().Value[0].Y;
            foreach (var q in mapDataColumn.MapObjDictionary.Keys)
            {
                for (int i = 1; i < mapDataColumn.MapObjDictionary[q].Count; i++)
                {
                    double x1 = mapDataColumn.MapObjDictionary[q][i].X;
                    double x2 = mapDataColumn.MapObjDictionary[q][i - 1].X;
                    double y1 = mapDataColumn.MapObjDictionary[q][i].Y;
                    double y2 = mapDataColumn.MapObjDictionary[q][i - 1].Y;

                    double minx = Math.Min(x1, x2);
                    double miny = Math.Min(y1, y2);

                    double maxx = Math.Max(x1, x2);
                    double maxy = Math.Max(y1, y2);

                    if ((x - x1) / (x2 - x1) - (y - y1) / (y2 - y1) < double.Epsilon && x >= minx && x <= maxx && y >= miny && y <= maxy)
                    {
                        flag = true;
                    }
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

            var x = mapDataLine.MapObjDictionary.First().Value[0].X;
            var y = mapDataLine.MapObjDictionary.First().Value[0].Y;
            foreach (var q in mapDataColumn.MapObjDictionary.Keys)
            {
                for (int i = 1; i < mapDataColumn.MapObjDictionary[q].Count; i++)
                {
                    double x1 = mapDataColumn.MapObjDictionary[q][i].X;
                    double x2 = mapDataColumn.MapObjDictionary[q][i - 1].X;
                    double y1 = mapDataColumn.MapObjDictionary[q][i].Y;
                    double y2 = mapDataColumn.MapObjDictionary[q][i - 1].Y;

                    double minx = Math.Min(x1, x2);
                    double miny = Math.Min(y1, y2);

                    double maxx = Math.Max(x1, x2);
                    double maxy = Math.Max(y1, y2);

                    if ((x - x1) / (x2 - x1) - (y - y1) / (y2 - y1) < double.Epsilon && x >= minx && x <= maxx && y >= miny && y <= maxy)
                    {
                        flag = true;
                    }
                }
            }
            if (flag == true)
            {
                matrixofnineintersections[0, 0] = true;
                matrixofnineintersections[2, 0] = true;
                return;
            }
            foreach (var q in mapDataColumn.MapObjDictionary.Keys)
            {
                int j = mapDataColumn.MapObjDictionary[q].Count-1;
                for (int i = 0; i < mapDataColumn.MapObjDictionary[q].Count; i++)
                {
                    if ((mapDataColumn.MapObjDictionary[q][i].Y < y && mapDataColumn.MapObjDictionary[q][j].Y >= y || mapDataColumn.MapObjDictionary[q][j].Y < y && mapDataColumn.MapObjDictionary[q][i].Y >= y) &&
                         (mapDataColumn.MapObjDictionary[q][i].X + (y - mapDataColumn.MapObjDictionary[q][i].Y) / (mapDataColumn.MapObjDictionary[q][j].Y - mapDataColumn.MapObjDictionary[q][i].Y) * (mapDataColumn.MapObjDictionary[q][j].X - mapDataColumn.MapObjDictionary[q][i].X) < x))
                    {
                        flag = !flag;
                    }
                    j = i;
                }
            }

            if (flag)
            {
                matrixofnineintersections[0, 0] = true;
                matrixofnineintersections[2, 0] = true;
            }
        }

        public void LinePoint()
        {
            bool flag = false;

            var x = mapDataColumn.MapObjDictionary.First().Value[0].X;
            var y = mapDataColumn.MapObjDictionary.First().Value[0].Y;

            foreach (var q in mapDataLine.MapObjDictionary.Keys)
            {
                for (int i = 1; i < mapDataLine.MapObjDictionary[q].Count; i++)
                {
                    double x1 = mapDataLine.MapObjDictionary[q][i].X;
                    double x2 = mapDataLine.MapObjDictionary[q][i - 1].X;
                    double y1 = mapDataLine.MapObjDictionary[q][i].Y;
                    double y2 = mapDataLine.MapObjDictionary[q][i - 1].Y;

                    double minx = Math.Min(x1, x2);
                    double miny = Math.Min(y1, y2);

                    double maxx = Math.Max(x1, x2);
                    double maxy = Math.Max(y1, y2);

                    if ((x - x1) / (x2 - x1) - (y - y1) / (y2 - y1) < double.Epsilon && x >= minx && x <= maxx && y >= miny && y <= maxy)
                    {
                        flag = true;
                    }
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

        }
        public void LinePolygon()
        {

        }

        public void PolygonPoint()
        {
            bool flag = false;

            var x = mapDataColumn.MapObjDictionary.First().Value[0].X;
            var y = mapDataColumn.MapObjDictionary.First().Value[0].Y;
            foreach (var q in mapDataLine.MapObjDictionary.Keys)
            {
                for (int i = 1; i < mapDataLine.MapObjDictionary[q].Count; i++)
                {
                    double x1 = mapDataLine.MapObjDictionary[q][i].X;
                    double x2 = mapDataLine.MapObjDictionary[q][i - 1].X;
                    double y1 = mapDataLine.MapObjDictionary[q][i].Y;
                    double y2 = mapDataLine.MapObjDictionary[q][i - 1].Y;

                    double minx = Math.Min(x1, x2);
                    double miny = Math.Min(y1, y2);

                    double maxx = Math.Max(x1, x2);
                    double maxy = Math.Max(y1, y2);

                    if ((x - x1) / (x2 - x1) - (y - y1) / (y2 - y1) < double.Epsilon && x >= minx && x <= maxx && y >= miny && y <= maxy)
                    {
                        flag = true;
                    }
                }
            }
            if (flag == true)
            {
                matrixofnineintersections[0, 0] = true;
                matrixofnineintersections[2, 0] = true;
                return;
            }
            foreach (var q in mapDataLine.MapObjDictionary.Keys)
            {
                int j = mapDataLine.MapObjDictionary[q].Count - 1;
                for (int i = 0; i < mapDataLine.MapObjDictionary[q].Count; i++)
                {
                    if ((mapDataLine.MapObjDictionary[q][i].Y < y && mapDataLine.MapObjDictionary[q][j].Y >= y || mapDataLine.MapObjDictionary[q][j].Y < y && mapDataLine.MapObjDictionary[q][i].Y >= y) &&
                         (mapDataLine.MapObjDictionary[q][i].X + (y - mapDataLine.MapObjDictionary[q][i].Y) / (mapDataLine.MapObjDictionary[q][j].Y - mapDataLine.MapObjDictionary[q][i].Y) * (mapDataLine.MapObjDictionary[q][j].X - mapDataLine.MapObjDictionary[q][i].X) < x))
                    {
                        flag = !flag;
                    }
                    j = i;
                }
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

    }
}
