using LabsLibrary.Common;
using System;
using System.Collections.Generic;

namespace LabsLibrary.Rozrah
{
    class TarjansProcessor
    {
		static readonly int NIL = -1;
		int time = 0;
		void DfsForBridge(List<(int, int)> bridgeVertices, int[][] adjacentedMatrix,
			int u, bool[] visited, int[] disc, int[] low, int[] parent)
		{
			visited[u] = true;
			disc[u] = low[u] = ++time;

			var adjVertices = Edge.GetAdjacentedVerticesForVertex(adjacentedMatrix, u);

			// Проходимо через всі суміжні вершини до поточної
			foreach (int i in adjVertices)
			{
				int v = i; // v - поточна суміжна до u

				// Якщо v ще не відвідана - робим її дочірньою для u в дереві і DFS для неї				
				if (!visited[v])
				{
					parent[v] = u;
					DfsForBridge(bridgeVertices, adjacentedMatrix, v, visited, disc, low, parent);

					// Перевіряємо чи піддерево з вершиною v має зв'язок з батьком u
					low[u] = Math.Min(low[u], low[v]);

					// Якщо найнижча вершина, до якої можна дійти з піддерева під v, 
					// знаходиться нижче u у дереві DFS, то u-v є мостом
					if (low[v] > disc[u])
						bridgeVertices.Add((u, v));
				}

				// Оновлюємо значення low u для батька
				else if (v != parent[u])
					low[u] = Math.Min(low[u], disc[v]);
			}
		}

		public List<(int, int)> GetBridges(int[][] adjacentedMatrix)
		{
			int V = adjacentedMatrix.Length;
			bool[] visited = new bool[V];
			int[] disc = new int[V];
			int[] low = new int[V];
			int[] parent = new int[V];
			List<(int, int)> bridgeVertices = new List<(int, int)>();

			for (int i = 0; i < V; i++)
			{
				parent[i] = NIL;
				visited[i] = false;
			}

			for (int i = 0; i < V; i++)
				if (visited[i] == false)
					DfsForBridge(bridgeVertices, adjacentedMatrix, i, visited, disc, low, parent);

			return bridgeVertices;
		}
	}
}