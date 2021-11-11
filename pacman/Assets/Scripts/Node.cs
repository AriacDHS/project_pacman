using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> directions{get; private set;}
    public LayerMask wallsLayer;

    private void Start()
    {
        directions = new List<Vector2>();

        CheckDirection(Vector2.up);
        CheckDirection(Vector2.left);  
        CheckDirection(Vector2.down);  
        CheckDirection(Vector2.right);     
    }

    //checa para saber se os inimigos podem ir para a direção desejada
    private void CheckDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.45f, 0.0f, direction, 1.5f, wallsLayer);
        
        if(hit.collider == null)
        {
            directions.Add(direction);
        }
    }
}
