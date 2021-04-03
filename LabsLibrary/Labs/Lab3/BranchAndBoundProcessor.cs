using LabsLibrary.Common;
using System.Collections.Generic;
using System.Linq;

namespace LabsLibrary.Labs.Lab3
{
    class BranchAndBoundProcessor
    {
        public (List<int> Path, int PathCost) SolveSalesmanProblemByBnB(int[][] matrix)
        {
            var reducedMatrix = GetReducedMatrix(matrix);

            var usedVertices = new List<int>();
            usedVertices.Add(0);

            var path = GetNextPath(matrix, reducedMatrix.reducedMatrix, reducedMatrix.reduceCost, 0, usedVertices);
            var pathCost = CalculatePathCost(matrix, path);

            return (path, pathCost);
        }

        private List<int> GetNextPath(int[][] incidenceMatrix, int[][] reducedMatrix, 
            int reduceCost, int vertexId, List<int> usedVertices)
        {
            // Якщо всі вершини задіяно закінчуємо роботу і повертаємо шлях
            if (usedVertices.Count == incidenceMatrix.Length)
            {
                usedVertices.Add(usedVertices[0]);
                return usedVertices;
            }                

            // Можливі наступні вершини (суміжні з vertexId)
            var possibleNextVertex = Edge.GetAdjacentedVerticesForVertex(incidenceMatrix, vertexId).Except(usedVertices).ToList();
            var possibleVerticesCosts = new List<int>();
            var localReducedMatrices = new int[possibleNextVertex.Count][][];

            // Розраховуємо зменшені матриці для кожної ймовірної наступної вершини
            for (int k = 0; k < possibleNextVertex.Count; k++)
            {
                var vertexCost = 0;

                localReducedMatrices[k] = new int[incidenceMatrix.Length][];

                for (int i = 0; i < localReducedMatrices[k].Length; i++)
                {
                    localReducedMatrices[k][i] = new int[reducedMatrix[i].Length];

                    for (int j = 0; j < localReducedMatrices[k][i].Length; j++)
                    {
                        localReducedMatrices[k][i][j] = reducedMatrix[i][j];
                    }
                }

                for (int i = 0; i < localReducedMatrices[k].Length; i++)
                {
                    localReducedMatrices[k][vertexId][i] = -1;
                    localReducedMatrices[k][i][possibleNextVertex[k]] = -1;
                }

                localReducedMatrices[k][possibleNextVertex[k]][vertexId] = -1;

                var q = GetMinsForEachColumn(localReducedMatrices[k]);
                var z = GetMinsForEachRow(localReducedMatrices[k]);

                // Обчислюємо вартість ймовірної вершини
                vertexCost += reducedMatrix[vertexId][possibleNextVertex[k]];
                vertexCost += reduceCost;
                vertexCost += q.Where(s => s >= 0).Sum();
                vertexCost += z.Where(s => s >= 0).Sum();

                possibleVerticesCosts.Add(vertexCost);
            }

            // Визначаємо наступну вершину за найменшою вартістю
            var minPossibleVerticesCost = possibleVerticesCosts.Min();
            vertexId = possibleNextVertex[possibleVerticesCosts.IndexOf(minPossibleVerticesCost)];
            reducedMatrix = localReducedMatrices[possibleVerticesCosts.IndexOf(minPossibleVerticesCost)];
            usedVertices.Add(vertexId);

            // Рекурсивно викликаємо даний метод для визначеної наступної вершини
            return GetNextPath(incidenceMatrix, reducedMatrix, minPossibleVerticesCost, vertexId, usedVertices);
        }

        private (int[][] reducedMatrix, int reduceCost) GetReducedMatrix(int[][] matrix)
        {
            var reducedMatrix = new int[matrix.Length][];
            for (int i = 0; i < reducedMatrix.Length; i++)
            {
                reducedMatrix[i] = new int[matrix[i].Length];

                for (int j = 0; j < reducedMatrix[i].Length; j++)
                {
                    reducedMatrix[i][j] = matrix[i][j];
                }
            }

            // Знаходимо мінімуми кодного рядка
            var minsForEachRow = GetMinsForEachRow(matrix);            

            // Віднімаємо від кожного елемента відповідний мінімум
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (i == j)
                        continue;
                    reducedMatrix[i][j] -= minsForEachRow[i];
                }
            }

            // Знаходимо мінімуми кодного стовпця
            var minsForEachColumn = GetMinsForEachColumn(reducedMatrix);

            // Віднімаємо від кожного елемента відповідний мінімум
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (i == j)
                        continue;
                    reducedMatrix[j][i] -= minsForEachColumn[i];
                }
            }

            return (reducedMatrix, minsForEachRow.Where(s => s != -1).Sum() + minsForEachColumn.Where(s => s != -1).Sum());
        }

        private List<int> GetMinsForEachColumn(int[][] matrix)
        {
            List<int> minsForEachColumn = new List<int>();

            var lengthOfFirstDimesion = matrix.Length;
            var lengthOfSecondDimesion = matrix[0].Length;

            for (int i = 0; i < lengthOfSecondDimesion; i++)
            {
                if (matrix[0][i] != -1)
                    minsForEachColumn.Add(matrix[0][i]);
                else
                    minsForEachColumn.Add(matrix[1][i]);

                for (int j = 1; j < lengthOfFirstDimesion; j++)
                {
                    if (matrix[j][i] < minsForEachColumn[i] && matrix[j][i] != -1)
                        minsForEachColumn[i] = matrix[j][i];
                }
            }

            return minsForEachColumn;
        }

        private List<int> GetMinsForEachRow(int[][] matrix)
        {
            List<int> minsForEachRow = new List<int>();

            var lengthOfFirstDimesion = matrix.Length;
            var lengthOfSecondDimesion = matrix[0].Length;

            for (int i = 0; i < lengthOfFirstDimesion; i++)
            {
                if (matrix[i][0] != -1)
                    minsForEachRow.Add(matrix[i][0]);
                else
                    minsForEachRow.Add(matrix[i][1]);

                for (int j = 1; j < lengthOfSecondDimesion; j++)
                {
                    if (matrix[i][j] < minsForEachRow[i] && matrix[i][j] != -1)
                        minsForEachRow[i] = matrix[i][j];
                }
            }

            return minsForEachRow;
        }

        private int CalculatePathCost(int[][] incidenceMatrix, List<int> path)
        {
            var pathCost = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                pathCost += incidenceMatrix[path[i]][path[i + 1]];
            }

            return pathCost;
        }
    }
}