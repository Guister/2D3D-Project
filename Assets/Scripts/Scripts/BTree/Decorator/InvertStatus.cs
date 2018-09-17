using UnityEngine;
using System.Collections;

public class InvertStatus : SingleChildBranchTask
{

	public InvertStatus(){}

	public InvertStatus(Task c):base(c){
	}

	public override void ChildFailed (Task child)
	{
		MarkSuccess ();
	}

	public override void ChildSucceeded (Task child)
	{
		MarkFailed ();
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
