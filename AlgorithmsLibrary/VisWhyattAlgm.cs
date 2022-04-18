using System;
using System.Collections.Generic;
using System.Linq;
using GeomObjectsLib;
using MapDataLib;

namespace AlgorithmsLibrary
{
    public abstract class VisWhyattAlgm : ISimplificationAlgm
    {
        public SimplificationAlgmParameters Options { get; set; }

        public void Run(Map map, GridLib.Grid grid)
        {
            foreach( var mapData in map.MapLayers)
            {
                Run(mapData);
            }
        }

        public virtual void Run(MapData map)
        {
            Options.Tolerance = Options.Tolerance * Options.Tolerance;
            foreach (var pair in map.MapObjDictionary)
            {
                var chain = pair.Value;
                int endIndex = chain.Count - 1;
                Run(ref chain, 0,  endIndex);
                map.MapObjDictionary[pair.Key] = chain;
            }
            Options.Parametr = Options.Tolerance;
        }

        private void Run(ref List<MapPoint> chain, int startIndex,  int endIndex)
        {
            if (endIndex - startIndex <= 2)
                return;
            
            LinkedList< MapPoint> list = new LinkedList<MapPoint>(chain);
            var heap = CreateHeap(chain, startIndex, endIndex);
            Process(heap, list);
            chain = list.ToList();
        }

        protected virtual void Process(UniqueHeap<double, MapPoint> heap, LinkedList<MapPoint> list)
        {
            var minWeightPoint = heap.GetMinElement(); 
            while (minWeightPoint.Key < Options.Tolerance)
            {
                var point = minWeightPoint.Value;
                heap.ExtractMinElement();
                var p = list.Find(point);
                if (p == null)
                    throw new ArgumentNullException("Point not found in LinkedList " + point);
                var nextPoint = p.Next;
                var prevPoint = p.Previous;
                list.Remove(p);
                CorrectPointWeight(heap, nextPoint);
                CorrectPointWeight(heap, prevPoint);
                minWeightPoint = heap.GetMinElement();
            }
        }

        protected UniqueHeap<double, MapPoint> CreateHeap(List<MapPoint> chain, int startIndex, int endIndex)
        {
            IComparer<double> comparer = Comparer<double>.Default; 
            UniqueHeap<double, MapPoint> heap = new UniqueHeap<double, MapPoint>(comparer, endIndex - startIndex);

            for (int i = startIndex + 1; i < endIndex; i++)
            {
                var t = new Triangle(chain[i - 1], chain[i], chain[i + 1]);
                var s = t.Square();
                if (s < double.Epsilon)
                {
                    if (chain[i].CompareTo(chain[i + 1]) == 0)
                    {
                        chain.RemoveAt(i);
                        endIndex--;
                    }
                    if (chain[i].CompareTo(chain[i - 1]) == 0)
                    {
                        chain.RemoveAt(i);
                        endIndex--;
                    }
                    continue;
                }
                chain[i].Weight = s;
                heap.Add(chain[i].Weight, chain[i]);
            }
            return heap;
        }

        protected void CorrectPointWeight(UniqueHeap<double, MapPoint> heap, LinkedListNode<MapPoint> pNode)
        {
            if(pNode == null)
                return;
            if(heap.Count==0)
                return;
            heap.Remove(pNode.Value);
            
            var prevNode = pNode.Previous;
            var nextNode = pNode.Next;
            if (prevNode == null || nextNode == null)
                return;
            var t = new Triangle(prevNode.Value, pNode.Value, nextNode.Value);
            pNode.Value.Weight = t.Square();
            heap.Add(pNode.Value.Weight, pNode.Value);
        }
    }

    public class VisWhyattAlgmWithTolerance : VisWhyattAlgm
    {
        protected override void Process(UniqueHeap<double, MapPoint> heap, LinkedList<MapPoint> list)
        {
            var minWeightPoint = heap.GetMinElement();
            while (minWeightPoint.Key < Options.Tolerance)
            {
                var point = minWeightPoint.Value;
                heap.ExtractMinElement();
                var p = list.Find(point);
                if (p == null)
                    throw new ArgumentNullException("Point not found in LinkedList " + point);
                var nextPoint = p.Next;
                var prevPoint = p.Previous;
                list.Remove(p);
                CorrectPointWeight(heap, nextPoint);
                CorrectPointWeight(heap, prevPoint);
                if (heap.Count > 0)
                {
                    minWeightPoint = heap.GetMinElement();
                }
                else break;
            }
        }

    }
}
