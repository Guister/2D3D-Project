using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph
{
	public List<IGraphNode> Nodes = new List<IGraphNode>();
	public List<GraphEdge> Edges = new List<GraphEdge>();
	public Dictionary<IGraphNode, List<GraphEdge>> NodeFrontier = new Dictionary<IGraphNode, List<GraphEdge>> ();

	private bool _directed;

	public Graph(bool directed) 
	{
		this._directed = directed;
	}

	public bool AddNode(IGraphNode node)
	{
		if (Nodes.Contains (node))
			return false;

		Nodes.Add (node);
		NodeFrontier.Add (node, new List<GraphEdge> ());
		return true;
	}

	public IGraphNode GetNode (int index) 
	{
		return Nodes[index];
	}
		
	public List<GraphEdge> GetFrontier (IGraphNode node) 
	{
		return NodeFrontier [node];
	}

	public bool RemoveNode (IGraphNode node) 
	{
		if (Nodes.Contains (node)) 
		{
			Nodes.Remove (node);
			return true;
		}

		return false;
	}

	public bool AddEdge (GraphEdge edge) 
	{
		if (Edges.Contains (edge)) 
		{
			return false;
		}

		Edges.Add (edge);
		AddFrontier (edge);

		if (_directed == false) 
		{
			GraphEdge inverted = new GraphEdge (edge.To, edge.From, edge.Cost);
			Edges.Add (inverted);
			AddFrontier (inverted);
		}
		return true;
	}

	public bool RemoveEdge (GraphEdge edge) 
	{
		if (Edges.Remove (edge)) 
		{
			List<GraphEdge> frontier = NodeFrontier [edge.From];
			frontier.Remove (edge);
			if (_directed == false)
			{
				GraphEdge inverted = FindEdge (edge.To, edge.From);

				if (inverted != null) 
				{
					Edges.Remove (inverted);
					frontier = NodeFrontier [inverted.From];
					frontier.Remove (inverted);	
				}
			}
			return true;
		}
		return false;
	}

	private GraphEdge FindEdge (IGraphNode from, IGraphNode to)
	{
		for (int i = 0; i < Edges.Count; i++) 
		{
			GraphEdge edge = Edges [i];
			if (edge.From == from && edge.To == to) 
			{
				return edge;
			}
		}
		return null;
	}

	private void AddFrontier (GraphEdge edge)
	{
		IGraphNode startNode = edge.From;

		if (NodeFrontier.ContainsKey (startNode) == false) 
		{
			AddNode (startNode);
		}

		NodeFrontier [edge.From].Add (edge);
	}

}
