using Common.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace LabsLibrary.Labs.Lab1
{
    class Lab1 : IRunnable
    {
        public void Run()
        {
            var incidenceMatrix = GetMatrixFromFile();
            Console.WriteLine("\nThe incidence matrix is: ");
            for (int i = 0; i < incidenceMatrix.Length; i++)
            {
                for (int j = 0; j < incidenceMatrix[i].Length; j++)
                {
                    Console.Write(incidenceMatrix[i][j] + " ");
                }
                Console.WriteLine();
            }


            var edges = GetListOfEdgesFromMatrix(incidenceMatrix);

            var mstByPrimsAlgorithm = PrimsAlgorithmProcessor.GetMSTByPrimsAlgorithm(edges);

            
            Console.WriteLine("\nThe minimum spanning tree by prim's algorithm is: ");

            for (int i = 0; i < mstByPrimsAlgorithm.Count; i++)
            {
                Console.WriteLine($"The {i}st edge is: (v1 = {mstByPrimsAlgorithm[i].v1}, v2 = {mstByPrimsAlgorithm[i].v2}, weight = {mstByPrimsAlgorithm[i].weight})");
            }
        }

        private List<Edge> GetListOfEdgesFromMatrix(int[][] incidenceMatrix)
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

        private int[][] GetMatrixFromFile()
        {
            var jsonTestDataFileName = "lab1Data.json";
            var absolutePathForDocs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory
                + jsonTestDataFileName);

            var json = File.ReadAllText(absolutePathForDocs);
            var jObject = JObject.Parse(json);
            var values = jObject["incidenceMatrix"]?.ToObject<int[][]>();

            return values != null ? values : throw new ArgumentNullException();
        }
    }
}