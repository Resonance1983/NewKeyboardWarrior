using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace KeyboardWarrior
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider2D))]
    public class Bean : MonoBehaviour
    {
        // public AudioClip audioOnCollect;
        private bool GetBean = false;
        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == PlayerManager.Instance.gameObject)
            {
                if (BeanCounter.Instance)
                {
                    BeanCounter.Instance.GetBean();
                }
                GetComponent<AudioSource>().Play();
                if (!GetBean) GetBean = true;
                
            }
        }

        void Update(){
            if(GetBean)
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if(!GetComponent<AudioSource>().isPlaying && GetBean)
                gameObject.SetActive(false);
        }

    }
}

