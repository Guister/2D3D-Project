using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SteeringBehaviours {

	public Vector3 Seek ( myVehicle vehicle, Vector3 targetPos)
	{
		// Calculate distance to target
		Vector3 toTarget = targetPos - vehicle.Position;
		//Calculate target "desired" velocity
		Vector3 desired = toTarget.normalized * vehicle.MaxSpeed;
		//Subtract target "desired" velocity and current vehicle velocity
		//And apply it as force to the vehicle
		return desired -  vehicle.Velocity;
	}

	public Vector3 Flee ( myVehicle vehicle, Vector3 targetPos)
	{
		// Calculate distance from target
		Vector3 fromTarget = vehicle.Position - targetPos;
		//Calculate target "desired" velocity
		Vector3 desired = fromTarget.normalized * vehicle.MaxSpeed;
		//Subtract target "desired" velocity and current vehicle velocity
		//And apply it as force to the vehicle
		return desired -  vehicle.Velocity;
	}

	public Vector3 Flee ( myVehicle vehicle, Vector3 targetPos, float panicDistance)
	{
		// Calculate distance to target
		Vector3 toTarget = targetPos - vehicle.Position;
		if (toTarget.sqrMagnitude < panicDistance * panicDistance) {
			return Flee (vehicle, targetPos);
		} 
		else return Vector3.zero;
	}

	public Vector3 Arrive ( myVehicle vehicle, Vector3 targetPos)
	{
		// Calculate distance to target
		Vector3 toTarget = targetPos - vehicle.Position;

		if (toTarget.sqrMagnitude > 0) 
		{
			float dist = toTarget.magnitude;
			float speed = dist / vehicle.Deceleration;
			speed = Mathf.Min (speed, vehicle.MaxSpeed);
			Vector3 desired = (toTarget / dist) * speed;
			return desired -  vehicle.Velocity;
		}
		return Vector3.zero;
	}

	public Vector3 Pursuit (myVehicle vehicle, myVehicle targetVehicle)
	{
		//Distance vector to the target
		Vector3 toTarget = targetVehicle.Position - vehicle.Position;

		//Using the vehicles and the desired distance to calculate our "LookAheadTime"
		float lookAheadTime = LookAheadTime (vehicle, targetVehicle, toTarget);

		//using the time to get the future point
		Vector3 targetPoint = targetVehicle.Position + (lookAheadTime * targetVehicle.Direction);

		//Using a regular Seek to get to the target position
		return Seek (vehicle, targetPoint);
	}

	public Vector3 Evade (myVehicle vehicle, myVehicle targetVehicle, float panicDistance)
	{
		//Distance vector to the target
		Vector3 toTarget = targetVehicle.Position - vehicle.Position;

		//Using the vehicles and the desired distance to calculate our "LookAheadTime"
		float lookAheadTime = LookAheadTime(vehicle, targetVehicle, toTarget);

		//using the time to get the future point
		Vector3 targetPoint = targetVehicle.Position + (lookAheadTime * targetVehicle.Direction);
		return Flee (vehicle, targetPoint, panicDistance);
	}

	public Vector3 Wander (myVehicle vehicle, float distance, float radius, float jitter, Vector3 localTarget)
	{
		//random variation to the localtarget
		localTarget += new Vector3 (Random.Range (-jitter, jitter), 0, Random.Range (-jitter, jitter));

		//normalizing the locat target into a unit 1 sphere
		localTarget.Normalize ();

		//multiplying the local target by the radius of the desired sphere
		localTarget *= radius;

		//Local coordinates to world coordinates
		Vector3 worldTarget = vehicle.LocalToWorld(localTarget);

		//giving the extra distance to the sphere
		worldTarget += (vehicle.Direction * distance);

		return Seek(vehicle, worldTarget);
	}

	public Vector3 OffsetPursuit (myVehicle vehicle, myVehicle targetVehicle, Vector3 offset)
	{
		//Distance vector to the offset regarding the target
		Vector3 worldOffset = targetVehicle.LocalToWorld (offset);
		Vector3 toTarget = worldOffset - vehicle.Position;

		//Using the vehicles and the desired distance to calculate our "LookAheadTime"
		float lookAheadTime = LookAheadTime (vehicle, targetVehicle, toTarget);

		//using the time to get the future point
		Vector3 targetPoint = worldOffset + (lookAheadTime * targetVehicle.Direction);

		return Arrive(vehicle, targetPoint);
	}


	public Vector3 Interpose(myVehicle vehicle, myVehicle targetVehicle1, myVehicle targetVehicle2) 
	{
		//Getting the targetPoint for the vehicle1 

		Vector3 toTarget = targetVehicle1.Position - vehicle.Position;
		float lookAheadTime = LookAheadTime(vehicle, targetVehicle1, toTarget);
		Vector3 targetPoint1 = targetVehicle1.Position + (lookAheadTime * targetVehicle1.Direction);

		//Getting the targetPoint for the vehicle2

		toTarget = targetVehicle2.Position - vehicle.Position;
		lookAheadTime = LookAheadTime(vehicle, targetVehicle2, toTarget);
		Vector3 targetPoint2 = targetVehicle2.Position + (lookAheadTime * targetVehicle2.Direction);

		Vector3 targetPoint = (targetPoint1 + targetPoint2) * 0.5f;

		return Arrive (vehicle, targetPoint);
	}


	public Vector3 Hide (myVehicle vehicle, myVehicle targetVehicle, List<Collider> obstacles, float hidingDistance, float panicDistance)
	{
		Vector3 result = Vector3.zero;
		float nearestDist = float.MaxValue;
		Vector3 vehiclePosition = vehicle.Position;

		foreach (Collider c in obstacles) 
		{
			
			Vector3 hidePos = GetHidePos (c, targetVehicle, hidingDistance);
			if ((hidePos - targetVehicle.Position).sqrMagnitude < panicDistance * panicDistance) 
			{ 
				continue;
			}
			float currentDist = (hidePos - vehiclePosition).sqrMagnitude;
			if ( currentDist < nearestDist) {
				
				nearestDist = currentDist;
				result = hidePos;
			} 
		}
		if (nearestDist == float.MaxValue) 
		{
			return Evade(vehicle, targetVehicle, panicDistance);
		}
		return Arrive(vehicle, result);
	}

	public Vector3 AvoidObstacles (myVehicle vehicle, List<Collider> obstacles, float avoidDistance){

		Vector3 repulsion = Vector3.zero;

		if (obstacles.Count == 0) 
		{
			return Vector3.zero;
		}

		foreach (Collider c in obstacles) 
		{
			Vector3 opositeVector = vehicle.Position - c.bounds.center;
			float distance = opositeVector.magnitude;

			if (distance > avoidDistance) 
			{
				return Vector3.zero;
			}

			float forceStr = 1f - (distance / avoidDistance);
			repulsion += opositeVector * forceStr;
		}
		Vector3 desired = Vector3.Reflect(vehicle.Velocity, repulsion);
		desired.Normalize ();
		desired *= vehicle.MaxSpeed;
		return desired - vehicle.Velocity;
	}

	public Vector3 FollowPath(myVehicle vehicle, List<Transform> wayPoints, bool loop, ref int index, float arriveDistance)
	{
		Transform wayPoint = wayPoints [index];
		Vector3 toPoint = wayPoint.position - vehicle.Position;
		if (toPoint.sqrMagnitude <= arriveDistance * arriveDistance)
		{
			if (index + 1 >= wayPoints.Count) 
			{
				if (loop)
				{
					index = 0;
				}
				else 
				{
					return Arrive (vehicle, wayPoint.position);
				}
			} 
			else 
			{
				index++;
			}
		} 
		return Seek (vehicle, wayPoint.position);
	}



	/***************************************************************************************/
	/**********************************EXTRA FUNCTIONS**************************************/
	/***************************************************************************************/

	public float LookAheadTime(myVehicle vehicle, myVehicle targetVehicle, Vector3 toTarget)
	{
		// Getting the magnitude of the distance 
		float dist = toTarget.magnitude;

		// the distance will be divided by this value of "vehicle velocities", we assume the vehicle will go thowards the target and as such it's max velocity will reduce the time it takes to get there
		// for the target however, its a bit tricky, the target can be moving away from the vehicle, and if that is the case, its velocity would make it so the original vehicle takes more time to get there
		// I decided to use (- Dot Product * velocity), in this case, if its going away it will give a negative value, if its going closer, it will give a positive value and help reduce the time.
		float vehicleVelocities = vehicle.MaxSpeed + (-Vector3.Dot(vehicle.Direction, targetVehicle.Direction) * targetVehicle.Velocity.magnitude);

		// This simple math clamp helps us by defining the minimum allowed "vehicle velocities", because if it's too close to 0 the "look ahead time" will be too high
		// and give the pursuer some wierd very far away target.
		vehicleVelocities = Mathf.Clamp (vehicleVelocities, 0.5f, vehicle.MaxSpeed + targetVehicle.Velocity.magnitude);

		// using the magnitude and the velocities of both vehicles to get an aproximation of the future position of said target point
		float lookAheadTime = dist / vehicleVelocities;

		//Vector3 targetPoint = targetVehicle.Position + (lookAheadTime * targetVehicle.Direction);
		return lookAheadTime;
	}

	public Vector3 GetHidePos (Collider c, myVehicle target, float hidingDist)
	{
		Vector3 toObstacle = c.bounds.center - target.transform.position;
		toObstacle.Normalize ();
		Vector3 hidePos = c.bounds.center + toObstacle * hidingDist;
		return hidePos;
	}
		
}