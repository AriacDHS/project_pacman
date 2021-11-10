using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //{get; private set;} utilizado para manter uma melhor organização do projeto
    public Movement movement {get; private set;}
    public EnemyHome home {get; private set;}
    public EnemyAway away {get; private set;}
    public EnemyChase chase {get; private set;}
    public EnemyScarried scarried {get; private set;}
    public EnemyBehaviour initialBehaviour;
    public Transform player;
    public int points = 200;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        home = GetComponent<EnemyHome>();
        away = GetComponent<EnemyAway>();
        chase = GetComponent<EnemyChase>();
        scarried = GetComponent<EnemyScarried>();        
    }

    private void Start()
    {
        ResetState();
    }

    //reset dos estados dos inimigos
    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.Reset();
        scarried.Disable();
        chase.Disable();
        away.Enable();

        if(home != initialBehaviour)
        {
            home.Disable();
        }
        if(initialBehaviour != null)
        {
            initialBehaviour.Enable();
        }
    }

    //detecta se o pacman comeu o fantasma ou se foi morto por ele
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            if(scarried.enabled)
            {
                FindObjectOfType<GameManager>().EnemyKilled(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PlayerKilled();
            }
        }    
    }
}
