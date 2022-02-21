using MapDataLib;

namespace AlgorithmsLibrary
{
    public interface ICriterion
    {
        void GetParamByCriterion(SimplificationAlgmParameters options);
        void Init(MapData initMap, SimplificationAlgmParameters options);
        bool IsSatisfy(MapData map);
    }
    public interface ISimplificationAlgm
    {
        SimplificationAlgmParameters Options { get; set; }
        void Run(MapData map);
    }

    public class SimplificationAlgmParameters
    {
        public double Tolerance { get; set; }
        public int OutScale { get; set; }
        public double OutParam { get; set; }
        public double PointNumberGap { get; set; }
       
        public double GhDistance { get; set; }
        
    }
}
