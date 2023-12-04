using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KeyboardWarrior
{
    public class EndSceneEvent : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
        }

        public void Replay()
        {
            SceneManager.LoadScene(1);
        }
    }
}
