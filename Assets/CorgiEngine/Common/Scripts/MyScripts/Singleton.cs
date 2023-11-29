using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T instance;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
