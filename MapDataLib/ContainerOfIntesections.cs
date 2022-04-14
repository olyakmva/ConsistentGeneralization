using System.Collections.Generic;
 

namespace MapDataLib
{
    public class ContainerOfIntersections
    {
        /// <summary>
        /// Тут будут храниться матрицы 9 пересечений
        /// </summary>
        List<List<ModelOfNineIntersections>> ModelOfNineIntersections;
        /// <summary>
        /// Тут хранятся все объекты
        /// </summary>
        public List<MapObjItem> mapDatas {get; }
        /// <summary>
        /// Количество линий
        /// </summary>
        public int CountLines = 0;
        /// <summary>
        /// Количество точек
        /// </summary>
        public int CountPoints = 0;
        /// <summary>
        /// Количество полигонов
        /// </summary>
        public int CountPolygons = 0;

        public ContainerOfIntersections()
        {
            ModelOfNineIntersections = new List<List<ModelOfNineIntersections>>();
            mapDatas = new List<MapObjItem>();
        }
        
        public void Add(MapObjItem mapData)
        {
            if (mapData.Geometry == GeometryType.Line)
            {
                CountLines++;
            }
            else if (mapData.Geometry == GeometryType.Point)
            {
                CountPoints++;
            }
            else if (mapData.Geometry == GeometryType.Polygon)
            {
                CountPolygons++;
            }
            mapDatas.Add(mapData);
            List<ModelOfNineIntersections> list = new List<ModelOfNineIntersections>(); 
            for (int i = 0; i < mapDatas.Count-1; i++)
            {
                ModelOfNineIntersections m = new ModelOfNineIntersections(mapDatas[i],mapData);
                ModelOfNineIntersections[i].Add(m);
            }
            for (int j = 0; j < mapDatas.Count; j++)
            {
                ModelOfNineIntersections m = new ModelOfNineIntersections(mapData, mapDatas[j]);
                list.Add(m);
            }
            ModelOfNineIntersections.Add(list);
        }

    }
}
