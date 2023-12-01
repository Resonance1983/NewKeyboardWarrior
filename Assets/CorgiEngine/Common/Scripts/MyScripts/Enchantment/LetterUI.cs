using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KeyboardWarrior
{
    public class LetterUI : MonoBehaviour
    {
        Word word;
        Text text;
        private void Start()
        {
            word = GetComponentInParent<Word>();
            text = GetComponent<Text>();
            text.text = word.letter;
        }
    }
}
