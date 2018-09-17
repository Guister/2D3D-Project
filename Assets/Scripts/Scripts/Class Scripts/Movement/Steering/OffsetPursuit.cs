using UnityEngine;
using System.Collections;

public class OffsetPursuit : SteeringBehaviour {

	public myVehicle leader;
	public Vector3 offSetVector;

	public override Vector3 Calculate (myVehicle vehicle)
	{
		return steering.OffsetPursuit (vehicle, leader, offSetVector);
	}
}
