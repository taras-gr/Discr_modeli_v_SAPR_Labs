using LabsLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabsLibrary.Labs.Lab1
{
    static class PrimsAlgorithmProcessor
    {
        public static List<Edge> GetMSTByPrimsAlgorithm(List<Edge> edges)
        {
            var mst = new List<Edge>();
            List<Edge> notUsedEdges = new List<Edge>(edges);
            List<int> usedVertices = new List<int>();
            List<int> notUsedVertices = new List<int>();

            var verticesNumber = edges.Select(s => s.v1).Union(edges.Select(s => s.v2)).Count();

            for (int i = 0; i < verticesNumber; i++)
                notUsedVertices.Add(i);

            // Визначення випадкової пешої вершини, з якої починати роботу
            Random rand = new Random();
            usedVertices.Add(rand.Next(0, verticesNumber));
            Console.WriteLine($"\nThe first vertice is {usedVertices[0]}");
            notUsedVertices.RemoveAt(usedVertices[0]);

            while (notUsedVertices.Count > 0)
            {
                // Пошук мінімального суміжного ребра до вже задіяних вершин
                var minEdge = notUsedEdges.Where(s => usedVertices.Contains(s.v1) || usedVertices.Contains(s.v2)).Min();

                // Додавання вершини в список "задіяних"
                if (usedVertices.Contains(minEdge.v1))
                {
                    usedVertices.Add(minEdge.v2);
                    notUsedVertices.Remove(minEdge.v2);
                }
                    
                else
                {
                    usedVertices.Add(minEdge.v1);
                    notUsedVertices.Remove(minEdge.v1);
                }

                Console.WriteLine($"Adding edge (v1 = {minEdge.v1}, v2 = {minEdge.v2}, weight = {minEdge.weight}) to MST");

                //Додавання ребра в дерево
                mst.Add(minEdge);
                notUsedEdges.Remove(minEdge);
            }

            return mst;
        }
    }
}