using MapDataLib;
using System.Collections.Generic;
using Xunit;

namespace TestOfContainerIntersections
{
    public class UnitTest1
    {
        [Fact]
        public void AfterCreationModelOfNineIntersectionsMatrixIsExist()
        {
            ModelOfNineIntersections model = new ModelOfNineIntersections();
            int expected = 3;
            bool flag = false;
            Assert.Equal(model.matrixofnineintersections.GetLength(0), expected);
            Assert.Equal(model.matrixofnineintersections.GetLength(1), expected);
            for (int i = 0; i < expected; i++)
            {
                for (int j = 0; j < expected; j++)
                {
                    Assert.Equal(model.matrixofnineintersections[i, j], flag);
                }
            }
        }
        #region PointPoint
        [Fact]
        public void EqualsPointPointModelOfNineIntersections()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(2,2,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(2,2,3,3)
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreEquals());
            Assert.True(model.ObjectsAreContains());
            Assert.True(model.ObjectsAreCovers());
            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreCoveredBy());
            Assert.True(model.ObjectsAreWithin());

            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }

        [Fact]
        public void DisjointPointPointModelOfNineIntersections()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(2,2,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(2,1,3,3)
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);
            model.ObjectsAreEquals();

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
        }
        #endregion

        #region LinePoint
        [Fact]
        public void IntersectLinePointModelOfNineIntersections()
        {
            var md1 = new MapData(GeometryType.Line);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(2,2,1,1),
                new MapPoint(4,4,1,1),
                new MapPoint(5,5,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(3,3,3,3)
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreContains());
            Assert.True(model.ObjectsAreCovers());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }

        [Fact]
        public void DisjointLinePointModelOfNineIntersections()
        {
            var md1 = new MapData(GeometryType.Line);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(2,2,1,1),
                new MapPoint(4,4,1,1),
                new MapPoint(5,5,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,3,3)
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());

        }
        #endregion

        #region PointLine
        [Fact]
        public void IntersectPointLineModelOfNineIntersections()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(3,3,3,3)
            });
            var md2 = new MapData(GeometryType.Line);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(2,2,1,1),
                new MapPoint(4,4,1,1),
                new MapPoint(5,5,1,1)
            });

            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreCoveredBy());
            Assert.True(model.ObjectsAreWithin());

            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }

        [Fact]
        public void DisjointPointLineModelOfNineIntersections()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,3,3)
            });
            var md2 = new MapData(GeometryType.Line);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(2,2,1,1),
                new MapPoint(4,4,1,1),
                new MapPoint(5,5,1,1)
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());

        }
        #endregion

        #region PolygonWithLeftAndRightOrderPoint
        [Fact]
        public void IntersectPolygonPointModelOfNineIntersectionsRightOrder()
        {
            var md1 = new MapData(GeometryType.Polygon);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(0,1,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(1,0,1,1),
                new MapPoint(0,0,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0.5,0.5,1,1),
            });

            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreContains());
            Assert.True(model.ObjectsAreCovers());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }

        [Fact]
        public void IntersectPolygonPointModelOfNineIntersectionsLeftOrder()
        {
            var md1 = new MapData(GeometryType.Polygon);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(1,0,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(0,1,1,1),
                new MapPoint(0,0,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0.5,0.5,1,1),
            });

            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreContains());
            Assert.True(model.ObjectsAreCovers());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }

        [Fact]
        public void DisjointPolygonPointModelOfNineIntersectionsRightOrder()
        {
            var md1 = new MapData(GeometryType.Polygon);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(0,1,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(1,0,1,1),
                new MapPoint(0,0,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(-2,-2,1,1),
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());

        }
        [Fact]
        public void DisjointPolygonPointModelOfNineIntersectionsLeftOrder()
        {
            var md1 = new MapData(GeometryType.Polygon);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(1,0,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(0,1,1,1),
                new MapPoint(0,0,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(-2,-2,1,1),
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());

        }
        #endregion

        #region PolygonWithLeftAndRightOrderPointNotConvex
        [Fact]
        public void IntersectPolygonPointModelOfNineIntersectionsRightOrderNotConvex()
        {
            var md1 = new MapData(GeometryType.Polygon);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(0,2,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(2,2,1,1),
                new MapPoint(2,0,1,1),
                new MapPoint(0,0,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0.5,0.5,1,1),
            });

            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreContains());
            Assert.True(model.ObjectsAreCovers());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }

        [Fact]
        public void IntersectPolygonPointModelOfNineIntersectionsLeftOrderNotConvex()
        {
            var md1 = new MapData(GeometryType.Polygon);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(2,0,1,1),
                new MapPoint(2,2,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(0,2,1,1),
                new MapPoint(0,0,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0.5,0.5,1,1),
            });

            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreContains());
            Assert.True(model.ObjectsAreCovers());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }

        [Fact]
        public void DisjointPolygonPointModelOfNineIntersectionsRightOrderNotConvex()
        {
            var md1 = new MapData(GeometryType.Polygon);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(0,2,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(2,2,1,1),
                new MapPoint(2,0,1,1),
                new MapPoint(0,0,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(1,1.75,1,1),
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());

        }
        [Fact]
        public void DisjointPolygonPointModelOfNineIntersectionsLeftOrderNotConvex()
        {
            var md1 = new MapData(GeometryType.Polygon);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(2,0,1,1),
                new MapPoint(2,2,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(0,2,1,1),
                new MapPoint(0,0,1,1)
            });
            var md2 = new MapData(GeometryType.Point);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(1,1.75,1,1),
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());

        }
        #endregion

        #region PointPolygonWithLeftAndRightOrder
        [Fact]
        public void IntersectPointPolygonModelOfNineIntersectionsRightOrder()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0.5,0.5,1,1),
            });
            var md2 = new MapData(GeometryType.Polygon);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(0,1,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(1,0,1,1),
                new MapPoint(0,0,1,1)
            });


            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreCoveredBy());
            Assert.True(model.ObjectsAreWithin());

            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }
        [Fact]
        public void IntersectPointPolygonModelOfNineIntersectionsLeftOrder()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0.5,0.5,1,1),
            });
            var md2 = new MapData(GeometryType.Polygon);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(1,0,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(0,1,1,1),
                new MapPoint(0,0,1,1)
            });


            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreCoveredBy());
            Assert.True(model.ObjectsAreWithin());

            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }


        [Fact]
        public void DisjointPointPolygonModelOfNineIntersectionsRightOrder()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(-2,-2,1,1),
            });

            var md2 = new MapData(GeometryType.Polygon);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(0,1,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(1,0,1,1),
                new MapPoint(0,0,1,1)
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());

        }
        [Fact]
        public void DisjointPointPolygonModelOfNineIntersectionsLeftOrder()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(-2,-2,1,1),
            });

            var md2 = new MapData(GeometryType.Polygon);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(1,0,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(0,1,1,1),
                new MapPoint(0,0,1,1)
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());

        }
        #endregion

        #region PointPolygonWithLeftAndRightOrderNotConvex
        [Fact]
        public void IntersectPointPolygonModelOfNineIntersectionsRightOrderNotConvex()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0.5,0.5,1,1),
            });
            var md2 = new MapData(GeometryType.Polygon);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(0,2,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(2,2,1,1),
                new MapPoint(2,0,1,1),
                new MapPoint(0,0,1,1)
            });


            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreCoveredBy());
            Assert.True(model.ObjectsAreWithin());

            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }
        [Fact]
        public void IntersectPointPolygonModelOfNineIntersectionsLeftOrderNotConvex()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0.5, 0.5,1,1),
            });
            var md2 = new MapData(GeometryType.Polygon);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(2,0,1,1),
                new MapPoint(2,2,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(0,2,1,1),
                new MapPoint(0,0,1,1)
            });


            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreIntersects());
            Assert.True(model.ObjectsAreCoveredBy());
            Assert.True(model.ObjectsAreWithin());

            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());
            Assert.False(model.ObjectsAreDisjoint());

        }


        [Fact]
        public void DisjointPointPolygonModelOfNineIntersectionsRightOrderNotConvex()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(1,1.75,1,1),
            });

            var md2 = new MapData(GeometryType.Polygon);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(0,2,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(2,2,1,1),
                new MapPoint(2,0,1,1),
                new MapPoint(0,0,1,1)
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());

        }
        [Fact]
        public void DisjointPointPolygonModelOfNineIntersectionsLeftOrderNotConvex()
        {
            var md1 = new MapData(GeometryType.Point);
            md1.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(1,1.75,1,1),
            });

            var md2 = new MapData(GeometryType.Polygon);
            md2.MapObjDictionary.Add(1, new List<MapPoint>()
            {
                new MapPoint(0,0,1,1),
                new MapPoint(2,0,1,1),
                new MapPoint(2,2,1,1),
                new MapPoint(1,1,1,1),
                new MapPoint(0,2,1,1),
                new MapPoint(0,0,1,1)
            });
            ModelOfNineIntersections model = new ModelOfNineIntersections(md1, md2);

            Assert.True(model.ObjectsAreDisjoint());

            Assert.False(model.ObjectsAreCoveredBy());
            Assert.False(model.ObjectsAreWithin());
            Assert.False(model.ObjectsAreIntersects());
            Assert.False(model.ObjectsAreContains());
            Assert.False(model.ObjectsAreCovers());
            Assert.False(model.ObjectsAreEquals());
            Assert.False(model.ObjectsAreMeets());

        }
        #endregion
    }
}