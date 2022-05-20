using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{


    public GameObject TaskGather;
    public GameObject TaskGossip;
    public GameObject TaskGoHome;
    public GameObject Meter;

    public GameObject GatherGossipIcon;
    public GameObject SpreadGossipIcon;
    public GameObject ReturnHomeIcon;

    public bool TaskGatherActive;
    public bool TaskGossipActive;
    public bool TaskGoHomeActive;

    public GameObject GameOverWindow;
    public GameObject VictoryWindow;

    public Button btnGameOverRetry;
    public Button btnGameOverExit;

    public Button btnVictoryRetry;
    public Button btnVictoryExit;
    public Button btnVictoryContinue;

    // Start is called before the first frame update
    void Start()
    {
        ActiveTaskGather();

        btnGameOverRetry.onClick.AddListener(Retry);
        btnGameOverExit.onClick.AddListener(Exit);

        btnVictoryRetry.onClick.AddListener(Retry);
        btnVictoryExit.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveTaskGather()
    {

        TaskGather.SetActive(true);
        TaskGossip.SetActive(false);
        TaskGoHome.SetActive(false);

        GatherGossipIcon.SetActive(true);
        SpreadGossipIcon.SetActive(false);
        ReturnHomeIcon.SetActive(false);

        TaskGatherActive = true;
        TaskGossipActive = false;
        TaskGoHomeActive = false;
    }

    public void ActiveTaskGossip()
    {

        TaskGather.SetActive(false);
        TaskGossip.SetActive(true);
        TaskGoHome.SetActive(false);

        GatherGossipIcon.SetActive(false);
        SpreadGossipIcon.SetActive(true);
        ReturnHomeIcon.SetActive(false);

        TaskGatherActive = false;
        TaskGossipActive = true;
        TaskGoHomeActive = false;
    }

    public void ActiveTaskGoHome()
    {
        Meter.SetActive(false);

        TaskGather.SetActive(false);
        TaskGossip.SetActive(false);
        TaskGoHome.SetActive(true);

        GatherGossipIcon.SetActive(false);
        SpreadGossipIcon.SetActive(false);
        ReturnHomeIcon.SetActive(true);

        TaskGatherActive = false;
        TaskGossipActive = false;
        TaskGoHomeActive = true;
    }

    public void GameOverActive()
    {
        GameOverWindow.SetActive(true);
    }

    public void GameOverInactive()
    {
        GameOverWindow.SetActive(false);
    }

    public void VictoryActive()
    {
        VictoryWindow.SetActive(true);
    }

    public void VictoryInactive()
    {
        VictoryWindow.SetActive(false);
    }

    public void Retry()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
