using MapDataLib;

namespace AlgorithmsLibrary
{
    public interface ISimplificationAlgm
    {
        SimplificationAlgmParameters Options { get; set; }
        void Run(Map map, GridLib.Grid grid);
    }

    public class SimplificationAlgmParameters
    {
        public double Tolerance { get; set; }
        public int OutScale { get; set; }
        public double Parametr { get; set; }
                           
    }
}
