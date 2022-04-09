using MapDataLib;

namespace AlgorithmsLibrary
{
    public interface ISimplificationAlgm
    {
        SimplificationAlgmParameters Options { get; set; }
        void Run(Map map);
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
