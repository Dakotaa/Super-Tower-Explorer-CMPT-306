using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (gameObject.GetComponent<AIPath>().reachedEndOfPath)
        {
            Debug.Log("DIE");
            Debug.Log("DIE");
            Debug.Log("DIE");
            Enemy.Destroy(this.gameObject,2);
        }
    }
}
