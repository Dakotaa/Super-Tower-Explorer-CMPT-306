using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTile : MonoBehaviour {

	/* returns a string of information about the tower to be used for tooltips */
	public virtual string GetInfo() {
		string info = this.name;
		return info;
	}

}
