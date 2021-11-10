using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            EarnPoints();
        }
    }

    protected virtual void EarnPoints()
    {
        FindObjectOfType<GameManager>().CoinCatched(this);
    }
}
