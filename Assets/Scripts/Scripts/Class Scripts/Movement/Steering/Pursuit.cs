using UnityEngine;
using System.Collections;

public class Pursuit : SteeringBehaviour {

	public myVehicle target;

	public override Vector3 Calculate (myVehicle vehicle)
	{
		return steering.Pursuit (vehicle, target);
	}
}
