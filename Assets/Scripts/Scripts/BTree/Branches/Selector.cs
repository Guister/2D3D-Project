using UnityEngine;
using System.Collections;

public class Selector : SingleRunningBranchTask {

	public override void ChildFailed (Task child)
	{
		if (currTaskIndex + 1 >= numChildren) {
			Debug.Log ("Sequence - all children failed! Selector Failed");
			MarkFailed ();
		} else {
			Debug.Log ("Sequence - child failed! trying next...");
			currTaskIndex++;
			currentTask.MarkRunning ();
			currentTask.TaskStart ();
			MarkRunning ();
		}
	}

	public override void ChildSucceeded (Task child)
	{
		MarkSuccess ();
	}

	public override void ChildCancelled (Task child)
	{
		MarkCancelled ();
	}

	public override void ChildRunning (Task child)
	{
		MarkRunning ();
	}
}
