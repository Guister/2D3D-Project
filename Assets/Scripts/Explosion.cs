using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float explosionDelay = 5;
	public float colliderTimer = 6;
	public GameObject grenade;
	public GameObject explosionEffect;
	public Transform grenadeSpawn;
	public Transform enemy;
	public Transform player;
	Vector3 toTarget;

	// Use this for initialization
	void Start () {
	}

	void Update ()
	{
		toTarget = player.position - enemy.position;
		StartCoroutine("GrenadeExplosion");
		//StartCoroutine ("ExplosionRemoval");
	}
	IEnumerator GrenadeExplosion() {
		if (toTarget.magnitude < 6f) {
		Debug.Log ("Before Waiting 5 seconds");
		yield return new WaitForSeconds (explosionDelay);
		Debug.Log ("After Waiting 5 Seconds");
			Instantiate (grenade, grenadeSpawn.position, grenadeSpawn.rotation);
			Instantiate (explosionEffect, grenadeSpawn.position, grenadeSpawn.rotation);
			Destroy (gameObject);
		}
	}/*
	IEnumerator ExplosionRemoval() {
		if (toTarget.magnitude < 6f) {
			Debug.Log ("Before Waiting 5 seconds");
			yield return new WaitForSeconds (colliderTimer);
		}
			Debug.Log ("After Waiting 6 Seconds");
			Destroy (grenade);
	}
	*/
}
