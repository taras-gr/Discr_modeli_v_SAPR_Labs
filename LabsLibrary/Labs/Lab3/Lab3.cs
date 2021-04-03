using Common.Core.FileAccess;
using Common.Core.Interfaces;
using System;

namespace LabsLibrary.Labs.Lab3
{
    class Lab3 : IRunnable
    {
        public void Run()
        {
            var incidenceMatrix = FileProcessor.GetMatrixFromFile("lab3Data.json", "incidenceMatrix");
            Console.WriteLine("\nThe incidence matrix is: ");
            for (int i = 0; i < incidenceMatrix.Length; i++)
            {
                for (int j = 0; j < incidenceMatrix[i].Length; j++)
                {
                    Console.Write(incidenceMatrix[i][j] + " ");
                }
                Console.WriteLine();
            }
            
            var sp = new BranchAndBoundProcessor();
            var result = sp.SolveSalesmanProblemByBnB(incidenceMatrix);

            Console.WriteLine("\nThe best way is: ");
            foreach (var item in result.Path)
            {
                Console.Write($"{item} - ");
            }
            Console.WriteLine($"Total weight is: {result.PathCost}");
        }
    }
}