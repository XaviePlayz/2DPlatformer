using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles : MonoBehaviour
{
    public static Collectibles instance;
    public TextMeshProUGUI text;

    int score;

    void Start()
    {
        score = 0;
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int gemValue)
    {
        score += gemValue;
        text.text = score.ToString();
        LevelManager.instance.score += 15;
    }
}
