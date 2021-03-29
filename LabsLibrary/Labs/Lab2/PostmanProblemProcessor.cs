using LabsLibrary.Common;
using System;
using System.Collections.Generic;

namespace LabsLibrary.Labs.Lab2
{
    public class PostmanProblemProcessor
    {
        private List<List<int>> adj;
        private int V;
        int[][] weight;
        int count = 0;
        public List<Edge> minEdges = new List<Edge>();

        public PostmanProblemProcessor(int v, int[][] w)
        {
            V = v;
            weight = w;
            adj = new List<List<int>>(v);

            for (int i = 0; i < v; i++)
            {
                adj.Add(new List<int>());
            }

            for (int i = 0; i < v; i++)
            {
                for (int j = i + 1; j < v; j++)
                {
                    if (w[i][j] != 0)
                    {
                        AddEdge(i, j);
                    }
                }
            }
        }  

        public void SolvePostmanProblem()
        {
            Test();
            PrintEulerTour();
        }

        public void AddMinEdge(int u, int v, int w)
        {
            Edge edge = new Edge(u, v, w);
            minEdges.Add(edge);
        }

        public void AddEdge(int v, int w)
        {
            adj[v].Add(w);
            adj[w].Add(v);
        }

        public void RemoveEdge(int u, int v)
        {
            adj[u].Remove(v);
            adj[v].Remove(u);
        }

        int DFSCount(int v, bool[] visited)
        {
            int count = 1;
            visited[v] = true;
            adj[v].ForEach(x =>
            {
                if (visited[x] == false)
                {
                    count += weight[v][x];
                    count += DFSCount(x, visited);
                }
            });
            return count;
        }

        public bool IsValidNextEdge(int u, int v)
        {
            if (adj[u].Count == 1)
            {
                return true;
            }
            else
            {
                bool[] visited = new bool[V];
                for (int i = 0; i < visited.Length; i++)
                {
                    visited[i] = false;
                }

                var count1 = DFSCount(u, visited);

                RemoveEdge(u, v);
                bool[] visted2 = new bool[V];
                for (int i = 0; i < visted2.Length; i++)
                {
                    visted2[i] = false;
                }
                var count2 = DFSCount(u, visted2);
                AddEdge(u, v);
                return (count1 > count2) ? false : true;
            }
        }

        public void PrintEulerUtil(int u)
        {
            try
            {
                foreach (var x in adj[u])
                {
                    if (IsValidNextEdge(u, x))
                    {
                        AddMinEdge(u, x, weight[u][x]);
                        count += weight[u][x];
                        RemoveEdge(u, x);
                        PrintEulerUtil(x);
                    }
                }
            }
            catch (Exception) { }
        }

        public void PrintEulerTour()
        {
            int u = 0;
            for (int i = 0; i < V; i++)
            {
                if (adj[i].Count % 2 != 0)
                {
                    u = i;
                    break;
                }
            }
            this.PrintEulerUtil(u);
        }

        private void Test()
        {
            int res = isEulerian();
            if (res == 0)
            {
                Console.WriteLine("\nThe graph is not an Eulerian.");
            }
            else if (res == 1)
            {
                Console.WriteLine("\nThe graph has an Eulerian path.");
            }
            else
            {
                Console.WriteLine("\nThe graph has an Eulerian cycle.");
            }
        }

        int isEulerian()
        {
            int odd = 0;
            for (int i = 0; i < V; i++)
            {
                if (adj[i].Count % 2 != 0)
                {
                    odd++;
                }
            }

            if (odd > 2)
            {
                return 0;
            }
            return (odd == 2) ? 1 : 2;
        }

    }
}