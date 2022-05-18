using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanodShowAlert : MonoBehaviour
{
    public TanodFOV fov;
    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;

        spriteRenderer.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (fov.playerVisible) spriteRenderer.sprite = defaultSprite;
    }
}
