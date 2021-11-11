using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAway : EnemyBehaviour
{
    //mudança de estado
    private void OnDisable()
    {
        enemy.chase.Enable();
    }

    //Estado de movimentação aleatória
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if(node != null && this.enabled && !enemy.scarried.enabled)
        {
            int index = Random.Range(0, node.directions.Count);

            if(node.directions[index] == enemy.movement.direction && node.directions.Count > 1)
            {
                index++;

                if(index >= node.directions.Count)
                {
                    index = 0;
                }
            }

            enemy.movement.SetDirection(node.directions[index]);
        }
    }
}
