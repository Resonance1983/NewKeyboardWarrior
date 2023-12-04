using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace KeyboardWarrior
{
    public class ClickToBegin : MonoBehaviour
    {
        public GameObject textObject;
        public bool clickToStart = true;
        private void Start()
        {
            StartCoroutine(BlinkText());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && clickToStart)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        IEnumerator BlinkText()
        {
            while (true)
            {
                Debug.Log("Blink");
                yield return new WaitForSeconds(1);
                textObject.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                textObject.SetActive(true);
            }
            
        }
    }
}
