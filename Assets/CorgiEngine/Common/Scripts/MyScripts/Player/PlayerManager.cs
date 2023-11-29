using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public PlayerKeyboardManager keyboardManager;
        public PlayerSkillManager skillManager;
        public PlayerDragManager dragManager;

        protected override void Awake()
        {
            base.Awake();
            keyboardManager = GetComponent<PlayerKeyboardManager>();
            skillManager = GetComponent<PlayerSkillManager>();
            dragManager = GetComponent<PlayerDragManager>();
        }
    }
}
