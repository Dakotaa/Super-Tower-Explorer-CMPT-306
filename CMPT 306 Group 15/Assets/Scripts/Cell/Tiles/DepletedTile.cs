using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepletedTile : CellTile {
	public override string GetInfo() {
		return "Depleted resource\n\nRespawning soon.";
	}
}