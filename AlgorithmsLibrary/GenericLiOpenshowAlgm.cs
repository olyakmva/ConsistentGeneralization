using MapDataLib;
using System;
using GridLib;


namespace AlgorithmsLibrary
{
    /// <summary>
    /// Обобщенный алгоритм Ли-Опеншоу: на карту наброшена сетка 
    /// и оставляются только одна точка в ячейке сетки
    /// </summary>
    public class GenericLiOpenshowAlgm:ISimplificationAlgm
    {
        private const double LiWeight = 200;
        private const double RightAngleWeight = -90;
        private double _cellSize;
        private double _shift;
        public SimplificationAlgmParameters Options { get; set; }

        public GenericLiOpenshowAlgm(Map map, Grid grid)
        {

        }
        public void Run(Map map)
        {
            throw new NotImplementedException();
        }
    }
}
