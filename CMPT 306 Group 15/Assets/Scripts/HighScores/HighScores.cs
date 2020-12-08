using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScores
{
    public int[] scores;
    public string[] names;

    public HighScores(int[] _scores, string[] _names)
    {
        scores = _scores;
        names = _names;
    }

    /*
     * Checks if a given score would be added to the list of high scores.
     * 
     * Returns true if so, false otherwise
     */
    public bool WouldBeNewScore(int score)
    {
        if (score > scores[scores.Length - 1])
            return true;
        else
            return false;
    }

    /*
     * Add a new score and name pair to the list of high scores.
     * 
     * Returns true if the score was added, false otherwise
     */
    public bool AddNewScore(int score, string name)
    {
        if (!WouldBeNewScore(score))
            return false;

        int posToAdd = scores.Length - 1;
        while (posToAdd > 0)
        {
            if (score > scores[posToAdd - 1])
            {
                scores[posToAdd] = scores[posToAdd - 1];
                names[posToAdd] = names[posToAdd - 1];
                posToAdd--;
            }
            else
            {
                break;
            }
        }
        scores[posToAdd] = score;
        names[posToAdd] = name;

        return true;
    }
}
