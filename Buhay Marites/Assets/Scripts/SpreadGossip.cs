using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpreadGossip : MonoBehaviour
{
    public HUDController HUDControllerScript;
    public GameObject Icon;
    public Image ProgressBar;

    public bool insideRange = false;
    public bool mousepressed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (insideRange)
        {
            if (Input.GetMouseButton(0))
            {
                mousepressed = true;
                if (ProgressBar.fillAmount < 1)
                {
                    ProgressBar.fillAmount += (.1f * Time.deltaTime);
                }
                else if (ProgressBar.fillAmount >= 1)
                {
                    ProgressBar.fillAmount = 0;
                    Icon.SetActive(false);
                    HUDControllerScript.ActiveTaskGoHome();

                }
            }
            else
            {
                mousepressed = false;
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
