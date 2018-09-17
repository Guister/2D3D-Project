using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


	public float followSpeed = 5f;
	public Transform target;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - target.transform.position;
	}

	// Update is called once per frame
	void Update () {
		
		Vector3 desiredPos = target.transform.position + offset;
		transform.position = Vector3.Lerp (transform.position, desiredPos, Time.deltaTime * followSpeed);
	}
}
