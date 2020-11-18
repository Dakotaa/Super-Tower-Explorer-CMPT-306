﻿using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellGrid : MonoBehaviour {
	public int size;	// the dimensions of the grid
	public CellTile[,] grid;	// array of tiles
	public CellTile emptyTile;  // prefab of default tile
	public CellTile wallTile;	// prefab of wall tile
	public CellTile towerTile;  // prefab of tower tile

	public CellTile treeTile; // prefab of tree tile
	public CellTile depletedTreeTile; // prefab of depleted tree tile
	public CellTile metalTile; // prefab of metal tile
	public CellTile depletedMetalTile; // prefab of depleted metal tile
	public CellTile stoneTile; // prefab of stone tile
	public CellTile depletedStoneTile; // prefab of depleted stone tile

	private CellTile currentTile;   // the tile the mouse is currently over
	private CellTile lastTile; // the last tile the mouse cursor was over
	private float cellSize; // the size of the cell
	private float tileSize;
	private float tileScale;    // the scale of each tile
	private bool overCell;
	private bool overTower;
	private Vector3 origin; // the position of the bottom left corner of the cell
	private GameObject canvas;
	private ToolTipController tooltip;
	private Inventory inventory;

	void Start() {
		grid = new CellTile[size, size];

		cellSize = GetComponent<BoxCollider>().bounds.size.x;	// get the size of the cell
		origin = GetComponent<BoxCollider>().transform.position;	// calculate the origin point
		origin.x -= cellSize / 2;
		origin.y -= cellSize / 2;

		tileScale = cellSize / size;    // calculate the scale of each tile

		CreateTiles();  // create default tiles

		canvas = GameObject.Find("Canvas"); // Finds Canvas GameObject

		int ResourceAmount = size / 4;
		for (int x = 0; x < ResourceAmount; x++) // Places resource nodes randomly
        {
			int random = UnityEngine.Random.Range(0, 3);
			if (random == 0)
            {
				PlaceTile(new int[] { UnityEngine.Random.Range(1, size - 1), UnityEngine.Random.Range(1, size - 1) }, treeTile);
			}
			else if (random == 1)
			{
				PlaceTile(new int[] { UnityEngine.Random.Range(1, size - 1), UnityEngine.Random.Range(1, size - 1) }, metalTile);
			}
			else if (random == 2)
			{
				PlaceTile(new int[] { UnityEngine.Random.Range(1, size - 1), UnityEngine.Random.Range(1, size - 1) }, stoneTile);
			}
		}

		tooltip = ToolTipController.instance;
		inventory = Inventory.instance;
	}

	private void CreateTiles() {
		for (int x = 0; x < size; x++) {
			for (int y = 0; y < size; y++) {
				CreateTile(x, y, emptyTile);
			}
		}
	}

	public void CreateTile(int x, int y, CellTile tileType) {
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
			// extra check to avoid index errors
			if (tilePos[0] >= 0 && tilePos[0] < size && tilePos[1] >= 0 && tilePos[1] < size) {
				return grid[tilePos[0], tilePos[1]];
			}
		}
		return null;
	}

	public int[] GetPosAtCoord(Vector3 coord)
	{
		int xTile = (int)((coord.x - origin.x) / tileScale);
		int yTile = (int)((coord.y - origin.y) / tileScale);
		return new int[] { xTile, yTile };
	}

	private void Update() {

		currentTile = GetTileAtCursor();
		if (Input.GetMouseButtonUp(0)) // Code to drop/place tiles
		{
			if (GetTileAtCursor() == null)
            {
				return;
            }

			// For when dragged tile is tower
			GameObject tower = GameObject.Find("Simple Tower");
			MouseTowerCreate towerCreate = tower.GetComponent<MouseTowerCreate>();
			int ironCount = inventory.GetResourceCount("Iron");
			if (overCell && (towerCreate.isTowerDragged) && (ironCount > 0) && (GetTileAtCursor().GetType() == typeof(EmptyTile))) // Checks if tower is being dragged from menu and over cell
			{
				PlaceTile(GetPosAtCursor(), towerTile);
				inventory.DecreaseResource("Iron", 1);
				towerCreate.isTowerDragged = false;
				return;
			}

			// For when dragged tile is wall
			GameObject wall = GameObject.Find("WallTile");
			MouseTowerCreate wallCreate = wall.GetComponent<MouseTowerCreate>();
			int stoneCount = inventory.GetResourceCount("Stone");
			if (overCell && (wallCreate.isTowerDragged) && (stoneCount > 0) && (GetTileAtCursor().GetType() == typeof(EmptyTile))) // Checks if wall is being dragged from menu and over cell
			{
				PlaceTile(GetPosAtCursor(), wallTile);
				inventory.DecreaseResource("Stone", 1);
				wallCreate.isTowerDragged = false;
				return;
			}

			if (GetTileAtCursor().GetType() != typeof(EmptyTile))
			{
				towerCreate.isTowerDragged = false;
				wallCreate.isTowerDragged = false;
			}
		}

		// handling checking which type of tile the cursor is over
		if (currentTile != null) {
			// check if this is a different tile
			if (lastTile != currentTile) {
				// new tile - update lasttile to this
				lastTile = currentTile;
				// hide the old tooltip
				tooltip.Hide();
				Type tileType = currentTile.GetType();
				// if the current tile is not empty, show a tooltip with info
				if (tileType != typeof(EmptyTile)) {
					tooltip.SetAndShow(currentTile.GetInfo());
				}
			}
		}
	}

	private void OnMouseEnter() {
		overCell = true;
	}

	private void OnMouseExit() {
		overCell = false;
	}

	private void OnMouseDown() {
		if (overCell) {

			if (currentTile is ResourceTile) {
				int[] pos = GetPosAtCursor();
				if (grid[pos[0], pos[1]].GetComponent<TreeTile>() != null)
                {
					inventory.IncreaseResource("Wood", 3);
					Destroy(grid[pos[0], pos[1]].gameObject);
					CreateTile(pos[0], pos[1], depletedTreeTile);
				}
				else if (grid[pos[0], pos[1]].GetComponent<MetalTile>() != null)
				{
					inventory.IncreaseResource("Iron", 3);
					Destroy(grid[pos[0], pos[1]].gameObject);
					CreateTile(pos[0], pos[1], depletedMetalTile);
				}
				else if (grid[pos[0], pos[1]].GetComponent<StoneTile>() != null)
				{
					inventory.IncreaseResource("Stone", 3);
					Destroy(grid[pos[0], pos[1]].gameObject);
					CreateTile(pos[0], pos[1], depletedStoneTile);
				}
				grid[pos[0], pos[1]].GetComponent<Timer>().countdown = UnityEngine.Random.Range(10, 25);
			}

			if (currentTile is Tower) {
				Tower towerTile = (Tower) currentTile;
				if (towerTile.IsUpgradable()) {
					if (!towerTile.IsMaxLevel()) {
						if (inventory.GetResourceCount("Iron") >= 1) {
							inventory.DecreaseResource("Iron", 1);
							towerTile.IncreaseLevel();
						}
					}
				}
			}
	}
	}
}
