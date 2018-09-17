using UnityEngine;
using System.Collections;

public class RandomStatus : SingleChildBranchTask {

	public override void ChildFailed (Task child)
	{
		RandStatus ();
	}

	public override void ChildSucceeded (Task child)
	{
		RandStatus ();
	}

	public override void ChildRunning (Task child)
	{
		MarkRunning ();
	}

	public override void ChildCancelled (Task child)
	{
		MarkCancelled ();
	}

	private void RandStatus(){
		if (Random.value < 0.5f) {
			MarkSuccess ();
		} else {
			MarkFailed ();
		}
	}

}

