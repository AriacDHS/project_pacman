using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AnimationSprites deathSprites;
    public SpriteRenderer spriteRenderer { get; private set; }
    public new Collider2D collider { get; private set; }
    public Movement movement;
    public AudioSource playerAudioSource;
    public float timer, duration;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }
    //Inputs de movimentação
    void Update()
    {
        if(GameManager.isMoving)
        {
            timer+=Time.deltaTime;
            if(timer>=duration)
            {
                timer=0;
                playerAudioSource.Play();
            }            
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && GameManager.isMoving)
        {
            movement.SetDirection(Vector2.up);
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && GameManager.isMoving)
        {
            movement.SetDirection(Vector2.left);
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && GameManager.isMoving)
        {
            movement.SetDirection(Vector2.down);
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && GameManager.isMoving)
        {
            movement.SetDirection(Vector2.right);
        }

        //rotação do personagem
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }


    public void ResetState()
    {
        movement.Reset();
        this.enabled = true;
        this.spriteRenderer.enabled = true;
        this.collider.enabled = true;
        deathSprites.enabled = false;
        deathSprites.spriteRenderer.enabled = false;
        gameObject.SetActive(true);

    }

    public void DeathSprites()
    {
        this.enabled = false;
        this.spriteRenderer.enabled = false;
        this.collider.enabled = false;
        this.movement.enabled = false;
        this.deathSprites.enabled = true;
        this.deathSprites.spriteRenderer.enabled = true;
        this.deathSprites.ResetAnimation();
    }
}
