using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyBehaviour
{
    //mudança de estado
    private void OnDisable()
    {
        enemy.away.Enable();
    }

    //Estado de perseguição
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if(node != null && this.enabled && !enemy.scarried.enabled)
        {
            Vector2 enemyDirection = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach(Vector2 direction in node.directions)
            {
                Vector3 newPosition = transform.position + new Vector3(direction.x, direction.y, 0.0f);
                float distance = (enemy.player.position - newPosition).sqrMagnitude;

                //atribui o valor da menor distancia entre o player e o inimigo
                if(distance < minDistance)
                {
                    enemyDirection = direction;
                    minDistance = distance;
                }
            }
            enemy.movement.SetDirection(enemyDirection);
        }
    }
}
