
    using System;
    using BayatGames.SaveGameFree;
    using UnityEngine;

    public class CoinManager : Singleton<CoinManager>
    {
        private const string COIN_KEY = "Coins";
        public float Coins { get; private set; }

        [SerializeField] private float coinTest;
        private void Start()
        {
            Coins = SaveGame.Load(COIN_KEY, coinTest);
        }

        public void AddCoins(float amount)
        {
            Coins += amount;
            SaveGame.Save(COIN_KEY, Coins);
        }

        public void RemoveCoins(float amount)
        {
            if (Coins >= amount)
            {
                Coins -= amount;
                SaveGame.Save(COIN_KEY,Coins);
            }
        }
    }
