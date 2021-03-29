using Common.Core.FileAccess;
using Common.Core.Interfaces;
using LabsLibrary.Common;
using System;
using System.Linq;

namespace LabsLibrary.Labs.Lab2
{
    class Lab2 : IRunnable
    {
        public void Run()
        {
            var incidenceMatrix = FileProcessor.GetMatrixFromFile("lab2Data.json", "incidenceMatrix");
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

            PostmanProblemProcessor postmanProblemProcessor = new PostmanProblemProcessor(3, incidenceMatrix);

            postmanProblemProcessor.SolvePostmanProblem();

            Console.WriteLine("The min path is: ");
            int q = 0;
            foreach (var item in postmanProblemProcessor.minEdges)
            {
                Console.WriteLine($"The {q}st edge is: (v1 = {item.v1}, v2 = {item.v2}, weight = {item.weight})");
                q++;
            }
            Console.WriteLine($"Total weight is: {postmanProblemProcessor.minEdges.Sum(x => x.weight)}");
        }        
    }
}