using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KeyboardWarrior
{
    public class ClickToBegin : MonoBehaviour
    {
        public GameObject textObject;

        private void Start()
        {
            StartCoroutine(BlinkText());
        }

        IEnumerator BlinkText()
        {
            while (!Input.GetMouseButtonDown(0))
            {
                Debug.Log("Blink");
                yield return new WaitForSeconds(1);
                textObject.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                textObject.SetActive(true);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
