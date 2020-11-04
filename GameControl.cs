using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private List<string> enemies = new List<string>();

    public List<string> GetEnemies()
    {
        return enemies;
    }

    public void SetEnemies(List<string> value)
    {
        enemies = value;
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
    }
}
