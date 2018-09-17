using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvoidObstacles : SteeringBehaviour {

	private List<Collider> obstacleList;
	private float avoidDistance;

	void Awake() 
	{
		obstacleList = new List<Collider>();
	}
	void Start() 
	{
		avoidDistance = GetComponent<SphereCollider> ().radius;
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag("Ground")) 
		{
			return;
		}
		obstacleList.Add (other);
	}

	void OnTriggerExit(Collider other)  
	{
		obstacleList.Remove (other);
	}

	public override Vector3 Calculate (myVehicle vehicle)
	{
		return steering.AvoidObstacles (vehicle, obstacleList, avoidDistance);
	}
}
