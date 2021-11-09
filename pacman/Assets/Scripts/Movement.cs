using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public float speed = 9.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask wallsLayer;

    public Vector2 direction {get; private set;}
    public Vector2 nextDirection {get; private set;}

    public Vector3 startPosition;
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Start()
    {
        Reset();
    }

    // isKinematic utilizado para os fantasmas
    public void Reset()
    {
        speedMultiplier = 1.0f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startPosition;
        rigidbody.isKinematic = false;
        this.enabled = true;
    }

    //movimentação
    void Update()
    {
        if(nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if(forced || !canMove(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }

    //teste de colisão com a layer de paredes
    public bool canMove(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, wallsLayer);
        return hit.collider != null;
    }
}
