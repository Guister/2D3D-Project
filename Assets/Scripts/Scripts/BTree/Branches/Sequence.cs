using UnityEngine;
using System.Collections;

public class Sequence : SingleRunningBranchTask {

	public override void UpdateStatus ()
	{
		base.UpdateStatus ();
		//Debug.Log ("Sequence - running task: " + currTaskIndex);
	}

	public override void TaskStart ()
	{
		base.TaskStart ();
		currTaskIndex = 0;
	}

	public override void ChildSucceeded (Task child)
	{
		if (currTaskIndex + 1 >= numChildren) {
			MarkSuccess ();
		} else {
			//Debug.Log ("Sequence - child succeeded!");
			currTaskIndex++;
			currentTask.MarkRunning ();
			currentTask.TaskStart ();
			MarkRunning ();
		}
	}

	public override void ChildFailed (Task child)
	{
		MarkFailed ();
		//Debug.Log ("Sequence - ChildFailed");
	}

	public override void ChildRunning (Task child)
	{
		MarkRunning ();
	}

	public override void ChildCancelled (Task child)
	{
		MarkCancelled ();
	}
}
