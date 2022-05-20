using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControl : MonoBehaviour
{
    private Animator animator;

    //Animation State
    public string Player_Listening = "Player_listen";
    public string Player_Gossiping = "Player_chismising";
    public string Player_GameOver = "Player_gameover";
    public string Player_Victory = "Player_victory";
    public string Player_Idle = "Player_idle";


    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {

    }

    // to change the state of the animation
    public void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }

    public void StopAnimationState(string newState)
    {
        animator.Play(newState, -1);
    }

}
