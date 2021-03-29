using Common.Core.FileAccess;
using Common.Core.Interfaces;
using LabsLibrary.Common;
using System;

namespace LabsLibrary.Labs.Lab1
{
    public class Lab1 : IRunnable
    {
        public void Run()
        {
            var incidenceMatrix = FileProcessor.GetMatrixFromFile("lab1Data.json", "incidenceMatrix");
            Console.WriteLine("\nThe incidence matrix is: ");
            for (int i = 0; i < incidenceMatrix.Length; i++)
            {
                for (int j = 0; j < incidenceMatrix[i].Length; j++)
                {
                    Console.Write(incidenceMatrix[i][j] + " ");
                }
                Console.WriteLine();
            }


            var edges = Edge.GetListOfEdgesFromMatrix(incidenceMatrix);

            var mstByPrimsAlgorithm = PrimsAlgorithmProcessor.GetMSTByPrimsAlgorithm(edges);

            
            Console.WriteLine("\nThe minimum spanning tree by prim's algorithm is: ");

            for (int i = 0; i < mstByPrimsAlgorithm.Count; i++)
            {
                Console.WriteLine($"The {i}st edge is: (v1 = {mstByPrimsAlgorithm[i].v1}, v2 = {mstByPrimsAlgorithm[i].v2}, weight = {mstByPrimsAlgorithm[i].weight})");
            }
        }        
    }
}