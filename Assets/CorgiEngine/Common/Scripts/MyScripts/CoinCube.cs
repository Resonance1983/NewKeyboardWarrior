using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class CoinCube : MonoBehaviour
    {
        public GameObject coin;

        public void OnDestroy()
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }
    }
}

