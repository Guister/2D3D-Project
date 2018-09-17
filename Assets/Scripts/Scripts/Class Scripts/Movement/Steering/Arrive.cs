using UnityEngine;
using System.Collections;

public class Arrive : SteeringBehaviour {

	public Transform target;

	public override Vector3 Calculate (myVehicle vehicle)
	{
		return steering.Arrive (vehicle, target.position);
	}
}
