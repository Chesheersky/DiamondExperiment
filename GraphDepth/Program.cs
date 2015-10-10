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
            var min = int.MaxValue;
            foreach (var verty in graphy.Vertexes)
            {
                graphy.ResetMarks();
                CalculateVertDepth(verty, 0);
                var max = graphy.Vertexes.Max(x => x.Mark);
                min = (max < min) ? max : min;
            }

            return min;
        }

        private void CalculateVertDepth(Vertex verty, int mark)
        {
            verty.Mark = mark;

            foreach (var related in verty.Related.Where(x => x.Mark > mark))
                CalculateVertDepth(related, mark + 1);
        }
    }

    class Vertex
    {
        public Vertex()
        {
            Related = new List<Vertex>();
            Reset();
        }

        public List<Vertex> Related { get; set; }
        public int Id { get; set; }
        public int Mark { get; set; }

        public void Bind(Vertex other)
        {
            Related.Add(other);
        }

        internal void Reset()
        {
            Mark = int.MaxValue;
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

        internal void ResetMarks()
        {
            foreach (var vertex in Vertexes)
                vertex.Reset();
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
