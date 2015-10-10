using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDepth
{

    class DepthCalculator
    {
        public int CalculateMinimalGraphDepth(Graph graphy)
        {
            var minDepth = 0;

            while (graphy.Vertexes.Count > 1)
            {
                Console.WriteLine("Count: {0} Minimal Depth: {1}", graphy.Vertexes.Count, minDepth);

                var markedForDeletion = graphy.Vertexes
                                              .Where(v => v.Value.Count() < 2)
                                              .Select(v => v.Key)
                                              .ToArray();
                foreach (var marked in markedForDeletion)
                    graphy.RemoveVertex(marked);

                minDepth++;
            }

            return minDepth;
        }
    }

    class Graph
    {
        public Graph()
        {
            Vertexes = new Dictionary<int, List<int>>();
        }

        public Dictionary<int, List<int>> Vertexes
        {
            get;
            set;
        }

        public void AddEdge(string input)
        {
            var vs = input.Split(' ');
            int xi = int.Parse(vs[0]); // the ID of a person which is adjacent to yi
            int yi = int.Parse(vs[1]); // the ID of a person which is adjacent to xi

            AddEdge(xi, yi);
        }

        private void AddEdge(int xi, int yi)
        {
            if (!Vertexes.ContainsKey(xi))
                Vertexes[xi] = new List<int>();
            Vertexes[xi].Add(yi);

            if (!Vertexes.ContainsKey(yi))
                Vertexes[yi] = new List<int>();
            Vertexes[yi].Add(xi);
        }

        public void RemoveVertex(int vertex)
        {
            foreach (var related in Vertexes[vertex])
                Vertexes[related].Remove(vertex);

            Vertexes.Remove(vertex);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var graphy = new Graph();
            var edges = Input.input.Split('\n').Skip(1);
            foreach (var edge in edges)
                graphy.AddEdge(edge);

            var result = new DepthCalculator().CalculateMinimalGraphDepth(graphy);

            Console.WriteLine("minimal depth: {0}", result);
            Console.ReadKey();
        }
    }

}
