using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesko
{
    class Program
    {  
        static void Main(string[] args)
        {
            Graph<int> g = new Graph<int>(new BFS<int>());
            evens even = new evens(g);
            odds odd = new odds(g);
            g.AddEdge(0, 1);
            g.AddEdge(0, 3);
            g.AddEdge(1, 4);
            g.AddEdge(1, 5);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(3, 6);
            g.AddEdge(4, 5);
            g.AddEdge(6, 7);
            g.AddEdge(6, 8);
            g.AddEdge(7, 8);

            Console.Write("BFS");
            foreach (var item in g)
            {
                Console.Write(item + " ");
            }

            g.bypassType = new DFS<int>();
            Console.Write("\n\nDFS");
            foreach (var item in g)
            {
                Console.Write(item + " ");
            }
            Console.ReadKey();

        }
    }
}
