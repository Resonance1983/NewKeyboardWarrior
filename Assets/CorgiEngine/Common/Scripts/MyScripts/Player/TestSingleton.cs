using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class TestSingleton : Singleton<TestSingleton>
{
        public float testVar;
    }
}

