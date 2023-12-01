using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KeyboardWarrior
{
    public class GameManager : Singleton<GameManager>
    {
        float restartTimer = 0;
        public float restartPressTime = 3f;

        private void Update()
        {
            if (restartTimer >= restartPressTime)
            {
                ReloadScene();
            }

            if (Input.GetKey(KeyCode.R))
            {
                restartTimer += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                restartTimer = 0;
            }
        }
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

