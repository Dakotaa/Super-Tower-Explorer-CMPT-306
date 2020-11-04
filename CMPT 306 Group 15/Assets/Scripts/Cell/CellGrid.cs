using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellGrid : MonoBehaviour {
	public int size;	// the dimensions of the grid
	public CellTile[,] grid;	// array of tiles
	public CellTile emptyTile;  // prefab of default tile
	public CellTile wallTile;	// prefab of wall tile
	public CellTile towerTile;	// prefab of tower tile
	private GameObject currentTile;	// the tile the mouse is currently over
	private float cellSize; // the size of the cell
	private float tileSize;
	private float tileScale;    // the scale of each tile
	private bool overCell;
	private Vector3 origin; // the position of the bottom left corner of the cell
	private GameObject canvas;

	void Start() {
		grid = new CellTile[size, size];

		cellSize = GetComponent<BoxCollider>().bounds.size.x;	// get the size of the cell
		origin = GetComponent<BoxCollider>().transform.position;	// calculate the origin point
		origin.x -= cellSize / 2;
		origin.y -= cellSize / 2;

		tileScale = cellSize / size;    // calculate the scale of each tile

		CreateTiles();  // create default tiles

		canvas = GameObject.Find("Canvas"); // Finds Canvas GameObject
	}

	private void CreateTiles() {
		for (int x = 0; x < size; x++) {
			for (int y = 0; y < size; y++) {
				CreateTile(x, y, emptyTile);
			}
		}
	}

	private void CreateTile(int x, int y, CellTile tileType) {
		Vector3 pos = origin;   // get the origin point, then move to the correct spot
		pos.x += tileScale * x;
		pos.y += tileScale * y;
		pos.z -= 0.001f;    // slightly forward, so it's in front of the cell
		grid[x, y] = Instantiate(tileType, pos, Quaternion.identity);   // create the tile
		CellTile tile = grid[x, y];
		tile.name = "[" + x + "," + y + "] " + tileType.name;
		tile.transform.localScale = new Vector3(tileScale, tileScale, 1);   // rescale tile
		tile.transform.parent = GetComponent<Transform>();  // set the tile's parent to the cell
	}

	private void PlaceTile(int[] pos, CellTile tileType) {
		CellTile replaced = grid[pos[0], pos[1]];
		if (replaced.GetType() == typeof(EmptyTile)) {  // new tiles can only be placed on empty tiles
			print(grid[pos[0], pos[1]].name);
			Destroy(grid[pos[0], pos[1]].gameObject);
			CreateTile(pos[0], pos[1], tileType);

		}
	}

	int[] GetPosAtCursor() {
		if (overCell) { // cursor must be over the current cell
			Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			int xTile = (int)((mp.x - origin.x) / tileScale);
			int yTile = (int)((mp.y - origin.y) / tileScale);
			return new int[] { xTile, yTile };
		}
		return null;
	}

	CellTile GetTileAtCursor() {
		if (overCell) { // cursor must be over the current cell
			int[] tilePos = GetPosAtCursor();
			return grid[tilePos[0], tilePos[1]];
		}
		return null;
	}

	private void Update() {
		if (Input.GetMouseButtonUp(0)) // Code to drop/place tower tile
		{
			// For when dragged tile is tower
			GameObject tower = GameObject.Find("Tower");
			MouseTowerCreate towerCreate = tower.GetComponent<MouseTowerCreate>();
			int ironCount = canvas.GetComponent<Inventory>().GetResourceCount("Iron");
			if (overCell && (towerCreate.isTowerDragged) && (ironCount > 0)) // Checks if tower is being dragged from menu and over cell
			{
				PlaceTile(GetPosAtCursor(), towerTile);
				canvas.GetComponent<Inventory>().DecreaseResource("Iron", 1);
				towerCreate.isTowerDragged = false;
				return;
			}

			// For when dragged tile is wall
			GameObject wall = GameObject.Find("WallTile");
			MouseTowerCreate wallCreate = wall.GetComponent<MouseTowerCreate>();
			int stoneCount = canvas.GetComponent<Inventory>().GetResourceCount("Stone");
			if (overCell && (wallCreate.isTowerDragged) && (stoneCount > 0)) // Checks if wall is being dragged from menu and over cell
			{
				PlaceTile(GetPosAtCursor(), wallTile);
				canvas.GetComponent<Inventory>().DecreaseResource("Stone", 1);
				wallCreate.isTowerDragged = false;
				return;
			}
		}
	}

	private void OnMouseEnter() {
		overCell = true;
	}

	private void OnMouseExit() {
		overCell = false;
	}

    /*private void OnMouseDown() {
		if (overCell) {
			PlaceTile(GetPosAtCursor(), wallTile);
		}
	}*/

    /*private void OnMouseDown()
    {
		if (overCell && (GetTileAtCursor().GetType() == typeof(towerTile)))
        {
			canvas.GetComponent<Inventory>().IncreaseResource("Iron", 3);
		}
    }*/
}
