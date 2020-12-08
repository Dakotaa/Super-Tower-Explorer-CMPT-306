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

    public GameObject[] DownOpen;

    public GameObject[] LeftOpen;

    public GameObject[] UpOpen;

    public GameObject[] RightOpen;

    public GameObject[] Terrains;

    public GameObject ClosedRoom;

    public GameObject HideRoom;

    private GameObject load;

    public int RoomLow; 
    public int RoomHigh;

    public int RoomCount;
    public int PrevCount;

    public bool done = false;

    void Start()
    {
        load = GameObject.FindGameObjectWithTag("LoadingScene");
        InvokeRepeating("ReBuild", 0.1f, 0.1f);
		Time.timeScale = 5f;
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
        
        //Reset the game if the number of rooms isn't in the given range
        if (done && ((RoomCount > RoomHigh) || (RoomCount < RoomLow)))
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Load scene called Game
        }
        else if (done)
        {
            Destroy(load.gameObject);
			Time.timeScale = 1f;
        }
    }



}
