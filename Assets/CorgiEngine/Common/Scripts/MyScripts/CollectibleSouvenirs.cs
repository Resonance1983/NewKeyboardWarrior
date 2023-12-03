using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class CollectibleSouvenirs : MonoBehaviour
    {
        public float rotateSpeed = 30f;
        public AudioSource audioSource;
        private GameObject coinSprite;

        void Start(){
            coinSprite = transform.Find("Sprite").gameObject;
        }
        

        void Update(){
            this.transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime, Space.Self);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == PlayerManager.Instance.gameObject && coinSprite.active == true)
            {
                audioSource.Play(0);
                if (CoinManager.Instance)
                {
                    CoinManager.Instance.GetCoin();
                }
                coinSprite.active = false;
            }
        }

    }
}
