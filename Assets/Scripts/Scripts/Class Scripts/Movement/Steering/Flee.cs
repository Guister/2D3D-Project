using UnityEngine;
using System.Collections;

public class Flee : SteeringBehaviour {

	public Transform target;
	public float panicDistance;

	public override Vector3 Calculate (myVehicle vehicle)
	{
		return steering.Flee (vehicle, target.position, panicDistance);
	}
}
