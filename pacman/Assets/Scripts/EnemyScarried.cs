using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScarried : EnemyBehaviour
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool defeated {get; private set;}

    //override utilizado para garantir o reset das sprites
    public override void Enable(float duration)
    {
        base.Enable(duration);

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        Invoke("Flash", duration/2.0f);
    }

    public override void Disable()
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;

    }

    //Debuff de velocidade
    private void OnEnable()
    {
        enemy.movement.speedMultiplier = 0.5f;
        defeated = false;
    }

    private void OnDisable()
    {
        enemy.movement.speedMultiplier = 1.0f;
        defeated = false;
    }

    private void Flash()
    {
        if(!defeated)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimationSprites>().ResetAnimation();
        }
    }

    //Mata o fantasma e faz o respawn na casa
    private void Defeated()
    {
        defeated = true;

        Vector3 position = enemy.home.indoor.position;
        position.z = enemy.transform.position.z;
        enemy.transform.position = position;

        enemy.home.Enable(duration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    //teste de colisão com o fantasma (com o debuff)
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            if(this.enabled)
            {
                Defeated();
            }
        }    
    }

    //Fugir do pacman enquanto estiver com o 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if(node != null && this.enabled)
        {
            Vector2 enemyDirection = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach(Vector2 direction in node.directions)
            {
                Vector3 newPosition = transform.position + new Vector3(direction.x, direction.y, 0.0f);
                float distance = (enemy.player.position - newPosition).sqrMagnitude;

                if(distance > maxDistance)
                {
                    enemyDirection = direction;
                    maxDistance = distance;
                }
            }
            enemy.movement.SetDirection(enemyDirection);
        }
    }
}
