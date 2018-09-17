using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FollowPath : SteeringBehaviour {

	public GameObject wayPointGroup;
	private List<Transform> wayPoints;
	public bool loop;
	public int startIndex = 0;
	public float arriveDistance = 2.0f;
	private int currIndex;

	void Start() {
		int i = 0;
		wayPoints = new List<Transform> ();
		foreach(Transform wpoint in wayPointGroup.GetComponentsInChildren<Transform> ())
		{
			if (i == 0) {
			} else {
				wayPoints.Add (wpoint);
			}
			i++;
		}
		currIndex = startIndex;
	}

	public override Vector3 Calculate (myVehicle vehicle)
	{
		return steering.FollowPath (vehicle, wayPoints, loop, ref currIndex, arriveDistance);
	}
}
