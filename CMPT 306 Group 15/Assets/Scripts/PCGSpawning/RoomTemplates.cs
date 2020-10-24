using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] DownRooms;

    public GameObject[] LeftRooms;

    public GameObject[] UpRooms;

    public GameObject[] RightRooms;

    public GameObject[] Terrains;

    public GameObject ClosedRoom;

    public int RoomLow; 
    public int RoomHigh;

    public int RoomCount;
    public int PrevCount;

    public bool done = false;

    void Start()
    {
        InvokeRepeating("ReBuild", 0.1f, 0.1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("Game"); //Load scene called Games
        }
    }

    private void ReBuild()
    {
        PrevCount = RoomCount;
        RoomCount = -40;
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            RoomCount++;
        }

        if (PrevCount == RoomCount)
        {
            done = true;
        }
        
        //Reset the game by pressing 'r' or if the number of rooms isn't in the given range
        if (done && ((RoomCount > RoomHigh) || (RoomCount < RoomLow)))
        { 
            SceneManager.LoadScene("Game"); //Load scene called Game
        }
    }
}
