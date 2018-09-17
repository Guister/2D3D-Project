using UnityEngine;
using System.Collections;

public class OrientForward : MonoBehaviour {

	public float rotSpeed = 100;
	private Rigidbody rb;
	private Quaternion targetRot;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		targetRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
			
			Vector3 dir = rb.velocity;
			if (dir.sqrMagnitude > 0.1f) {
				dir.Normalize ();
				targetRot = Quaternion.LookRotation (dir);
			}
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, rotSpeed * Time.deltaTime);
	}
}
