using KeyboardWarrior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class BeanCounter : Singleton<BeanCounter>
    {
        [Tooltip("current bean")]
        [SerializeField]
        public int counter;
        [Tooltip("target number of beans")]
        public int targetCount = 26;

        private bool enoughBean = false;
        public LevelEndPortal endLevelPortal;
        protected override void Awake()
        {
            base.Awake();
        }

        public void GetBean()
        {
            counter++;
            if (counter >= targetCount)
            {
                enoughBean = true;
                endLevelPortal.gameObject.SetActive(true);
            }
        }
    }
}

