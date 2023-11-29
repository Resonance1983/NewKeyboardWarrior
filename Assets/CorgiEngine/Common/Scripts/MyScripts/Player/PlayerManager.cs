using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class PlayerManager : MonoBehaviour
    {
        public PlayerKeyboardManager keyboardManager;

        private void Awake()
        {
            keyboardManager = GetComponent<PlayerKeyboardManager>();
        }
    }
}
