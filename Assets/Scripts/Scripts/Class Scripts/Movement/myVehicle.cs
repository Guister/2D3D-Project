using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class myVehicle : MonoBehaviour {

	public Vector3 Direction{ get{ return _direction;}}
	public Vector3 Position{ get{ return _rb.position;}}
	public Vector3 Velocity{ get{ return _rb.velocity;}}

	public float MaxSpeed = 5.0f;
	public float MaxForce = 5f;
	public float Deceleration = 0.5f;
	public List<SteeringBehaviour> Behaviours;

	public enum CombineModes
	{
		Weight,
		Priority
	};

	public CombineModes CombineMode;

	private Rigidbody _rb;
	private Vector3 _direction;


	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody>();
		_direction = transform.forward;
		SortBehaviours ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 forces = Vector3.zero;

		switch (CombineMode) 
		{
		case CombineModes.Priority: 
			forces = ForcesByPriority ();
			break;
		case CombineModes.Weight:
			forces = ForcesByWeight ();
			break;
		}

		AddForce (forces);

		Vector3 vel = _rb.velocity;
		float minVel = 0.1f;
		if (vel.sqrMagnitude > minVel * minVel) 
		{
			_direction = vel;
			_direction.Normalize ();
		}
		if (vel.sqrMagnitude > MaxSpeed * MaxSpeed) 
		{
			vel.Normalize ();
			vel *= MaxSpeed;
			_rb.velocity = vel;
		}
	}

	private Vector3 ForcesByWeight() 
	{
		Vector3 forces = Vector3.zero;
		for (int i = 0; i < Behaviours.Count; i++) 
		{
			SteeringBehaviour b = Behaviours [i];
			if (b.enabled) 
			{
				forces += b.Calculate (this) * b.Weight;
			}
		}

		forces = Vector3.ClampMagnitude (forces, MaxForce);
		return forces;
	}

	private Vector3 ForcesByPriority() 
	{
		Vector3 forces = Vector3.zero;
		for (int i = 0; i < Behaviours.Count; i++) 
		{
			SteeringBehaviour b = Behaviours [i];
			if (b.enabled) 
			{
				Vector3 force = b.Calculate (this) * b.Weight;
				if (!AccumulateForce (force, ref forces)) 
				{
					return forces;
				}
			}
		}
			
		return forces;
	}

	private bool AccumulateForce(Vector3 force, ref Vector3 totalForces )
	{
		
		if (totalForces.sqrMagnitude >= MaxForce * MaxForce) 
		{
			return false;
		} 

		float curMagnitude = totalForces.magnitude + force.magnitude;

		if (curMagnitude >= MaxForce) {
			Vector3 newForce = force.normalized * (MaxForce - totalForces.magnitude);
			totalForces += newForce;
			return false;
		} 

		totalForces += force;
		return true;
	}



	public void AddForce(Vector3 f)
	{
		_rb.AddForce (f, ForceMode.Impulse);
	}

	public Vector3 LocalToWorld(Vector3 local)
	{
		Quaternion rotQuat = Quaternion.LookRotation (_direction);
		Matrix4x4 transformationMatrix = Matrix4x4.TRS (_rb.position, rotQuat, new Vector3 (1, 1, 1));
		return transformationMatrix.MultiplyPoint (local);
	}
	public void SortBehaviours()
	{
		Behaviours.Sort ((a, b) => -a.Priority.CompareTo(b.Priority));
	}
}
