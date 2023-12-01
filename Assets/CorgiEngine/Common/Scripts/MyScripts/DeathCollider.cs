using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KeyboardWarrior
{
    public class DeathCollider : MonoBehaviour
    {
        public bool realDeathCollider = false;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (realDeathCollider && collision.gameObject == PlayerManager.Instance.gameObject)
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
