using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StarterAssets
{
    public class Timer : MonoBehaviour
    {

        [SerializeField]
        float startTime;
        [SerializeField]
        Transform tv;
        [SerializeField]
        Transform scText;
        [SerializeField]
        GameObject secondCam;
        [SerializeField]
        GameObject chara;

        [SerializeField]
        ClassificationManager clasMan;

        bool finished = true;
        Vector3 newPos;
        Vector3 newTextPos;

        float endTime = 4;
        private Text timerText;
        // Start is called before the first frame update
        void Start()
        {
            timerText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            if (clasMan.win)
            {
                startTime = 0;
            }

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
            else if (finished)
            {
                newPos = Camera.main.transform.position + Camera.main.transform.forward * 4;
                newPos.y += 10;

                tv.position = newPos;

                newPos.y -= 10;
                newTextPos = scText.position;
                newTextPos.y -= 150;

                tv.rotation = Camera.main.transform.rotation;

                secondCam.gameObject.SetActive(true);
                //tv.gameObject.GetComponent<Animator>().enabled = true;
                chara.GetComponent<FirstPersonController>().enabled = false;
                finished = false;
            }
            else
            {
                tv.position = Vector3.Lerp(tv.position, newPos, 0.025f);
                if (tv.position.y < newPos.y + 0.1f)
                {
                    scText.position = Vector3.Lerp(scText.position, new Vector3(scText.position.x, newTextPos.y, scText.position.z), 0.025f);
                }

                endTime -= Time.deltaTime;
                if(endTime < 0)
                {
                    SceneManager.LoadScene("MainMenu");
                    Cursor.visible = true;
                }
            }
        }

        public void setTimeZero()
        {
            startTime = 0.001f;
        }
    }
}