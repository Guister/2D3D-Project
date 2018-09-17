using UnityEngine;
using System.Collections;

public class Wander : SteeringBehaviour {

	public float projectionDistance = 5.0f;
	public float radius = 5.0f;
	public float jitter = 0.5f;
	private Vector3 worldTarget;

	protected Vector3 _localTarget;

	void Awake() 
	{
		_localTarget = transform.forward;
	}

	public override Vector3 Calculate (myVehicle vehicle)
	{
		return steering.Wander (vehicle, projectionDistance, radius ,jitter, _localTarget);
	}
}
