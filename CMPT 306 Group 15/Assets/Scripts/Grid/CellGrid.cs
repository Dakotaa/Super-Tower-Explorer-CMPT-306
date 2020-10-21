using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour {
	public int size;
	public GameObject[,] grid;
	public GameObject squareTile;
	private float cellSize;
	private float tileScale;
	private Vector3 origin;

	void Start() {
		grid = new GameObject[size, size];

		cellSize = GetComponent<BoxCollider>().bounds.size.x;
		origin = GetComponent<BoxCollider>().transform.position;
		origin.x -= cellSize / 2;
		origin.y -= cellSize / 2;

		tileScale = cellSize / size;

		CreateTiles();
	}

	private void CreateTiles() {
		for (int x = 0; x < size; x++) {
			for (int y = 0; y < size; y++) {
				Vector3 pos = origin;
				pos.x += tileScale * x;
				pos.y += tileScale * y;
				pos.z -= 0.01f;
				GameObject tile = Instantiate(squareTile, pos, Quaternion.identity);
				tile.name = "[" + x + "," + y + "]";
				tile.transform.localScale = new Vector3(tileScale, tileScale, 1);
				tile.transform.parent = GetComponent<Transform>();
				grid[x, y] = tile;
			}
		}
	}

	void Update() { }
}
