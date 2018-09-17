using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public FollowPath followPath;
	public GameObject target;
	public Pursuit pursuit;
	public Wander wander;
	public Arrive arrive;
	public Vector3 targetPoint;

	public float boredTimer = 10.0f;
	public float reportDelay = 0.5f;
	public float captureDelay = 0.5f;
	public float reach = 1f;
	public float agressionRange = 20.0f;

	//private FSM _fsm;
	private Vector3 _position;
	public Vector3 Position { get{ return _position;}}

	// Use this for initialization
	void Start () {
		//_fsm = GetComponent<FSM> ();
		//
		//_fsm.LoadState<EnemyArrive> ();
		//_fsm.LoadState<EnemyCapture> ();
		//_fsm.LoadState<EnemyPursuit> ();
		//_fsm.LoadState<EnemyWait> ();
		//_fsm.LoadState<WanderEnemy> ();
	}
	
	// Update is called once per frame
	void Update () {
		_position = this.transform.position;
	}
}
