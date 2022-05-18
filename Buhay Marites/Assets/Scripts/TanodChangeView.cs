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
    public Sprite[] idleSprites;

    public TanodFOV fov;

    public int rotationDirection;
    private Sprite alertSprite;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer alertRenderer;

    // Start is called before the first frame update
    void Start()
    {
        alertSprite = alertRenderer.sprite;
        alertRenderer.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(fov.playerVisible)
        {
            // Change sprite according to view direction using the velocity
            // TOP | BOTTOM
            if (Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x))
            {
                if (rb.velocity.y >= 0f) rotationDirection = 0;
                else                     rotationDirection = 2;
            }
            // LEFT | RIGHT
            else
            {
                if (rb.velocity.x > 0f) rotationDirection = 1;
                else                    rotationDirection = 3;
            }

            //if (spriteRenderer.sprite != idleSprites[rotationDirection])
            spriteRenderer.sprite = idleSprites[rotationDirection];
        }
        else
        {
            if (rotationDirection != RotationDirection())
            {
                spriteRenderer.sprite = idleSprites[RotationDirection()];
                rotationDirection = RotationDirection();
            }
        }

        // Show alert icon if player found
        if (viewDirection.GetComponent<TanodFOV>().playerVisible) alertRenderer.sprite = alertSprite;
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
