using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject MenuWindow;

    public Button btnPlay;

    public GameObject LevelsWindow;

    public Button btnLevels;
    public Button btnLevelsExit;
    public Button btnLevelsOne;
    public Button btnLevelsTwo;
    public Button btnLevelsThree;
    public Button btnLevelsFour;
    public Button btnLevelsFive;


    public GameObject CreditsWindow;
    public Button btnCredits;
    public Button btnCreditsExit;

    public Button btnQuit;


    // Start is called before the first frame update
    void Start()
    {
        btnPlay.onClick.AddListener(PlayOnClick);

        btnLevels.onClick.AddListener(LevelsOnClick);
        btnLevelsExit.onClick.AddListener(LevelsExitOnClick);
        btnLevelsOne.onClick.AddListener(LevelsOneOnClick);
        btnLevelsTwo.onClick.AddListener(LevelsTwoOnClick);
        btnLevelsThree.onClick.AddListener(LevelsThreeOnClick);
        btnLevelsFour.onClick.AddListener(LevelsFourOnClick);
        btnLevelsFive.onClick.AddListener(LevelsFiveOnClick);

        btnCredits.onClick.AddListener(CreditsOnClick);
        btnCreditsExit.onClick.AddListener(CreditsExitOnClick);

        btnQuit.onClick.AddListener(QuitOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOnClick()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void LevelsOnClick()
    {
        MenuWindow.SetActive(false);
        LevelsWindow.SetActive(true);
    }

    public void LevelsExitOnClick()
    {
        MenuWindow.SetActive(true);
        LevelsWindow.SetActive(false);
    }

    public void LevelsOneOnClick()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void LevelsTwoOnClick()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    public void LevelsThreeOnClick()
    {
        SceneManager.LoadScene("LevelThree");
    }
    public void LevelsFourOnClick()
    {
        SceneManager.LoadScene("LevelFour");
    }
    public void LevelsFiveOnClick()
    {
        SceneManager.LoadScene("LevelFive");
    }
    public void CreditsOnClick()
    {
        MenuWindow.SetActive(false);
        CreditsWindow.SetActive(true);
    }
    public void CreditsExitOnClick()
    {
        MenuWindow.SetActive(true);
        CreditsWindow.SetActive(false);
    }

    public void QuitOnClick()
    {
        Application.Quit();
    }


}
