using UnityEngine;
using System.Collections;

public class BehaviourTree: SingleChildBranchTask {

	//root = child
	public Task root{
		get{
			return child;
		}
		set{
			child = value;
		}
	}

	public bool hasRoot{
		get{
			return hasChild;
		}
	}

	public BehaviourTree(GameObject agent){
		this.agent = agent;
		tree = this;
	}

	public override void UpdateStatus ()
	{
		if (hasRoot) {
			Task r = root;
			if (r.status != Status.Running) {
				r.MarkRunning ();
				r.TaskStart ();
				r.UpdateStatus ();
			} else {
				r.UpdateStatus ();
			}
		}
	}

	public override void ChildCancelled (Task child)
	{
		MarkCancelled ();
	}

	public override void ChildFailed (Task child)
	{
		MarkFailed ();
	}

	public override void ChildRunning (Task child)
	{
		MarkRunning ();
	}

	public override void ChildSucceeded (Task child)
	{
		MarkSuccess ();
	}

	/*public void UpdateTree(){
		if (root != null) {
			if (root.status != Status.Running) {
				root.MarkRunning ();
				root.TaskStart ();
				root.UpdateStatus ();
			} else {
				root.UpdateStatus ();
			}
		}
	}*/

}
