using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{


    public GameObject TaskGather;
    public GameObject TaskGossip;
    public GameObject TaskGoHome;

    
    public Image Progress;

    public Button GatherSpot;

    // Start is called before the first frame update
    void Start()
    {
        GatherSpot.onClick.AddListener(GatherGossip);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GatherGossip()
    {
        Progress.fillAmount += (float) 0.1;
    }


    public void ActiveTaskGather()
    {
        TaskGather.SetActive(true);
        TaskGossip.SetActive(false);
        TaskGoHome.SetActive(false);
    }

    public void ActiveTaskGossip()
    {
        TaskGather.SetActive(false);
        TaskGossip.SetActive(true);
        TaskGoHome.SetActive(false);
    }

    public void ActiveTaskGoHome()
    {
        TaskGather.SetActive(false);
        TaskGossip.SetActive(false);
        TaskGoHome.SetActive(true);
    }
}
