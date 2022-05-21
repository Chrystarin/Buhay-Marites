using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mechanics : MonoBehaviour
{
    public HUDController HUDControllerScript;
    public SpriteControl SpriteControlScript;
    public PlayerMovement PlayerMovementScript;
    public Timer TimerScript;

    public SpriteRenderer Range;
    public GameObject Area;
    public GameObject HUD;
    public GameObject Windows;

    public AudioSource Audio;

    public Image ProgressBar;
    public float speed;

    public bool insideRange = false;
    public bool player_movement_enabled = true;

    public float playerSpeedStorage;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeedStorage = PlayerMovementScript.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Excecute if player is in range
        if (insideRange)
        {
            Range.color = new Color((float)0.2146941, (float)1, (float)0.0518868, (float)0.3137255);
            
            //Excecute code if task is to gather or to gossip
            if (HUDControllerScript.TaskGatherActive || HUDControllerScript.TaskGossipActive)
            {
                //Excecute code while left click is held
                if (Input.GetMouseButton(0))
                {

                    if (!Audio.isPlaying)
                    {
                        Audio.Play();
                    }

                    PlayerMovementScript.moveSpeed = 0;
                    //Excecuted if progress bar isnt full
                    if (ProgressBar.fillAmount < 1)
                    {
                        //Changes sprite animation of player to listening
                        if (HUDControllerScript.TaskGatherActive)
                        {
                            if (insideRange)
                                SpriteControlScript.ChangeAnimationState(SpriteControlScript.Player_Listening);
                        }
                        //Changes sprite animation of player to gossiping
                        else if (HUDControllerScript.TaskGossipActive)
                        {
                            if (insideRange)
                                SpriteControlScript.ChangeAnimationState(SpriteControlScript.Player_Gossiping);
                        }
                        //Fills progress bar
                        ProgressBar.fillAmount += (speed * Time.deltaTime);
                    }
                    //Excecuted if progress bar is full
                    else if (ProgressBar.fillAmount >= 1)
                    {
                        
                        //Resets progress bar to zero
                        ProgressBar.fillAmount = 0;

                        SpriteControlScript.StopAnimationState(SpriteControlScript.Player_Idle);
                        PlayerMovementScript.moveSpeed = playerSpeedStorage;

                        //Excecuted if current task is to gather
                        if (HUDControllerScript.TaskGatherActive)
                        {
                            //Actives the task of gossiping
                            HUDControllerScript.ActiveTaskGossip();
                        }

                        //Excecuted if current task is to gossip
                        else if (HUDControllerScript.TaskGossipActive)
                        {
                            //Actives the task of going home
                            HUDControllerScript.ActiveTaskGoHome();
                        }

                        //Disable the area of the current task
                        Area.SetActive(false);
                    }
                }
                else{
                    //Excecute if mouse is not held
                    Audio.Stop();
                    PlayerMovementScript.moveSpeed = playerSpeedStorage;
                    SpriteControlScript.StopAnimationState(SpriteControlScript.Player_Idle);
                }
            }
            //Excecute if task is to go home
            else
            {
                if (Input.GetMouseButton(0))
                {
                    SpriteControlScript.ChangeAnimationState(SpriteControlScript.Player_Victory);
                    PlayerMovementScript.moveSpeed = 0;
                    HUD.SetActive(false);
                    HUDControllerScript.VictoryActive();
                    Area.SetActive(false);
                }
            }

        }
        //Excecute if player is out of range
        else
        {
            Range.color = new Color((float)0.509804, (float)0.509804, (float)0.509804, (float)0.3137255);
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
