using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : CellTile {
    private void Start()
    {
        var bounds = GetComponent<BoxCollider2D>().bounds;

        //Expand the bounds along the Z axis
        bounds.Expand(Vector3.forward * 1000);
        
        var guo = new GraphUpdateObject(bounds);
        guo.addPenalty = 10000;
        
        //Debug.Log("update");
        //Change some settings on the object
        AstarPath.active.UpdateGraphs(guo);
    }

	public override string GetInfo() {
		return "Wall Tile";
	}

}
