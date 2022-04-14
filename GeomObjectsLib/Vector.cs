using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeomObjectsLib
{
    public class Vector
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
}
