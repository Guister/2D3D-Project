using UnityEngine;
using System.Collections;

public class AlwaysFail : SingleChildBranchTask {

	public AlwaysFail(Task c):base(c){
	}
	
	public AlwaysFail(){}

	public override void ChildSucceeded (Task child)
	{
		ChildFailed (child);
	}

	public override void ChildFailed (Task child)
	{
		MarkFailed ();
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
