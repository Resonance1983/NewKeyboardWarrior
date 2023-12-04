using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.CorgiEngine;

namespace KeyboardWarrior
{
    public class DeathCollider : MonoBehaviour
    {
        public bool realDeathCollider = false;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Word>() && collision.gameObject.GetComponent<Word>().canDestroy)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
