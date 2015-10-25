using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;


namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();
            calculator.Initialize();
            calculator.FindSolutions();
            calculator.PrintTheBestOne();

            Console.ReadKey();
        }


    }


    class Calculator
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int[,] Matrix { get; set; }

        public void Initialize()
        {
            Width = 7;
            Height = 5;

            Matrix = new int[Width, Height];
            var lines = new string[]
            {                
                "2..2.1." ,
                ".3..5.3" ,
                ".2.1..." ,
                "2...2.." ,
                ".1....2" ,
            };
            for (int i = 0; i < Height; i++)
            {
                var line = lines[i];
                for (int j = 0; j < Width; j++)
                {
                    Matrix[j, i] = (line[j] == '.') ? 0 : int.Parse(line[j].ToString());
                }
            }
        }

        IEnumerable<Node> GetInitial()
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    if (Matrix[i, j] > 0)
                        yield return new Node { X = i, Y = j, InitialHunger = Matrix[i, j] };
        }

        private Graph BuildTheBase()
        {
            var initial = new Graph();

            initial.Nodes.AddRange(GetInitial());
            SolveObvious(initial, hope);
            return initial;
        }

        private static void SolveObvious(Graph graph)
        {
            var hope = true;

            while (hope && !graph.IsComplete())
            {
                hope = false;
                foreach (var node in graph.Nodes)
                {
                    var available = graph.GetAvailable(node);
                    if (node.IsPerfect(available))
                    {
                        foreach (var an in available)
                        {
                            while (an.HungerFor(node) > 0
                                && node.Hunger > 0)
                                graph.Connect(node, an);
                        }
                        hope = true;
                    }
                }
            }=
        }

        public static Graph DeepClone(Graph obj)
        {
            object objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return (Graph)objResult;
        }

        private Graph Dig(Graph theBase)
        {

            foreach (var node in theBase.Nodes)
            {
                var variants = GetVariants(node);
                foreach (var variant in variants)
                {
                    var copy = DeepClone(theBase);
                    var result = Dig(copy);
                    if (result == null)
                        continue;
                    SolveObious(result);
                    if (result.IsComplete)
                        return result;
                }
            }

        }

        public void FindSolutions()
        {
            var theBase = BuildTheBase();

            if (theBase.IsComplete())
                Solution = theBase;
            else
                Solution = Dig(theBase);
        }

        Graph Solution { get; set; }

        public void PrintTheBestOne()
        {
            Solution.Print();
        }
    }

    class Variant
    {
        public Variant()
        {
            Ways = new Dictionary<Node, int>();
        }
        public Dictionary<Node, int> Ways { get; set; }
    }

    [Serializable()]
    class Graph
    {
        public Graph()
        {
            Nodes = new List<Node>();
            Connections = new List<Connection>();
        }

        public List<Node> Nodes { get; set; }
        public List<Connection> Connections { get; set; }

        public void Connect(Node first, Node second)
        {
            var connection = first.Connect(second);
            Connections.Add(connection);
        }

        public IEnumerable<Node> GetAvailable(Node node)
        {
            var byX = Nodes.Where(candidate => candidate.X == node.X);
            var byY = Nodes.Where(candidate => candidate.Y == node.Y);

            var result = new[]{
                byY.Where(c=>c.X>node.X).OrderBy(c=>Math.Abs(c.X-node.X)).FirstOrDefault(),
                byY.Where(c=>c.X<node.X).OrderBy(c=>Math.Abs(c.X-node.X)).FirstOrDefault(),
                byX.Where(c=>c.Y>node.Y).OrderBy(c=>Math.Abs(c.Y-node.Y)).FirstOrDefault(),
                byX.Where(c=>c.Y<node.Y).OrderBy(c=>Math.Abs(c.Y-node.Y)).FirstOrDefault(),
            };

            return result.Where(x => x != null);
        }

        public void Print()
        {
            foreach (var connection in Connections)
                connection.Print();
        }

        public bool IsComplete()
        {
            return Nodes.All(x => x.IsSatisfied);
        }
    }

    [Serializable()]
    class Node
    {
        public Node()
        {
            Connections = new List<Connection>();
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int InitialHunger { get; set; }
        public List<Connection> Connections { get; set; }


        public int Hunger { get { return InitialHunger - Connections.Count; } }
        public bool IsSatisfied
        {
            get { return Connections.Count == Hunger; }
        }

        public int HungerFor(Node node)
        {
            var allowedConnections = 2 - Connections.Where(c => c.Contains(node)).Count();
            return Math.Min(Hunger, allowedConnections);
        }

        public bool IsPerfect(IEnumerable<Node> available)
        {
            var hungerForThis = available.Sum(x => x.HungerFor(this));
            return this.Hunger > 0
                && (hungerForThis == this.Hunger
                || (hungerForThis > this.Hunger && available.Count() == 1));
        }

        public bool CanConnect(Node other)
        {
            return Connections.Where(x => x.Contains(other)).Count() <= 2;
        }

        public Connection Connect(Node other)
        {
            if (this.CanConnect(other))
            {
                var connection = new Connection
                {
                    First = this,
                    Second = other
                };
                Connections.Add(connection);
                other.Connections.Add(connection);
                return connection;
            }
            else
            {
                Console.WriteLine("[!!!] Wrong attempt to connect: {0} {1} {2} {3}",
                    this.X,
                    this.Y,
                    other.X,
                    other.Y);
                return null;
            }
        }

    }

    [Serializable()]
    class Connection
    {
        public Node First { get; set; }
        public Node Second { get; set; }
        public void Print()
        {
            Console.WriteLine("{0} {1} {2} {3} 1", First.X, First.Y, Second.X, Second.Y);
        }

        public bool Contains(Node node)
        {
            return First == node || Second == node;
        }
    }

}
