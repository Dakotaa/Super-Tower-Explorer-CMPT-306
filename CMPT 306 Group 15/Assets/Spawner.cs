using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public GameControl gameControl = GameControl.instance;
    public Enemy fastEnemy;
    bool enabled = false;

    // Start is called before the first frame update
    void Start()
    {
        enabled = true;
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnLoop()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(4);
            var spawn = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z);

            
            Instantiate(fastEnemy, spawn, transform.rotation);
        }
    }
}
