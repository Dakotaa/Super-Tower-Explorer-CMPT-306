using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.ConstrainedExecution;

public static class HighScoreSaver
{
    // Determine how long the high score list will be.
    public static int scoresToSave = 5;

    private static string path = Application.persistentDataPath + "/highscores" + scoresToSave.ToString() + ".save";

    /*
     * Save the scores of a HighScores object within a binary file.
     */
    public static void SaveHighScores(HighScores highScores)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, highScores);
        stream.Close();
    }

    /*
     * Load saved highscores from a binary file into a HighScores object.
     */
    public static HighScores LoadHighScores()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HighScores highScores = formatter.Deserialize(stream) as HighScores;
            stream.Close();

            return highScores;
        }
        else
        {
            int[] scores = new int[scoresToSave];
            string[] names = new string[scoresToSave];
            HighScores highScores = new HighScores(scores, names);

            return highScores;
        }
    }
}
