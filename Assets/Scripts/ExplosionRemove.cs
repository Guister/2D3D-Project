using UnityEngine;
using System.Collections;

public class ExplosionRemove : MonoBehaviour {

	public float colliderTimer = 10;
	public Transform enemy;
	public Transform player;
	Vector3 toTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		toTarget = player.position - enemy.position;
		StartCoroutine ("ExplosionRemoval");
	}
	IEnumerator ExplosionRemoval() {
		if (toTarget.magnitude < 6f) {
			Debug.Log ("Before Waiting 5 seconds2");
			yield return new WaitForSeconds (colliderTimer);
		}
		Debug.Log ("After Waiting 10 Seconds2");
		Destroy (gameObject);
	}
}
