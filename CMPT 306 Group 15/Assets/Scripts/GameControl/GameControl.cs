using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{


    private List<string> enemies = new List<string>(); //empty list for enemies, add enemies to list

    bool gameHasEnded = false; //current game state when starting

    public float restartDelay = 1f;

	public List<string> GetEnemies()
    {
        return enemies;
    }

    public void SetEnemies(List<string> value)
    {
        enemies = value;
    }

    public void EndGame() // handles game ending when health of life tower hits 0
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
