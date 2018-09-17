using UnityEngine;
using System.Collections;

public abstract class SteeringBehaviour: MonoBehaviour{


	public float Weight = 1f;
	public int Priority = 0;

	protected SteeringBehaviours steering;

	void Start() 
	{
	}

	public SteeringBehaviour() 
	{
		steering = new SteeringBehaviours ();
	}
	public abstract Vector3 Calculate (myVehicle vehicle);
}