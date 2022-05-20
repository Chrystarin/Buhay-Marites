using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image TimerCircle;

    public GameOver GameOverScript;
    public Text timerText;

    public float time;
    public float timeRemaining;
    public bool timerIsActive = false;
    public string minutes;
    public string seconds;

    


    // Start is called before the first frame update
    void Start()
    {
        timerIsActive = true;

        timerText.text = ((int)(time/60)) + ":" + ((int)(time%60));

    }

    // Update is called once per frame
    void Update()
    {
        //
        if (timerIsActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                TimerCircle.fillAmount = timeRemaining / time;

                minutes = ""+((int)(timeRemaining / 60));

                if((int)(timeRemaining % 60) > 9)
                {
                    seconds = ""+(int)(timeRemaining % 60);
                }
                else
                {
                    seconds = "0" + ((int)(timeRemaining % 60));
                }

                timerText.text = minutes  + ":" + seconds;
            }
            else
            {
                GameOverScript.GameOverActive();
                timeRemaining = 0;
                timerIsActive = false;
            }
        }
    }
}
