using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
    public static int score
    {
        get
        {
            return PlayerPrefs.GetInt("score", 0);
        }
        set
        {
            PlayerPrefs.SetInt("score", value);

            if (SaveData.score > SaveData.highScore)
            {
                PlayerPrefs.SetInt("highScore", value);
            }
        }
    }

    public static int highScore
    {
        get
        {
            return PlayerPrefs.GetInt("highScore");
        }
    }
}
