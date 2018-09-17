using UnityEngine;
using System.Collections;

public class BTreeTests : MonoBehaviour {

	BehaviourTree btree;
	public bool IsCatching = true;
	public FollowPath followPath;
	public Pursuit pursuit;
	public GameObject Target;
	public Material material;


	private isCatching _isCatching;
	private Patrolling _patrolling;
	private Chasing _chasing;

	// Use this for initialization
	void Start () {

		followPath = GetComponent<FollowPath> ();
		pursuit = GetComponent<Pursuit> ();

		btree = new BehaviourTree(gameObject);
		material = GetComponent<Renderer> ().material;

		_isCatching = new isCatching (this);
		_chasing = new Chasing (this);
		_patrolling = new Patrolling (this);

		//RandomColorTask colorTask = new RandomColorTask (material);
		//WaitTask waitTask = new WaitTask (WaitForSeconds);

		Selector root = new Selector ();

		Sequence Pursuit = new Sequence ();
		Pursuit.AddChild (_isCatching);
		Pursuit.AddChild (_patrolling);

		Selector FollowPath = new Selector ();
		FollowPath.AddChild (_isCatching);
		FollowPath.AddChild (_chasing);

		root.AddChild (Pursuit);
		root.AddChild (FollowPath);
		btree.root = root;


	}
	
	// Update is called once per frame
	void Update () {
		btree.UpdateStatus ();
	}
}
