using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hide  : SteeringBehaviour {

	public myVehicle target;
	public float hideDistance = 0.5f;
	public float panicDistance = 10;
	public GameObject obstaclesContainer;
	public List<Collider> obstacleList;

	void Start() 
	{
		Collider[] obstacles = obstaclesContainer.GetComponentsInChildren<Collider> ();
		obstacleList = new List<Collider> (obstacles);
	}

	public override Vector3 Calculate (myVehicle vehicle)
	{
		return steering.Hide (vehicle, target, obstacleList, hideDistance, panicDistance);
	}
}
