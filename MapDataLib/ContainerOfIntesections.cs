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
            switch (mapData.Geometry)
            {
                case GeometryType.Line:
                    CountLines++;
                    break;
                case GeometryType.Point:
                    CountPoints++;
                    break;
                case GeometryType.Polygon:
                    CountPolygons++;
                    break;
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
