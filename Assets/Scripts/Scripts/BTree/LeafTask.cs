using UnityEngine;
using System.Collections;

public abstract class LeafTask : Task {


	public override int numChildren {
		get {
			return 0;
		}
	}

	public virtual void OnTaskStart (){
		
	}
	public virtual void OnTaskEnd(){
	}

	public abstract Status ExecuteState ();

	public override void UpdateStatus ()
	{
		if (status == Status.Running) {
			Status s = ExecuteState ();
			switch (s) {
			case Status.Succeeded:
				MarkSuccess ();
				break;
			case Status.Failed:
				MarkFailed ();
				break;
			case Status.Running:
				MarkRunning ();
				break;
			default:
				Debug.Log ("LeafTask - returned invalid status: " + s.ToString());
				break;
			}
		}
	}

	public override void TaskEnd ()
	{
		base.TaskEnd ();
		OnTaskEnd ();
	}

	public override void TaskStart ()
	{
		base.TaskStart ();
		OnTaskStart ();
	}

	public override void ChildCancelled (Task child){}
	public override void ChildFailed (Task child){}
	public override void ChildRunning (Task child){}
	public override void ChildSucceeded (Task child){}

}
