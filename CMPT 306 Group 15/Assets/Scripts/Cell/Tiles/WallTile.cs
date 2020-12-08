using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : CellTile {
	public string wallType;
    private void Start()
    {
        var bounds = GetComponent<BoxCollider2D>().bounds;

        //Expand the bounds along the Z axis
        bounds.Expand(Vector3.forward * 1000);
        
        var guo = new GraphUpdateObject(bounds);
        guo.addPenalty = 9000;
        
        //Debug.Log("update");
        //Change some settings on the object
        AstarPath.active.UpdateGraphs(guo);
    }

	public override string GetInfo() {
		return wallType;
	}

}
