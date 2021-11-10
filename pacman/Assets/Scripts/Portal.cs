using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destiny;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = other.transform.position;
        position.x = destiny.position.x;
        position.y = destiny.position.y;
        other.transform.position = position;
    }
}
