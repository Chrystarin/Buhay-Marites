using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    // to check if the player is in range
    public bool isInRange;

    // Detection Point
    public Transform deetectionPoint;
    
    // Detection Radius
    private const float detectionRadius = 0.2f;
    
    // Detection Layer
    public LayerMask detectionLayer;

    //Animation State
    const string Player_Listening = "Player_listen";
    const string Player_Idle = "Player_idle";

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                ChangeAnimationState(Player_Listening);
                
                if (isInRange == false)
                {
                    ChangeAnimationState(Player_Idle);
                }
            }
            
        }
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    // detecting the gather spot
    bool DetectObject()
    {
        return Physics2D.OverlapCircle(deetectionPoint.position, detectionRadius, detectionLayer);
    }

    // to change the state of the animation
    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
    }
}