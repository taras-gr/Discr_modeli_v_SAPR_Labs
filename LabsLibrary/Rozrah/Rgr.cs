using Common.Core.FileAccess;
using Common.Core.Interfaces;
using System;

namespace LabsLibrary.Rozrah
{
    class Rgr : IRunnable
    {
        public void Run()
        {
            var adjacentedMatrix = FileProcessor.GetMatrixFromFile("rgr.json", "adjacencyMatrix");
            Console.WriteLine("\nThe adjacency matrix is: ");
            for (int i = 0; i < adjacentedMatrix.Length; i++)
            {
                for (int j = 0; j < adjacentedMatrix[i].Length; j++)
                {
                    Console.Write(adjacentedMatrix[i][j] + " ");
                }
                Console.WriteLine();
            }

            var tp = new TarjansProcessor();
            var bridges = tp.GetBridges(adjacentedMatrix);

            Console.WriteLine("\nThe brigdes are:");
            foreach (var bridge in bridges)
            {
                Console.WriteLine($"{bridge.Item1} - {bridge.Item2}");
            }
        }
    }
}