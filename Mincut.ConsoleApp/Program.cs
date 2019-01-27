using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinCut.ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string inputFile = "kargerMinCut.txt";
            int minCut = CalculateMinCut(inputFile);
            Console.WriteLine(minCut);
        }

        public static List<Node> ParseGraph(string inputFile)
        {
            List<Node> graph = new List<Node>();
            foreach (string line in File.ReadLines(inputFile))
            {
                List<int> values = line.Split('\t', ' ')
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .Select(int.Parse)
                    .ToList();
                int id = values[0];
                values.RemoveAt(0);
                Node node = new Node(id, values);
                graph.Add(node);
            }
            return graph;
        }

        public static int CalculateRandomCut(List<Node> graph)
        {
            Random r = new Random();
            while (graph.Count > 2)
            {
                int nIndex = r.Next(graph.Count);
                Node node1 = graph[nIndex];
                int eIndex = r.Next(node1.Edges.Count);
                int edgeId = node1.Edges[eIndex];
                Node node2 = graph.Single(n => n.Id == edgeId);

                Contract(graph, node1, node2);
            }
            return graph[0].Edges.Count;
        }

        private static void Contract(List<Node> graph, Node node1, Node node2)
        {
            //clone edges of node1
            List<int> edges = node1.Edges.Select(n => n).ToList();
            //add all edges of node2 to node1's set of edges
            edges.AddRange(node2.Edges);
            //remove self-references
            edges = edges.Where(n => n != node1.Id && n != node2.Id).ToList();
            //add node2 to node1's contracted set
            node1.ContractedNodes = node1.ContractedNodes.Union(node2.ContractedNodes).ToHashSet();
            //re-label neighbors of node2 to point to node1 instead
            IEnumerable<int> neighborEdges = node2.Edges.Where(n => n != node1.Id).Distinct();
            foreach (int edgeId in neighborEdges)
            {
                Node neighbor = graph.Single(n => n.Id == edgeId);
                for (int i = 0; i < neighbor.Edges.Count; i++)
                {
                    if (neighbor.Edges[i] == node2.Id)
                    {
                        neighbor.Edges[i] = node1.Id;
                    }
                }
            }
            node1.Edges = edges;
            //remove node2 from the graph
            graph.Remove(node2);
        }

        public static int CalculateMinCut(string inputFile)
        {
            int minCut = int.MaxValue;
            int nNodes = File.ReadAllLines(inputFile).Length;
            int trials = (int)Math.Pow(nNodes, 2);
            if (trials > 2000)
            {
                trials = 2000;
            }

            while (trials-- > 0)
            {
                List<Node> graph = ParseGraph(inputFile);
                int cut = CalculateRandomCut(graph);
                if (cut < minCut)
                {
                    minCut = cut;
                }
            }
            return minCut;
        }
    }
}
