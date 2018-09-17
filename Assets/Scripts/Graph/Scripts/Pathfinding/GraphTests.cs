using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphTests : MonoBehaviour {

	private GridManager _gridManager;
	private Graph _graph;
	private DijkstraGraphSearch _dijkstragraphSearch;
	private AStarGraphSearch _starGraphSearch;
	public Vector2 SourceCoordinates = new Vector2 (0, 0);
	public Vector2 TargetCoordinates = new Vector2 (10, 10);
	public IHeuristicCalculator heuristicCalc;

	public bool DiagonalEdges;
	public enum SearchTypes
	{
		Dijkstra,
		AStar,
	};

	public SearchTypes SearchType;


	// Use this for initialization
	void Start () {
		_gridManager = GetComponent<GridManager> ();
		_graph = new Graph (true);
		_dijkstragraphSearch = new DijkstraGraphSearch ();
		_starGraphSearch = new AStarGraphSearch ();

		//ADD NODES TO GRAPH
		for (int i = 0; i < _gridManager.numX; i++) 
		{
			for (int j = 0; j < _gridManager.numZ; j++)
			{
				Tile currTile = _gridManager.grid [i] [j];

				if (!currTile.isObstacle) 
				{
					_graph.AddNode (currTile);
				}
			}
		}

		//ADD EDGES TO GRAPH
		for (int i = 0; i < _gridManager.numX; i++) 
		{
			for (int j = 0; j < _gridManager.numZ; j++)
			{
				Tile currTile = _gridManager.grid [i] [j];

				if (!currTile.isObstacle) 
				{
					List<GraphEdge> edges = GetEdges (i, j);
					foreach (GraphEdge edge in edges) 
					{
						_graph.AddEdge (edge);
					}
				}
			}
		}


		heuristicCalc = new EuclideanCalculator ();
		Tile source = _gridManager.grid[(int)SourceCoordinates.x][(int)SourceCoordinates.y];
		Tile target = _gridManager.grid [(int)TargetCoordinates.x] [(int)TargetCoordinates.y];
		bool success = _starGraphSearch.Search (_graph, source, target, heuristicCalc);
		if (success) 
		{
			GameObject path = GameObject.Find ("Path");

			foreach (Transform t in _starGraphSearch.shortestPathVector) 
			{
				GameObject waypoint = new GameObject ("wayPoint");
				waypoint.transform.parent = path.transform;
				waypoint.transform.position = t.position;
			}

			GameObject player = GameObject.Find ("PinkRobot");
			FollowPath followScript = player.GetComponent<FollowPath> ();
			followScript.enabled = true;
		}
	}
	
	private List<GraphEdge> GetEdges (int x, int z)
	{
		Tile currNode = _gridManager.grid [x] [z];
		List<GraphEdge> edgeList = new List<GraphEdge> ();

		for (int i = -1; i < 2; i++) 
		{
			for (int j = -1; j < 2; j++) 
			{
				if ((j == 0 && i == 0) || x + i < 0 || x + i >= _gridManager.numX || z + j < 0 || z + j >= _gridManager.numZ)
					continue;
					
				Tile targetNode = _gridManager.grid [x + i] [z + j];

				if (targetNode.isObstacle)
					continue;
				
				float cost = 1;

				// Only the diagonals have a value 2 when we do the sum of both positive values
				if (Mathf.Abs (i) + Mathf.Abs (j) == 2) {

					//IF the Graph is not diagonal we dont need this edge
					if (!DiagonalEdges)
						continue;
					
					//If we use diagonals we can increase the cost of the edge
					cost = 2;
				} 
				GraphEdge edge = new GraphEdge (currNode, targetNode, cost); 
				edgeList.Add (edge);
			}
		}
		return edgeList;
	}

	void OnDrawGizmos() 
	{
		if (_graph == null) 
		{
			return;
		}
			
		//DEBUG GRAPH EDGES
		/*
		List<GraphEdge> edges = _graph.Edges;
		foreach (GraphEdge edge in edges) 
		{
			Tile startTile = (Tile)edge.From;
			Tile endTile = (Tile)edge.To;
			Debug.DrawLine(startTile.transform.position,  endTile.transform.position, Color.yellow);
		}*/

		//DEBUG SHORTESTPATHTREE
		Gizmos.color = Color.red;
		foreach (KeyValuePair<IGraphNode, GraphEdge> entry in _starGraphSearch.SearchFrontier) 
		{
			GraphEdge e = entry.Value;
			Tile t1 = (Tile)e.From;
			Tile t2 = (Tile)e.To;
			Gizmos.DrawLine (t1.transform.position, t2.transform.position);
		}

		Gizmos.color = Color.yellow;
		List<IGraphNode> shortestPath = _starGraphSearch.shortestPath;
		if (shortestPath.Count == 0)
			return;
		for (int i = 0; i < shortestPath.Count - 1; i++) 
		{
			Tile t1 = (Tile)shortestPath[i];
			Tile t2 = (Tile)shortestPath[i + 1];
			Gizmos.DrawLine (t1.transform.position, t2.transform.position);
		}

		Gizmos.color = Color.blue;
		List<Transform> shortestPathTransform = _starGraphSearch.shortestPathVector;
		if (shortestPathTransform.Count == 0)
			return;
		for (int i = 0; i < shortestPathTransform.Count - 1; i++) 
		{
			Transform t1 = shortestPathTransform[i];
			Transform t2 = shortestPathTransform[i + 1];
			Gizmos.DrawLine (t1.position, t2.position);
		}

	} 
}
