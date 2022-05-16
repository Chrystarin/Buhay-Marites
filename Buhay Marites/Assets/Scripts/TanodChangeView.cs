using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanodChangeView : MonoBehaviour
{
    /* 
     * This Class focuses on sprite rendering
     */

    public GameObject viewDirection;
    public Sprite[] directionSprites;

    public int rotationDirection;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Change the sprite according to direction facing
        if(rotationDirection != RotationDirection())
        {
            spriteRenderer.sprite = directionSprites[RotationDirection()];
            rotationDirection = RotationDirection();
        }
    }

    int RotationDirection()
    {
        // Get the current rotation angles
        Vector3 rotateAngle = viewDirection.transform.eulerAngles;

        // Identify the which direction is facing by angles
        switch (rotateAngle.z)
        {
            case 0:
                return 0;
            case 90:
                return 3;
            case 180:
                return 2;
            case 270:
                return 1;
            default:
                return rotationDirection;
        }
    }
}
