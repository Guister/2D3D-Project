using UnityEngine;
using System.Collections.Generic;

public abstract class BranchTask : Task {

	public override int numChildren {
		get {
			return _children.Count;
		}
	}

	public override GameObject agent {
		get {
			return base.agent;
		}
		set {
			base.agent = value;
			foreach (Task t in _children) {
				t.agent = value;
			}
		}
	}

	public override BehaviourTree tree {
		get {
			return base.tree;
		}
		set {
			base.tree = value;
			foreach (Task t in _children) {
				t.tree = value;
			}
		}
	}

	public List<Task> children{
		get{
			return _children;
		}
	}

	protected List<Task> _children = new List<Task>();


	public virtual int AddChild(Task child){
		int index = _children.Count;
		_children.Add (child);
		child.parent = this;
		child.agent = agent;
		child.tree = tree;
		return index;
	}

	public virtual bool RemoveChild(Task child){
		bool ret = _children.Remove (child);
		if (ret){
			if (child.parent == this) {
				child.parent = null;
			}
			if (child.agent = agent) {
				child.agent = null;
			}
			if (child.tree == tree) {
				child.tree = null;
			}
		}
		return ret;
	}

	public int ClearChildren(){
		int num = _children.Count;
		foreach (Task child in _children) {
			if (child.parent == this) {
				child.parent = null;
			}
			if (child.agent = agent) {
				child.agent = null;
			}
			if (child.tree == tree) {
				child.tree = null;
			}
		}
		_children.Clear ();
		return num;
	}

	public override void MarkCancelled ()
	{
		base.MarkCancelled ();
		foreach (Task t in _children) {
			if (t.status == Status.Running) {
				t.MarkCancelled ();
			}
		}
	}

	public override void Reset(){
		base.Reset ();
		foreach (Task t in _children) {
			t.Reset ();
		}
	}

}
