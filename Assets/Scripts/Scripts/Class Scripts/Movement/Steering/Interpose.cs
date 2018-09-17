using UnityEngine;
using System.Collections;

public class Interpose : SteeringBehaviour {

	public myVehicle targetVehicle1;
	public myVehicle targetVehicle2;

	public override Vector3 Calculate (myVehicle vehicle)
	{
		return steering.Interpose (vehicle, targetVehicle1, targetVehicle2);
	}
}
