using KeyboardWarrior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class BeanCounter : Singleton<BeanCounter>
    {
        [Tooltip("current bean")]
        public int counter;
        [Tooltip("target number of beans")]
        public int targetCount = 26;

        public bool enoughBean = false;
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
            }
        }
    }
}

