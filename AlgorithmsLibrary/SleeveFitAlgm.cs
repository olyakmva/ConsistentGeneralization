using System;
using System.Collections.Generic;
using MapDataLib;


namespace AlgorithmsLibrary
{
    public class SleeveFitAlgm : ISimplificationAlgm
    {
        public SimplificationAlgmParameters Options { get; set; }

        public virtual void Run(MapData map)
        {
            foreach (var pair in map.MapObjDictionary)
            {
                var chain = pair.Value;
                Process(chain);
            }
            Options.OutParam = Options.Tolerance;
        }
        void Process(List<MapPoint> chain)
        {
            var i = 0;
            while (i < chain.Count - 1)
            {
                var indStart = i;
                var indEnd = ExstractLineSegment(chain, indStart);
                i = indEnd;
                chain[indStart].Weight = 2;
                chain[indEnd].Weight = 2;
            }
            chain.RemoveAll(point => point.Weight < 2);
        }
        private int ExstractLineSegment(List<MapPoint> chain, int indStart)
        {
            int indEnd = indStart + 2;

            bool isLine = true;
            while (indEnd < chain.Count && isLine)
            {
                Line line;
                if (indEnd < chain.Count && chain[indStart].CompareTo(chain[indEnd]) == 0)
                {
                    break;
                }
                try
                {
                    line = new Line(chain[indStart], chain[indEnd]);
                }
                catch (LineCoefEqualsZeroException e)
                {
                    throw new ApplicationException(e.Message);
                }

                for (int j = indStart + 1; j < indEnd; j++)
                {
                    var dist = line.GetDistance(chain[j]);
                    if (dist > Options.Tolerance)
                    {
                        isLine = false;
                        indEnd--;
                        break;
                    }
                }
                indEnd++;
            }
            // конец прямолинейного участка
            indEnd--;
            return indEnd;
        }
    }
}
