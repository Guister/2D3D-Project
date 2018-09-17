using UnityEngine;
using System.Collections;

public class Collisionss : MonoBehaviour {

	// Use this for initialization
	public bool collison = false;

	void OnTriggerEnter(Collider collision) {
		collison = true;

	}
	void OnTriggerExit(Collider collision) {
		collison = false;

	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
