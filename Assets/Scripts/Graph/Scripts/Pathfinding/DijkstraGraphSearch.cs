using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DijkstraGraphSearch
{
	public Dictionary<IGraphNode, GraphEdge> ShortestPathTree = new Dictionary<IGraphNode, GraphEdge>();
	public Dictionary<IGraphNode, GraphEdge> SearchFrontier = new Dictionary<IGraphNode, GraphEdge>();
	public List<IGraphNode> shortestPath = new List<IGraphNode> ();
	public List<Transform> shortestPathVector = new List<Transform> ();
	public PathSmoother pathSmoother;

	GraphNodePriorityQueue pq = new GraphNodePriorityQueue();


	public bool Search(Graph graph, IGraphNode sourceNode, IGraphNode targetNode) 
	{
		pq.Add (sourceNode);
		while (pq.count > 0)
		{
			IGraphNode nextClosestNode = pq.PopFirst ();

			if (SearchFrontier.ContainsKey (nextClosestNode)) 
			{
				GraphEdge nearestEdge = SearchFrontier [nextClosestNode];

				if (ShortestPathTree.ContainsKey (nextClosestNode)) 
				{
					ShortestPathTree [nextClosestNode] = nearestEdge;
				} 
				else 
				{
					ShortestPathTree.Add (nextClosestNode, nearestEdge);
				}
			}

			if (nextClosestNode == targetNode) 
			{
				pathSmoother = new PathSmoother ();
				Debug.Log ("Target Found");
				ConstructPath (sourceNode, targetNode);
				shortestPathVector = pathSmoother.Smooth(shortestPath, 1 << 8);
				return true;
			}

			List<GraphEdge> frontier = graph.GetFrontier (nextClosestNode);

			foreach (GraphEdge edge in frontier) 
			{
				IGraphNode neighbour = edge.To;

				float totalCost = nextClosestNode.totalNodeCost + edge.Cost;

				if (SearchFrontier.ContainsKey (neighbour)) 
				{
					if (totalCost < neighbour.totalNodeCost) 
					{
						neighbour.totalNodeCost = totalCost;
						SearchFrontier [neighbour] = edge;
						pq.MarkToSort ();
					}
					
				} 
				else 
				{
					neighbour.totalNodeCost = totalCost;
					SearchFrontier.Add (neighbour, edge);
					pq.Add (neighbour);
				}
			}
		}
		return false;
	}

	private void ConstructPath(IGraphNode origin, IGraphNode target) 
	{
		shortestPath.Clear ();

		IGraphNode currNode = target;
		shortestPath.Add (currNode);

		GraphEdge edge = null;

		while (currNode != origin) 
		{
			edge = ShortestPathTree[currNode];
			shortestPath.Add (edge.From);
			currNode = edge.From;
		}

		shortestPath.Add (currNode);
		shortestPath.Reverse ();
	}

}
