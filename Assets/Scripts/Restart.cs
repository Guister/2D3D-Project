using UnityEngine;
using System.Collections;





public class Restart : MonoBehaviour {
	


	void position(){
		Application.LoadLevel (Application.loadedLevel);
		Time.timeScale = 1;
	}

	public void RunFunction(){
		position ();
	}

	// Use this for initialization
	void Start () {


	}



	
	// Update is called once per frame
	void Update () {
	

	}
}
