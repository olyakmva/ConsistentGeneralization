using System.Collections.Generic;


namespace MapDataLib
{
    public class MapObjItem
    {
        public int Id {get; set;}
        public List<MapPoint> Points { get; set;}
        public GeometryType Geometry { get; set; }
    }
}
