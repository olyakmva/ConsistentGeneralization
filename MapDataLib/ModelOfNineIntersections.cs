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
               matrixofnineintersections[0, 1] == false &&
               matrixofnineintersections[1, 0] == false)
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
                    PointLine();
                    break;
                case GeometryType.Line when mapDataColumn.Geometry == GeometryType.Line:
                    LineLine();
                    break;
                case GeometryType.Line when mapDataColumn.Geometry == GeometryType.Polygon:
                    LinePolygon();
                    break;
                case GeometryType.Polygon when mapDataColumn.Geometry == GeometryType.Point:
                    PointPolygon();
                    break;
                case GeometryType.Polygon when mapDataColumn.Geometry == GeometryType.Line:
                    LinePolygon();
                    break;
                case GeometryType.Polygon when mapDataColumn.Geometry == GeometryType.Polygon:
                    PolygonPolygon();
                    break;
            }
        }

        //Когда происходит проверка пересечения точек, они либо совпадают, либо не пересекаются
        public void PointPoint()
        {

        }
        public void PointLine()
        {

        }

        public void PointPolygon()
        {

        }

        public void LineLine()
        {

        }

        public void LinePolygon()
        {

        }

        public void PolygonPolygon()
        {

        }
    }
}
