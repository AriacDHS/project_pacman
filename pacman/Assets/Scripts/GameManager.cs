using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemy[] enemies;
    public int enemyPointsMultiplier=1;
    public Player player;
    public Transform coins;

    public int score;
    public int hp;

    void Start()
    {
        GameStart();
    }
    
    private void Update()
    {
        if(hp <= 0 && Input.anyKeyDown)
        {
            GameStart();
        }
    }

    //Set das variáveis de pontuação, vida e criação do nível
    void GameStart()
    {
        Score(0);
        Health(3);
        NewLevel();
    }

    //Ativa as moedas do mapa
    void NewLevel()
    {
        foreach(Transform coin in this.coins)
        {
            coin.gameObject.SetActive(true);
        }
        ResetLevel();
    }

    //Fantasma derrotado
    public void EnemyKilled(Enemy enemy)
    {
        Score(this.score + (enemy.points * enemyPointsMultiplier));
        enemyPointsMultiplier++;
    }

    void ResetEnemyMultiplier()
    {
        enemyPointsMultiplier = 1;
    }

    //Player derrotado
    public void PlayerKilled()
    {
        player.gameObject.SetActive(false);
        Health(hp - 1);
        if(hp > 0)
        {
            Invoke("ResetLevel", 2.0f);
        }
        else{
            GameOver();
        }
    }

    //moedas e powerup
    public void CoinCatched(Coin coin)
    {
        coin.gameObject.SetActive(false);
        Score(score + coin.points);
        if(!CoinCount())
        {
            player.gameObject.SetActive(false);
            Invoke("NewLevel", 3.0f);
        }
    }
    
    public void PowerUpCoinCatched(PowerUpCoin coin)
    {
        CoinCatched(coin);
        CancelInvoke();
        Invoke("ResetEnemyMultiplier", coin.duration);    
    }

    //verifica se ainda existem moedas no mapa
    private bool CoinCount()
    {
        foreach(Transform coin in this.coins)
        {
            if(coin.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    //Reset do player e dos fantasmas
    void ResetLevel()
    {
        ResetEnemyMultiplier();

        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].ResetState();
        }
        player.ResetState();
    }


    //Game Over
    void GameOver()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(false);
        }
        player.gameObject.SetActive(false);
    }

    //UI Pontuação e vida

    void Score(int score)
    {
        this.score = score;
    }

    void Health(int hp)
    {
        this.hp = hp;
    }
}
