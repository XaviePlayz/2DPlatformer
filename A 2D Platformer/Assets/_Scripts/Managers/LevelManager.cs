using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int score;

    public Text totalDeathPointsText;
    public Text totalVictoryPointsText;

    public Text GameScoreText;
    public GameObject GameOverScreen;
    public GameObject VictoryScreen;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {       
        if (GameOverScreen.activeInHierarchy)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        GameScoreText.text = score.ToString();
        GameScoreText.text = score.ToString();
    }
}
