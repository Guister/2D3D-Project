using UnityEngine;
using System.Collections;

public class Jumping : MonoBehaviour {

	public bool grounded = true;
	public float jumpingSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.Space) && grounded == true)
		{
			GetComponent<Rigidbody> ().AddForce (new Vector3 (0, jumpingSpeed, 0), ForceMode.Impulse);
			grounded = false;
		}
	}

	void OnCollisionStay () {
		grounded = true;
	}
}
