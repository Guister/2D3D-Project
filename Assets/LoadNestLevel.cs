using UnityEngine;
using System.Collections;

public class LoadNestLevel : MonoBehaviour {
	public string level;
	// Use this for initialization

	void OnTriggerEnter(Collider other) {
		Application.LoadLevel (level);
	}

	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
