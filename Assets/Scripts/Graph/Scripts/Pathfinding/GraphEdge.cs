using UnityEngine;
using System.Collections;

public class GraphEdge {

	public IGraphNode From, To;
	public float Cost;

	public GraphEdge(IGraphNode from, IGraphNode to, float cost)
	{
		this.To = to;
		this.From = from;
		this.Cost = cost;
	}
}
