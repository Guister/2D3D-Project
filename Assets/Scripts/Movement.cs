
	using UnityEngine;
	using System.Collections;

	public class Movement : MonoBehaviour {

	    GameObject MovementCheck;
		public GameObject MovementCheck2D; 
		public float speed = 0.1f;
		float movingfoward;
		float movingdepth;
		public float cooldown;
		Vector3 offsetpos;
		Vector3 movement;
		Vector3 positionB;
		public bool grounded = true;
		public float jumpingSpeed;
		bool intreadmill = false;
		bool jumping = false ;  
		bool colliding ;
		bool colliding2d;

		void OnTriggerEnter( Collider col){

		if (col.tag == "Deathfall") {
				Debug.Log ("Your Death");
				GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = true;
				Time.timeScale = 0;
			}
		}


		void OnCollisionEnter(Collision collision){

		if (collision.collider.tag == "Treadmill") {
			intreadmill = true;
			grounded = true;
		} 

		}
		void OnCollisionExit(Collision collision){
		if (collision.collider.tag == "Treadmill") {

			Debug.Log ("You Exit");
			intreadmill = false;
		}
	}
			




		// Use this for initialization
		void Start () {
			positionB = transform.position;
		    MovementCheck = GameObject.Find ("MovementCheck");
			
		}

		// Update is called once per frame
		void FixedUpdate () {
		
		grounded =  GameObject.Find ("GroundCheck").GetComponent<GroundedCheck> ().grounded;
		colliding = MovementCheck.GetComponent<Collisionss> ().collison;
		colliding2d = MovementCheck2D.GetComponent<Collisionss> ().collison;

			movingfoward = Input.GetAxis ("Horizontal");
			movingdepth = Input.GetAxis ("Vertical");

		if (grounded == true && !colliding2d) {
			movement = new Vector3 (movingfoward, 0, movingdepth);
			movement.x = movingfoward * speed;
			movement.z = movingdepth * speed;
			jumping = false;

			GetComponent<Rigidbody> ().velocity = movement;

			transform.LookAt (transform.position + GetComponent<Rigidbody> ().velocity);



				

			if (Input.GetKey (KeyCode.Space) && grounded == true && Time.time >= cooldown) {
				jumping = true;
				GetComponent<Rigidbody> ().AddForce (new Vector3 (0, jumpingSpeed, 0), ForceMode.Impulse);
				cooldown = Time.time + 0.05f;
					
			}

		} else if (grounded == false && colliding == false && !colliding2d) {
			movement = new Vector3 (movingfoward * 3, GetComponent<Rigidbody> ().velocity.y, movingdepth * 3);
			GetComponent<Rigidbody> ().velocity = movement;

			transform.LookAt (transform.position + new Vector3 (GetComponent<Rigidbody> ().velocity.x, 0, GetComponent<Rigidbody> ().velocity.z));

		} else if (grounded == false && jumping == true && colliding == true) {

			movement = new Vector3 (0, -4.5f, 0);
			GetComponent<Rigidbody> ().velocity = movement; 


		} else if (colliding2d) {

			movement = new Vector3 (0, 0, 0);
		}
	

		if (intreadmill && GetComponent<Rigidbody> ().velocity.x > 0) {
			GetComponent<Rigidbody> ().velocity -= movement;
		} else if (intreadmill && GetComponent<Rigidbody> ().velocity.x == 0)
			GetComponent<Rigidbody> ().velocity =new Vector3(-2.5f , 0 , 0);
	}
		
}
	