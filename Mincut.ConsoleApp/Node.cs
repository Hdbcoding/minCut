using System.Collections.Generic;

namespace MinCut.ConsoleApp
{
    public class Node
    {
        public int Id { get; set; }
        public HashSet<int> ContractedNodes { get; set; } = new HashSet<int>();
        public List<int> Edges { get; set; }

        public Node(int id, List<int> edges)
        {
            Id = id;
            Edges = edges;
            ContractedNodes = new HashSet<int>(new[] { id });
        }
    }
}
