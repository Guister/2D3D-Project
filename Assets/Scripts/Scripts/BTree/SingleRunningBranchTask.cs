using UnityEngine;
using System.Collections.Generic;

public abstract class SingleRunningBranchTask : BranchTask {

	protected List<Task> shuffledChildren;
	protected int currTaskIndex = -1;

	protected virtual Task currentTask{
		get{
			if (hasCurrentTask) {
				if (shuffledChildren != null) {
					return shuffledChildren [currTaskIndex];
				}
				return _children [currTaskIndex];
			}
			return null;
		}
	}

	protected virtual bool hasCurrentTask{
		get{
			return currTaskIndex >= 0 && currTaskIndex < numChildren;
		}
	}

	public override void UpdateStatus ()
	{
		if (hasCurrentTask) {
			currentTask.UpdateStatus();
		}
	}

	public override void TaskStart ()
	{
		base.TaskStart ();
		if (shuffledChildren != null) {
			ShuffleTaskList ();
		}
		currTaskIndex = 0;
		Task t = currentTask;
		t.parent = this;
		t.agent = agent;
		t.tree = tree;
		t.TaskStart ();
		t.MarkRunning ();
	}

	public override void Reset ()
	{
		base.Reset ();
		currTaskIndex = -1;
	}

	public override int AddChild (Task child)
	{
		int ret = base.AddChild (child);
		child.parent = this;
		if (shuffledChildren != null) {
			shuffledChildren.Add (child);
		}
		return ret;
	}

	public override bool RemoveChild (Task child)
	{
		if (child.parent == this) {
			child.parent = null;
		}
		bool ret = base.RemoveChild (child);
		if (ret && shuffledChildren != null) {
			shuffledChildren.Remove (child);
		}
		return ret;
	}

	protected void UseRandChildren(){
		if (shuffledChildren == null) {
			shuffledChildren = new List<Task> ();
			if (numChildren > 0) {
				foreach (Task t in _children) {
					shuffledChildren.Add (t);
				}
			}
		}
	}

	//utility method used by RandomSelector and RandomSequence to shuffle children
	protected void ShuffleTaskList(){
		if (shuffledChildren == null) {
			return;
		}
		//shuffle
		int n = shuffledChildren.Count;  
		while (n > 1) {  
			n--;  
			int k = Random.Range (0, n);
			Task value = shuffledChildren[k];  
			shuffledChildren[k] = shuffledChildren[n];  
			shuffledChildren[n] = value;  
		}  
	}
}
