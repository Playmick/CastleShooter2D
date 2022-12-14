using UnityEngine;
using System;
using System.Collections;
using Architecture.Scenes;
using Architecture;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        Game.Run();
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

    }


    private void Update()
    {
        /*
         * код из курса, чисто для примера
        if (!Bank.isInitialized)
            return;
        if(Input.GetKeyDown(KeyCode.A))
        {
            Bank.AddCoins(this, 5);
            Debug.Log($"Coins added (5), {Bank.coins}");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Bank.Spend(this, 10);
            Debug.Log($"Coins spended (10), {Bank.coins}");
        }*/
    }
}
