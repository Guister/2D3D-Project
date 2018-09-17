using UnityEngine;
using System.Collections;

public class GroundedCheck : MonoBehaviour {
	public bool grounded = false;

	void OnTriggerEnter ( Collider collli ) {

		grounded = true;

	}

	void OnTriggerExit ( Collider collli){
		grounded = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
