using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TambayControl : MonoBehaviour
{
    public PlayerMovement PlayerMovementScript;
    public GameObject Area;
    public SpriteRenderer Range;
    public SpriteControl SpriteControlScript;
    public AudioSource Arguing;

    public bool insideRange = false;
    public float playerSpeedStorage;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeedStorage = PlayerMovementScript.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (insideRange)
        {
            
            Range.color = new Color((float)1, (float)0.2783019, (float)0.2783019, (float)0.227451);
            PlayerMovementScript.moveSpeed =0.5f;
            SpriteControlScript.ChangeAnimationState(SpriteControlScript.Tambay_Arguing);
            
        }
        else
        {
            Range.color = new Color((float)0.509804, (float)0.509804, (float)0.509804, (float)0.3137255);
            SpriteControlScript.StopAnimationState(SpriteControlScript.Tambay_Arguing);
            PlayerMovementScript.moveSpeed = playerSpeedStorage;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        insideRange = true;
        Arguing.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        insideRange = false;
        Arguing.Stop();
    }
}
