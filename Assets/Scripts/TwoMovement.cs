using UnityEngine;
using System.Collections;

public class TwoMovement : MonoBehaviour {

	public GameObject MovementCheck;
	public float speed = 0.1f;
	float movingfoward;
	float movingdepth;
	bool moving;
	Vector2 movement;
	public bool grounded = true;
	public float jumpingSpeed;
	bool colliding;
	public float cooldown;


	void OnTriggerEnter( Collider col){

		if (col.tag == "Deathfall") {
			Debug.Log ("Your Death");
			GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = true;
			Time.timeScale = 0;
		}
	}

	void OnCollisionEnter (Collision collision) {
		grounded = true;
	}

	void OnCollisionExit (Collision collision) {
		grounded = false;
	}

	// Use this for initialization
	void Start () {
		MovementCheck = GameObject.Find ("MovementCheck");


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		grounded =  GameObject.Find ("2DGroundCheck").GetComponent<GroundedCheck> ().grounded;


		moving = MovementCheck.GetComponent<Collisionss> ().collison;


		if (!moving) {
			


			movingfoward = Input.GetAxis ("Horizontal");

			transform.Translate (-movingfoward * speed, 0, 0);
		
			if (Input.GetKey (KeyCode.Space) && grounded == true && Time.time >= cooldown)
			{
				GetComponent<Rigidbody> ().AddForce (new Vector3 (0, jumpingSpeed, 0), ForceMode.Impulse);
				grounded = false;
				Debug.Log ("2D Jump");
				cooldown = Time.time + 0.05f;


			}

			else if (grounded == false) {
				movement = new Vector3 (movingfoward * 0.1f, GetComponent<Rigidbody> ().velocity.y, 0);
				transform.Translate (movement * speed);

			}


			if (Input.GetKey (KeyCode.UpArrow)) {
				if (transform.localScale.x < 1.5) {
					transform.localScale += new Vector3 (0.005f, 0.005f, 0);
					transform.position = new Vector3 (transform.position.x, transform.position.y + 0.010f, transform.position.z); 
				}
			}

			if (Input.GetKey (KeyCode.DownArrow)) {
				if (transform.localScale.x > 0.7f) {
					transform.localScale -= new Vector3 (0.005f, 0.005f, 0);
					transform.position = new Vector3 (transform.position.x, transform.position.y - 0.010f, transform.position.z); 
				}
			}
		
		}

			}

		}
	

