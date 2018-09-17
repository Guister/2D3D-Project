using UnityEngine;
using System.Collections;

public class AlwaysSucceed : SingleChildBranchTask {

	public AlwaysSucceed(){}

	public AlwaysSucceed(Task c):base(c){
	}

	public override void ChildFailed (Task child)
	{
		ChildSucceeded (child);
	}

	public override void ChildSucceeded (Task child)
	{
		MarkSuccess ();
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
