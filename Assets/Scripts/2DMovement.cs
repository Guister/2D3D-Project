using UnityEngine;
using System.Collections;

public class dsMovement : MonoBehaviour {


	public GameObject popop; 
	public float speed = 0.1f;
	float movingfoward;
	float movingdepth;
	float velocity;
	public bool moving;
	public bool grounded;
	public float jumpingSpeed;



	Vector2 movement;



	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		movingfoward = Input.GetAxis ("Horizontal");




		if (moving) {
			movement = new Vector2 (movingfoward, 0.0f);

			transform.Translate (movement * speed);
		}

		if (Input.GetKey (KeyCode.Space) && grounded == true) {
			GetComponent<Rigidbody> ().AddForce (new Vector3 (0, jumpingSpeed, 0), ForceMode.Impulse);
			grounded = false;
			Debug.Log("2D Jump");
		} 
		else if (grounded == false) {
			movement = new Vector3 (movingfoward * 0.1f, GetComponent<Rigidbody> ().velocity.y, 0);
			transform.Translate (movement * speed);
		}

		if (Input.GetKey (KeyCode.UpArrow) && moving) {
			if (transform.localScale.x < 2.0) {
				transform.localScale += new Vector3 (0.005f, 0.005f, 0);
				transform.position = new Vector3 (transform.position.x, transform.position.y + 0.010f, transform.position.z); 
			}
		}

		if (Input.GetKey (KeyCode.DownArrow) && moving) {
			if (transform.localScale.x > 0.5f) {
				transform.localScale -= new Vector3 (0.005f, 0.005f, 0);
				transform.position = new Vector3 (transform.position.x, transform.position.y - 0.010f, transform.position.z); 
			}
		}
	}

	void OnCollisionEnter (Collision collision) {
		grounded = true;
	}

	void OnCollisionExit (Collision collision) {
		grounded = false;
	}
}
