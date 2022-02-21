using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MapDataLib
{
    // Данные слоя карты. В списке хранятся точки на карте  
    // слой одной геометрии 
    [Serializable]
    public class MapData
    {
        public Dictionary<int, List<MapPoint>> Vertexes { get; }
        public GeometryType Geometry { get; }
        public int Count => GetAllVertices().Count;

        public MapData( GeometryType type)
        {
            Vertexes = new Dictionary<int, List<MapPoint>>();
            Geometry = type;
        }
        
        public List<MapPoint> GetAllVertices()
        {
            var resultList = new List<MapPoint>();
            foreach (var objPair in Vertexes)
            {
                resultList.AddRange(objPair.Value);
            }
            return resultList;
        }
        
        public MapData Clone()
        {
            MapData clone;
            var bf = new BinaryFormatter();
            using (Stream fs = new FileStream("temp.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, this);
            }
            using (Stream fs = new FileStream("temp.bin", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                clone = (MapData)bf.Deserialize(fs);
            }
            return clone;
        }
        
        public void ClearWeights()
        {
            foreach (var chain in Vertexes)
            {
                foreach (var vertex in chain.Value)
                {
                    vertex.Weight = 1;
                }
            }
        }
    }
}
