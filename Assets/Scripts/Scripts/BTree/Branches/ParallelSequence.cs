using UnityEngine;
using System.Collections;

public class ParallelSequence : BranchTask {

	private int numSucceeded;

	public override void UpdateStatus ()
	{
		if (status == Status.Running) {
			numSucceeded = 0;
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
				if (status == Status.Failed) {
					return;
				}
			}
			if (numSucceeded == numChildren) {
				MarkSuccess ();
			} else {
				MarkRunning ();
			}
		}
	}

	public override void ChildSucceeded (Task child){
		numSucceeded++;
	}

	public override void ChildFailed (Task child){
		MarkFailed ();
	}

	public override void ChildRunning (Task child){

	}

	public override void ChildCancelled (Task child){
		MarkCancelled ();
	}
}
