using System.Collections.Generic;
using System.Linq;

namespace MapDataLib
{
    public class ContainerOfIntersections
    {
        /// <summary>
        /// Тут будут храниться матрицы 9 пересечений
        /// </summary>
        public List<List<ModelOfNineIntersections>> ModelOfNineIntersections;
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
        private int FindIndex(int objId)
        {
            return mapDatas.FindIndex(t => t.Id == objId);
        }

        public List<ModelOfNineIntersections> GetListsOfModels(int objId)
        {
            int index = FindIndex(objId);
            var list = ModelOfNineIntersections[index];
            return list;
        }

        public List<MapPoint> GetIntersectionPoints(int objId)
        {
            var list =  GetListsOfModels(objId);
            var result = new List<MapPoint>();
            foreach(var matrix in list)
            {
                if(matrix.PointIntesection!=null)
                    result.Add(matrix.PointIntesection);
            }
            return result.Distinct().ToList();
        }


    }
}
