using UnityEngine;
using System.Collections;

public class Seek : SteeringBehaviour{

	public Transform target;

	public override Vector3 Calculate (myVehicle vehicle)
	{
		return steering.Seek (vehicle, target.position);
	}

}
