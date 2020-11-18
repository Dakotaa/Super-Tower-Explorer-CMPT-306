using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float countdown;
    private bool isTimerRunning;
    private CellGrid grid;
    int[] pos;

    // Start is called before the first frame update
    void Start()
    {
        countdown = Random.Range(1, 2);
        isTimerRunning = true;
        grid = gameObject.GetComponentInParent<CellGrid>();
        pos = grid.GetPosAtCoord(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                countdown = 0;
                isTimerRunning = false;
                Destroy(gameObject);
                if (gameObject.GetComponent<TreeTile>() != null)
                {
                    grid.CreateTile(pos[0], pos[1], grid.treeTile);
                }
                else if (gameObject.GetComponent<MetalTile>() != null)
                {
                    grid.CreateTile(pos[0], pos[1], grid.metalTile);
                }
                else if (gameObject.GetComponent<StoneTile>() != null)
                {
                    grid.CreateTile(pos[0], pos[1], grid.stoneTile);
                }
            }
        }
    }
}
