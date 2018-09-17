using UnityEngine;
using System.Collections;


public class Tile : MonoBehaviour, IGraphNode {

	public float totalNodeCost {
		get;
		set;
	}

	public float realNodeCost{
		get;
		set;
	}

	public float heuristicNodeCost {
		get;
		set;
	}

	public Color color{
		set{
			material.color = value;
		}
	}

	public float width = 1f, height = 1f;
	public MeshRenderer meshRenderer;

	public int indexX, indexZ;
	public bool isObstacle = false; 

	private Material material;

	void Awake(){
		transform.localScale = new Vector3 (width, 1f, height);
		material = meshRenderer.material;
	}

	void OnDestroy(){
		Destroy (material);
	}

	public int CompareTo(object o){
		Tile t = o as Tile;
		return totalNodeCost.CompareTo (t.totalNodeCost);
	}
}
