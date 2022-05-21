using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TanodChangeView : MonoBehaviour
{
    /* 
     * This Class focuses on sprite rendering
     */

    public GameObject viewDirection;
    public GameObject alert;
    public Rigidbody2D rb;

    TanodFOV fov;
    TanodMovement move;

    Animator animator;
    string[] idleAnimations = { "Tanod_Back_Idle", "Tanod_Right_Idle", "Tanod_Front_Idle", "Tanod_Left_Idle" };
    string[] walkAnimations = { "Tanod_Back_Walking", "Tanod_Right_Walking", "Tanod_Front_Walking", "Tanod_Left_Walking" };
    string currentAnimation;

    public int rotationDirection;
    
    Sprite alertSprite;
    public SpriteRenderer alertRenderer;

    // Start is called before the first frame update
    void Start()
    {
        fov = viewDirection.GetComponent<TanodFOV>();
        move = viewDirection.GetComponent<TanodMovement>();
        animator = GetComponent<Animator>();

        alertSprite = alertRenderer.sprite;
        alertRenderer.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (move.tanodGameOver)
        {
            PlayAnimation("Tanod_GameOver");
            return;
        }

        if (fov.playerVisible)
        {
            // Show alert icon
            alertRenderer.sprite = alertSprite;

            // Change sprite according to view direction using the velocity
            if (Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x))
            {
                if (rb.velocity.y >= 0f) rotationDirection = 0; // TOP
                else                     rotationDirection = 2; // BOTTOM
            }
            else
            {
                if (rb.velocity.x > 0f) rotationDirection = 1;  // RIGHT
                else                    rotationDirection = 3;  // LEFT
            }
        }
        else
        {
            rotationDirection = RotationDirection();
        }

        if (move.isWalking)
            PlayAnimation(walkAnimations[rotationDirection]);
        else
            PlayAnimation(idleAnimations[rotationDirection]);
    }

    void PlayAnimation(string animation)
    {
        if (currentAnimation == animation) return;

        animator.Play(animation);
        currentAnimation = animation;
    }

    int RotationDirection()
    {
        // Get the current rotation angles
        Vector3 rotateAngle = viewDirection.transform.eulerAngles;

        // Identify the which direction is facing by angles
        if (rotateAngle.z >= 315 || rotateAngle.z < 45)  return 0;
        if (rotateAngle.z >= 45  && rotateAngle.z < 135) return 3;
        if (rotateAngle.z >= 135 && rotateAngle.z < 225) return 2;
        if (rotateAngle.z >= 225 && rotateAngle.z < 315) return 1;
        return rotationDirection;
    }
}
