using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class LetterGenerator : MonoBehaviour
    {
        public Word letterPrefab;
        public Word playerLetter;
        public List<string> letters;
        public int letterIndex;
        public bool levelEnd = false;
        public float generateInterval = 3f;
        private void Start()
        {
            StartCoroutine(WordGeneration());
        }
        public void GenerateWorld()
        {
            Word newLetter = new Word();
            if (letterIndex < letters.Count-1) 
            { 
                newLetter = Instantiate(letterPrefab, transform.position, Quaternion.identity); 
            }
            else if (letterIndex == letters.Count-1)
            {
                newLetter = Instantiate(playerLetter, transform.position, Quaternion.identity);
                BroadcastMessage("Rickroll");
            }
            newLetter.letter = letters[letterIndex];
        }

        public void WordMatched()
        {
            if (letterIndex < letters.Count - 1)
            {
                letterIndex++;
            }
            else
            {
                levelEnd = true;
            }
        }

        IEnumerator WordGeneration()
        {
            while (!levelEnd)
            {
                GenerateWorld() ;
                yield return new WaitForSeconds(generateInterval);
            }
        }
    }
}
