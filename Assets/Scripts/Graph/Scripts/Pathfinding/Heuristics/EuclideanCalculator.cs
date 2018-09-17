using UnityEngine;
using System.Collections;

public class EuclideanCalculator : IHeuristicCalculator {

	public float Calculate(IGraphNode node, IGraphNode target)
	{
		Tile t1 = (Tile)node;
		Tile t2 = (Tile)target;

		return (t2.transform.position - t1.transform.position).sqrMagnitude;
	}

}
