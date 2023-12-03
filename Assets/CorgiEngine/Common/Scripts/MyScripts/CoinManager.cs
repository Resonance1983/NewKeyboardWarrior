using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KeyboardWarrior
{
    public class CoinManager : Singleton<CoinManager>
    {
        public int totalCoin = 5;
        private int currentCoin = 0;
        bool enoughCoinForGoldKeyboard = false;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        public void GetCoin()
        {
            currentCoin++;
            if (currentCoin >= totalCoin && !enoughCoinForGoldKeyboard)
            {
                enoughCoinForGoldKeyboard = true;
            }
        }
    }
}

