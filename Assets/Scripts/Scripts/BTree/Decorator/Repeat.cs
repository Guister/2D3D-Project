using UnityEngine;
using System.Collections;

public class Repeat : SingleChildBranchTask {

	public int numTimes;

	private int currCount;

	public override void TaskStart ()
	{
		base.TaskStart ();
		currCount = 0;
	}

	public override void ChildSucceeded (Task child)
	{
		CountTimes ();
	}

	public override void ChildFailed (Task child)
	{
		CountTimes ();
	}

	public override void ChildRunning (Task child)
	{
		MarkRunning ();
	}

	public override void ChildCancelled (Task child)
	{
		MarkCancelled ();
	}

	private void CountTimes(){
		if (numTimes > 0) {
			currCount++;
			if (currCount >= numTimes) {
				MarkSuccess ();
				return;
			}
		}
		child.TaskStart ();
		MarkRunning ();
	}


}
