using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mechanics : MonoBehaviour
{
    public HUDController HUDControllerScript;
    public GameObject Icon;
    public Image ProgressBar;
    public float speed;

    public bool insideRange = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (insideRange)
        {
            if (HUDControllerScript.TaskGatherActive || HUDControllerScript.TaskGossipActive)
            {
                if (Input.GetMouseButton(0))
                {
                    if (ProgressBar.fillAmount < 1)
                    {
                        ProgressBar.fillAmount += (speed * Time.deltaTime);
                    }
                    else if (ProgressBar.fillAmount >= 1)
                    {
                        ProgressBar.fillAmount = 0;
                        if (HUDControllerScript.TaskGatherActive)
                        {
                            HUDControllerScript.ActiveTaskGossip();
                        }
                        else if (HUDControllerScript.TaskGossipActive)
                        {
                            HUDControllerScript.ActiveTaskGoHome();
                        }
                        Icon.SetActive(false);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    HUDControllerScript.VictoryActive();
                    Icon.SetActive(false);
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        insideRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        insideRange = false;
    }
}
