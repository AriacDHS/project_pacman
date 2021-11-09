using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSprites : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float time = 0.25f;
    public int frame {get ; private set;}
    public bool loop = true;

    void awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //loop de animação
    private void Start()
    {
        InvokeRepeating("Walk", time, time);
    }

    //animação
    void Walk()
    {
        if(!spriteRenderer.enabled)
        {
            return;
        }
        frame++;
        if(frame >= sprites.Length && loop)
        {
            frame = 0;
        }

        if(frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }
    }

    void ResetAnimation()
    {
        frame = -1;
        Walk();
    }
}
