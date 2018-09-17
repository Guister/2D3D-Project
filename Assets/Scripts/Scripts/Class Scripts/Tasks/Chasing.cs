using UnityEngine;
using System.Collections;

public class Chasing : LeafTask {

	private BTreeTests _btree;

	public Chasing(BTreeTests btree)
	{
		_btree = btree;
	}

	public override Status ExecuteState ()
	{
		if (Mathf.Abs ((_btree.gameObject.transform.position - _btree.Target.transform.position).magnitude) < 6f) 
		{
			_btree.IsCatching = !_btree.IsCatching;
			_btree.followPath.enabled = false;
			return Status.Failed;
		}
		return Status.Running;
	}
}
