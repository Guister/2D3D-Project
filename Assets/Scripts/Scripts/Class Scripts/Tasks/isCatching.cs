using UnityEngine;
using System.Collections;

public class isCatching : LeafTask {

	private bool _isCatching;
	private BTreeTests _btree;

	public isCatching(BTreeTests btree)
	{
		_isCatching = btree.IsCatching;
		_btree = btree;
	}

	public override void OnTaskStart ()
	{
		_isCatching = _btree.IsCatching;
	}

	public override Status ExecuteState ()
	{
		if (_isCatching == false) 
		{
			_btree.pursuit.enabled = true;
			_btree.followPath.enabled = false;
			return Status.Succeeded;
		} 
		else 
		{
			_btree.followPath.enabled = true;
			_btree.pursuit.enabled = false;
			return Status.Failed;
		}
	}
}
