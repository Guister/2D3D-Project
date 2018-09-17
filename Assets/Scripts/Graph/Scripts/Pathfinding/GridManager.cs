using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

	public Color color1, color2;
	public Tile gridPrefab;
	public Transform obstaclePrefab;
	public int numX = 10, numZ = 10;

	public List<List<Tile>> grid;
	private GraphTests graphTests;


	void Awake(){

		graphTests = GetComponent<GraphTests> ();
		int targetX = (int)graphTests.TargetCoordinates.x;
		int targetY = (int)graphTests.TargetCoordinates.y;
		int sourceX = (int)graphTests.SourceCoordinates.x;
		int sourceY = (int)graphTests.SourceCoordinates.y;

		grid = new List<List<Tile>> ();
		for (int i = 0; i < numX; i++) {
			List<Tile> tilesX = new List<Tile> ();
			grid.Add (tilesX);
			for (int j = 0; j < numZ; j++) {
				Tile tile = Instantiate (gridPrefab) as Tile;
				float posX = (float)i * tile.width;
				float posZ = (float)j * tile.height;
				tile.transform.position = new Vector3 (posX, 0, posZ);
				tile.transform.parent = transform;
				tile.indexX = i;
				tile.indexZ = j;
				if (Random.value < 0.1f && !(i == targetX && j == targetY) && !(i == sourceX && j == sourceY)) {
					tile.isObstacle = true;
				}
				if (tile.isObstacle) {
					Transform obstacle = Instantiate (obstaclePrefab) as Transform;
					obstacle.position = tile.transform.position;
				} else {
					if (i % 2 != 0) {
						if (j % 2 == 0) {
							tile.color = color1;
						} else {
							tile.color = color2;
						}
					} else {
						if (j % 2 != 0) {
							tile.color = color1;
						} else {
							tile.color = color2;
						}
					}
				}

				tilesX.Add (tile);
			}
		}
	}

	public void GetNeighbours(Tile tile, ref List<Tile> result){
		result.Clear ();
		//left
		Tile left = GetTile(tile.indexX-1, tile.indexZ);
		if (left != null) {
			result.Add (left);
		}

		Tile right = GetTile (tile.indexX + 1, tile.indexZ);
		if (right != null) {
			result.Add (right);
		}

		Tile top = GetTile (tile.indexX, tile.indexZ + 1);
		if (top != null) {
			result.Add (top);
		}

		Tile bottom = GetTile (tile.indexX, tile.indexZ - 1);
		if (bottom != null) {
			result.Add (bottom);
		}
	}

	public Tile GetTile(int indexX, int indexZ){
		if (indexX < 0 || indexX >= numX) {
			return null;
		}
		if (indexZ < 0 || indexZ >= numZ) {
			return null;
		}
		return grid[indexX][indexZ];
	}
}
