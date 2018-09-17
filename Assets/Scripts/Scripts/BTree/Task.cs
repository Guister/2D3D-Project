using UnityEngine;
using System.Collections.Generic;

public abstract class Task {

	public enum Status{
		Fresh, Running, Failed, Succeeded, Cancelled
	}

	public Status status{
		get{
			return _status;
		}
	}
		
	public virtual int numChildren{
		get{
			return 0;
		}
	}

	public Task parent;
	public virtual GameObject agent {
		get;
		set;
	}

	public virtual BehaviourTree tree {
		get;
		set;
	}

	private Status _status = Status.Fresh;

	public virtual void TaskStart(){}

	public virtual void TaskEnd(){}

	public virtual void UpdateStatus(){}

	public abstract void ChildSucceeded (Task child);

	public abstract void ChildFailed (Task child);

	public abstract void ChildRunning (Task child);

	public abstract void ChildCancelled (Task child);

	public virtual void Reset(){
		_status = Status.Fresh;
		parent = null;
	}

	public virtual void MarkRunning(){
		_status = Status.Running;
		if (parent != null) {
			parent.ChildRunning (this);
		}
	}

	public virtual void MarkSuccess(){
		_status = Status.Succeeded;
		TaskEnd ();
		if (parent != null) {
			parent.ChildSucceeded (this);
		}
	}

	public virtual void MarkFailed(){
		_status = Status.Failed;
		TaskEnd();
		if (parent != null) {
			parent.ChildFailed (this);
		}
	}

	public virtual void MarkCancelled(){
		_status = Status.Succeeded;
		TaskEnd ();
		if (parent != null) {
			parent.ChildCancelled (this);
		}
	}
}
