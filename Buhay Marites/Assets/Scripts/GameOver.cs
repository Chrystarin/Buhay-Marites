using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverWindow;

    public HUDController HUDControllerScript;
    public SpriteControl SpriteControlScript;
    public PlayerMovement PlayerMovementScript;
    public GameObject HUD;

    public Button btnRetry;
    public Button btnExit;

    // Start is called before the first frame update
    void Start()
    {
        btnRetry.onClick.AddListener(Retry);
        btnRetry.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverActive()
    {
        SpriteControlScript.ChangeAnimationState(SpriteControlScript.Player_GameOver);
        PlayerMovementScript.moveSpeed = 0;
        GameOverWindow.SetActive(true);
        HUD.SetActive(false);
    }

    public void GameOverInactive()
    {
        GameOverWindow.SetActive(false);
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
