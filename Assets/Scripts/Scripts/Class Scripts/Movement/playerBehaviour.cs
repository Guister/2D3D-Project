using UnityEngine;
using System.Collections;

public class playerBehaviour : MonoBehaviour {

	public float maxSpeed, maxRotSpeed;
	myVehicle vehicle;

	// Use this for initialization
	void Start () {	
		vehicle = GetComponent<myVehicle> ();
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		transform.Rotate (0, h * maxRotSpeed, 0);
	}

	void FixedUpdate() 
	{
		float v = Input.GetAxis ("Vertical");
		vehicle.AddForce (transform.forward * v * maxSpeed * Time.deltaTime);
	}
}
