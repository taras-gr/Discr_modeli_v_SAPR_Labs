using System;

namespace LabsLibrary.Labs.Lab4
{
    public class FordFulkersonProcessor
    {
        public int CalculateMaxFlow(int[][] cap, int vertexFrom, int vertexTo)
        {
            var maxFlowValue = 0;

            while (true)
            {
                var foundFlow = FindPath(cap, new bool[cap.Length], vertexFrom, vertexTo, int.MaxValue);

                // Поки існує шлях foundFlow від джерела до стоку в залишковій мережі
                if (foundFlow == 0)
                    break;
                maxFlowValue += foundFlow;
            }

            return maxFlowValue;
        }

        private int FindPath(int[][] matrix, bool[] visitedVertices, 
            int vertexFrom, int vertexTo, int flowValue)
        {
            if (vertexFrom == vertexTo)
                return flowValue;
            visitedVertices[vertexFrom] = true;
            for (int v = 0; v < visitedVertices.Length; v++)
                if (!visitedVertices[v] && matrix[vertexFrom][v] > 0)
                {
                    // Рекурсивно шукаєм шлях до кожної наступної вершини
                    int df = FindPath(matrix, visitedVertices, v, vertexTo, 
                        Math.Min(flowValue, matrix[vertexFrom][v]));
                    if (df > 0)
                    {
                        // Оновлюємо потік відповідно до випадків приналежності до мережі
                        matrix[vertexFrom][v] -= df;
                        matrix[v][vertexFrom] += df;
                        return df;
                    }
                }
            return 0;
        }
    }
}