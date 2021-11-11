using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHome : EnemyBehaviour
{
    public Transform indoor;
    public Transform outdoor;

    private void OnEnable() 
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if(gameObject.activeSelf)
        {
            StartCoroutine(ExitHome());
        } 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(this.enabled && other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            enemy.movement.SetDirection(-enemy.movement.direction);
        }
    }

    //transforma o rigidbody em kinematic para atravessar as paredes e realiza a animação inicial dos fantasmas que iniciam trancados
    private IEnumerator ExitHome()
    {
        enemy.movement.SetDirection(Vector2.up, true);
        enemy.movement.rigidbody.isKinematic = true;
        enemy.movement.enabled = false;

        Vector3 position = transform.position;
        float duration = 0.5f;
        float timer = 0.0f;

        while(timer < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, indoor.position, timer/duration);
            newPosition.z = position.z;
            enemy.transform.position = newPosition;
            timer += Time.deltaTime;

            yield return null;
        }

        timer = 0.0f;

        while(timer < duration)
        {
            Vector3 newPosition = Vector3.Lerp(indoor.position, outdoor.position, timer/duration);
            newPosition.z = position.z;
            enemy.transform.position = newPosition;
            timer += Time.deltaTime;

            yield return null;
        }

        enemy.movement.SetDirection(new Vector2(Random.value <0.5f ? -1.0f : 1.0f, 0.0f), true);
        enemy.movement.rigidbody.isKinematic = false;
        enemy.movement.enabled = true;
    }
}
