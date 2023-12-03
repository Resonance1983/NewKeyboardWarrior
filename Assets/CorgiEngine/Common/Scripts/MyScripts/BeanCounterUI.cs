using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KeyboardWarrior
{
    public class BeanCounterUI : MonoBehaviour
    {
        Text text;

        private void Start()
        {
            text = GetComponent<Text>();
        }

        private void Update()
        {
            text.text = BeanCounter.Instance.counter.ToString();
        }
    }

}
