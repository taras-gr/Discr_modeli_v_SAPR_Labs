using Common.Core.FileAccess;
using Common.Core.Interfaces;
using System;

namespace LabsLibrary.Labs.Lab4
{
    class Lab4 : IRunnable
    {
        public void Run()
        {
            var incidenceMatrix = FileProcessor.GetMatrixFromFile("lab4Data.json", "incidenceMatrix");
            Console.WriteLine("\nThe incidence matrix is: ");
            for (int i = 0; i < incidenceMatrix.Length; i++)
            {
                for (int j = 0; j < incidenceMatrix[i].Length; j++)
                {
                    Console.Write(incidenceMatrix[i][j] + " ");
                }
                Console.WriteLine();
            }

            var ffp = new FordFulkersonProcessor();
            var result = ffp.CalculateMaxFlow(incidenceMatrix, 0, 2);

            Console.WriteLine($"\nThe max flow value is: {result}");            
        }
    }
}