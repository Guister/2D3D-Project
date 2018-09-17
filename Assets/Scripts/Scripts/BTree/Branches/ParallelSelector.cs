using UnityEngine;
using System.Collections;

public class ParallelSelector : BranchTask {

	private int numFailled;

	public override void UpdateStatus ()
	{
		if (status == Status.Running) {
			numFailled = 0;
			foreach (Task t in _children) {
				Status s = t.status;
				if (s != Status.Running) {
					t.parent = this;
					t.agent = agent;
					t.tree = tree;
					t.MarkRunning ();
					t.TaskStart ();
				}
				t.UpdateStatus ();
				if (status == Status.Succeeded) {
					return;
				}
			}
			if (numFailled == numChildren) {
				MarkFailed ();
			} else {
				MarkRunning ();
			}
		}
	}

	public override void ChildSucceeded (Task child){
		MarkSuccess ();
	}

	public override void ChildFailed (Task child){
		numFailled++;
	}

	public override void ChildRunning (Task child){
		
	}

	public override void ChildCancelled (Task child){

	}
}
