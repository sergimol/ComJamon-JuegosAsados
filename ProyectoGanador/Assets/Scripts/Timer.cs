using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float startTime;

    private Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime > 0)
        {
            //Si ha perdido
            startTime -= Time.deltaTime;

            int seconds = (int)Math.Truncate(startTime % 60);

            string minutes = ((int)Math.Truncate(startTime / 60)).ToString();

            string zero = "";

            if (seconds < 10)
                zero = "0";

            timerText.text = minutes + ":" + zero + seconds.ToString();
        }
        else
        {
            SceneManager.LoadScene("ScoreScene");
        }
    }
}
