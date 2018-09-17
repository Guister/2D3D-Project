using UnityEngine;
using System.Collections;

public class ButtonGoingDown : MonoBehaviour {

	public float Groundposition ;
	public GameObject door ;
	public GameObject player;
	float initialposition;
	public bool buttonpressed = false;



	void OnCollisionEnter( Collision colli ){

		player.GetComponent<Transform>().parent = gameObject.transform;
	}


	void  OnTriggerEnter (Collider coli){

		if (transform.position.y > Groundposition) {

			buttonpressed = true;

		
		}

	}

	/* void OnTriggerExit (Collider coli){
		
		buttonpressed = false;

		
	}
*/
	// Use this for initialization
	void Start () {
		initialposition= gameObject.transform.position.y ;
	}
	
	// Update is called once per frame
	void Update () {
	


		if (buttonpressed == false && transform.position.y < initialposition) {
			gameObject.transform.Translate (0, 0.01f, 0);
			door.GetComponent<BoxCollider> ().enabled = true;
			door.GetComponent<SpriteRenderer> ().enabled = true
				;

		} else if (buttonpressed == true && transform.position.y > Groundposition) {
			gameObject.transform.Translate (0, -0.01f, 0);

		} else if (buttonpressed == true && gameObject.transform.position.y <= Groundposition) {
			Debug.Log ("doing This");
			door.GetComponent<BoxCollider> ().enabled = false;
			door.GetComponent<SpriteRenderer> ().enabled = false;

		}
	
}
}
