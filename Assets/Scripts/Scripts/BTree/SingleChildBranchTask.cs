using UnityEngine;
using System.Collections;

public abstract class SingleChildBranchTask : Task {

	public override int numChildren {
		get {
			return _child == null ? 0 : 1;
		}
	}

	public bool hasChild{
		get{
			return _child != null;
		}
	}

	public virtual Task child{
		get{
			return _child;
		}
		set{
			if (_child != null) {
				if (_child.parent == this) {
					_child.parent = null;
				}
				if (_child.agent == agent) {
					_child.agent = null;
				}
				if (_child.tree == tree) {
					_child.tree = null;
				}
			}
			_child = value;
			_child.parent = this;
			_child.agent = agent;
			_child.tree = tree;
		}
	}

	private Task _child;

	public SingleChildBranchTask(Task child){
		this.child = child;
	}

	public SingleChildBranchTask(){}

	public override void UpdateStatus ()
	{
		if (hasChild) {
			_child.UpdateStatus ();
		}
	}

	public override void TaskStart ()
	{
		base.TaskStart ();
		if (_child == null) {
			return;
		}
		_child.parent = this;
		_child.agent = agent;
		_child.MarkRunning ();
		_child.TaskStart ();
	}

	public override void Reset ()
	{
		base.Reset ();
		if (_child == null) {
			return;
		}
		_child.Reset ();
	}

	/*public override void ChildFailed (Task child)
	{
		base.ChildFailed (child);
		MarkFailed ();
	}

	public override void ChildRunning (Task child)
	{
		base.ChildRunning (child);
		MarkRunning ();
	}

	public override void ChildSucceeded (Task child)
	{
		base.ChildSucceeded (child);
		MarkSuccess ();
	}*/


	public override void MarkCancelled ()
	{
		base.MarkCancelled ();
		if (hasChild) {
			if (child.status == Status.Running) {
				child.MarkCancelled ();
			}
		}
	}

}
