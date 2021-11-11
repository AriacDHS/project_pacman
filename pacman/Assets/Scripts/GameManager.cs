using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Enemy[] enemies;
    public int enemyPointsMultiplier=1;
    public Player player;
    public Transform coins;

    public int score;
    private int highscore=0;
    public int hp;
    public GameObject life1, life2, life3, gameover, options;
    public Text scoreText, highScoreText;

    public bool firstEntry = true;
    public static bool isMoving = false;
    public float timer, duration;
    public Enemy blinky;

    public AudioSource src;
    public AudioClip dead, start, ghostEating;

    void Start()
    {
        GameStart();
        timer=0.0f;
        duration=4.75f;
        if(!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetInt("highscore", 0);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void Load()
    {
        highscore = PlayerPrefs.GetInt("highscore");
    }
    
    private void Update()
    {

        //Início do jogo
        if(firstEntry)
        {
            timer+=Time.deltaTime;
            if(timer>=duration)
            {
                timer=0;
                firstEntry = false;
                isMoving = true;
                player.movement.SetDirection(Vector2.right);
                blinky.movement.SetDirection(Vector2.left);
            }
        }
        if(!firstEntry)
        {
            blinky.movement.initialDirection.x= -1.0f;
        }

        if(hp <= 0 && Input.anyKeyDown)
        {
            gameover.SetActive(false);
            GameStart();
        }

        //Elementos de UI
        scoreText.text = "Score:  " + score;
        highScoreText.text = "High Score:  " + PlayerPrefs.GetInt("highscore").ToString();

        if(hp >= 1)
        {
            life1.SetActive(true);
        }
        else{
            life1.SetActive(false);
        }
        if(hp >= 2)
        {
            life2.SetActive(true);
        }else{
            life2.SetActive(false);
        }
        if(hp == 3)
        {
            life3.SetActive(true);
        }else{
            life3.SetActive(false);
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
        src.clip = start;
        src.Play();
        isMoving = false;
        firstEntry = true;
        foreach(Transform coin in this.coins)
        {
            coin.gameObject.SetActive(true);
        }
        ResetLevel();
    }

    //Fantasma derrotado
    public void EnemyKilled(Enemy enemy)
    {
        src.clip = ghostEating;
        src.Play();
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
        src.clip = dead;
        src.Play();
        //player.gameObject.SetActive(false);
        player.DeathSprites();
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
            if(PlayerPrefs.GetInt("highscore") < score)
            {
                PlayerPrefs.SetInt("highscore", score);
                score = 0;
            }

            player.gameObject.SetActive(false);
            Invoke("NewLevel", 3.0f);
        }
    }
    
    public void PowerUpCoinCatched(PowerUpCoin coin)
    {
        for(int i = 0; i< enemies.Length; i++)
        {
            enemies[i].scarried.Enable(coin.duration);
        }
        
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
        gameover.SetActive(true);

        if(PlayerPrefs.GetInt("highscore") < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }

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
