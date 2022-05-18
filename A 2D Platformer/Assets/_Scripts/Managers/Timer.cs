using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue;
    public Text timeText;
    public GameObject Player;

    void Start()
    {
        timeText = GetComponent<Text>();
    }
    void Update()
    {
        timeText.text = "" + Mathf.Round(timeValue);

        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }
    }        
}
