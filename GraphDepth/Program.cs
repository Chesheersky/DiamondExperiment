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

            while(graphy.Vertexes.Count > 1)
            {
                Console.WriteLine("Count: {0} Minimal Depth: {1}", graphy.Vertexes.Count, minDepth);
                foreach (var verty in graphy.Vertexes)
                    if (verty.Related.Count < 2)
                        verty.MarkedForDeletion = true;
                graphy.Shake();
                minDepth++;
            }

            return minDepth;
        }
    }

    class Vertex
    {
        public Vertex()
        {
            Related = new List<Vertex>();
        }

        public List<Vertex> Related { get; set; }
        public int Id { get; set; }
        public bool MarkedForDeletion { get; set; }

        public void Bind(Vertex other)
        {
            Related.Add(other);
        }
    }

    class Graph
    {
        public Graph()
        {
            Vertexes = new List<Vertex>();
        }

        public List<Vertex> Vertexes
        {
            get;
            set;
        }

        public void AddEdge(string input)
        {
            var vs = input.Split(' ');
            int xi = int.Parse(vs[0]); // the ID of a person which is adjacent to yi
            int yi = int.Parse(vs[1]); // the ID of a person which is adjacent to xi

            var X = GetVertex(xi);
            var Y = GetVertex(yi);

            BindVertexes(X, Y);
        }

        private void BindVertexes(Vertex X, Vertex Y)
        {
            X.Bind(Y);
            Y.Bind(X);
        }

        private Vertex GetVertex(int xi)
        {
            var result = Vertexes.FirstOrDefault(x => x.Id == xi);
            if (result == null)
                Vertexes.Add(result = new Vertex { Id = xi });

            return result;
        }

        internal void Shake()
        {
            var marked = Vertexes.Where(v => v.MarkedForDeletion).ToArray();
            foreach (var vertex in marked)
                RemoveVertex(vertex);
        }

        private void RemoveVertex(Vertex vertex)
        {
            foreach(var related in vertex.Related)
                related.Related.Remove(vertex);

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
