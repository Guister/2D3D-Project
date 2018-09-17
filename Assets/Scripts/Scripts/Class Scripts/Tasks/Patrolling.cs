using UnityEngine;
using System.Collections;

public class Patrolling : LeafTask {

	private BTreeTests _btree;

	public Patrolling(BTreeTests btree)
	{
		_btree = btree;
	}

	public override Status ExecuteState ()
	{
		if (Mathf.Abs ((_btree.gameObject.transform.position - _btree.Target.transform.position).magnitude) >= 6f) 
		{
			_btree.IsCatching = !_btree.IsCatching;
			_btree.pursuit.enabled = false;
			_btree.followPath.enabled = true;
			return Status.Succeeded;
		}
		return Status.Running;
	}
}
