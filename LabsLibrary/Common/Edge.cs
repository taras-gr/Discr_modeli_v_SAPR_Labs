using System;
using System.Collections.Generic;
using System.Linq;

namespace LabsLibrary.Common
{
    public class Edge : IComparable<Edge>
    {
        public int v1, v2;

        public int weight;

        public Edge(int v1, int v2, int weight)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.weight = weight;
        }

        public int CompareTo(Edge other)
        {
            if (this.weight > other.weight)
                return 1;
            if (this.weight < other.weight)
                return -1;
            else
                return 0;
        }

        public static List<Edge> GetListOfEdgesFromMatrix(int[][] incidenceMatrix)
        {
            var edges = new List<Edge>();

            for (int i = 0; i < incidenceMatrix.Length; i++)
            {
                for (int j = 1 + i; j < incidenceMatrix[i].Length; j++)
                {
                    if (incidenceMatrix[i][j] != 0)
                        edges.Add(new Edge(i, j, incidenceMatrix[i][j]));
                }
            }

            return edges;
        }

        public static List<int> GetAdjacentedVerticesForVertex(int[][] incidenceMatrix, int vertex)
        {
            var adjacentedVertices = new List<int>();

            for (int i = 0; i < incidenceMatrix[vertex].Length; i++)
            {
                if (incidenceMatrix[vertex][i] > 0)
                    adjacentedVertices.Add(i);
            }

            return adjacentedVertices;
        }
    }
}