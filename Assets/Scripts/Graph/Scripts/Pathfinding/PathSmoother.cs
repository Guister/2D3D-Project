using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathSmoother {

	public List<Transform> Smooth (List<IGraphNode> path, LayerMask mask)
	{
		List<Transform> result = new List<Transform> ();
		Tile currTile = (Tile)path [0];
		result.Add (currTile.transform);
		int pivot = 0;
		int endPoint = path.Count - 1;

		for (int i = 1; i < path.Count - 1; i++) 
		{
			Tile t1 = (Tile)path [pivot];
			Tile t2 = (Tile)path [i + 1];
			if (IsBlocked (t1.transform, t2.transform, mask)) 
			{
				pivot = i;
				currTile = (Tile)path [i];
				result.Add (currTile.transform);
			}
		}
		currTile = (Tile)path [endPoint];
		result.Add (currTile.transform);
		return result;
	}

	private bool IsBlocked (Transform origin, Transform target, LayerMask mask)
	{
		Vector3 distance = target.position - origin.position;
		float distanceMagnitude = distance.magnitude;
		if (Physics.Raycast(origin.position, distance.normalized, distanceMagnitude, mask))
		{
			return true;
		}
		return false;
	}
}
