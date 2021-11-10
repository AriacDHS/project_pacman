using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCoin : Coin
{
    public float duration = 8.0f;

    protected override void EarnPoints()
    {
        FindObjectOfType<GameManager>().PowerUpCoinCatched(this);
    }
}
