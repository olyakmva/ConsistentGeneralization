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
        List<MapData> mapDatas;

        public ContainerOfIntersections()
        {
            ModelOfNineIntersections = new List<List<ModelOfNineIntersections>>();
            mapDatas = new List<MapData>();
        }
        
        public void Add(MapData mapData)
        {
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
