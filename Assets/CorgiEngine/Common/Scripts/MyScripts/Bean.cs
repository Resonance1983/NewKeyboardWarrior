using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider2D))]
    public class Bean : MonoBehaviour
    {
        public AudioClip audioOnCollect;
        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.ToLower() == "player")
            {
                if (BeanCounter.Instance)
                {
                    BeanCounter.Instance.GetBean();
                }
                if (audioOnCollect) GetComponent<AudioSource>().PlayOneShot(audioOnCollect);
                gameObject.SetActive(false);
            }
        }
    }
}

