using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class LetterGenerator : MonoBehaviour
    {
        public LetterCube letterPrefab;
        public LetterCube coinCubePrefab;
        public Transform startPos;
        public List<string> letters;
        public int letterIndex;
        public bool levelEnd = false;
        public float generateInterval = 3f;
        bool generatedCoinCube = false;

        public void GenerateLetter()
        {

            LetterCube newLetter = new LetterCube();
            if (letterIndex < letters.Count) 
            {
                if (!generatedCoinCube && CoinCube())
                {
                    generatedCoinCube = true;
                    newLetter = Instantiate(coinCubePrefab, startPos.position, Quaternion.identity);
                }
                else
                {
                    newLetter = Instantiate(letterPrefab, startPos.position, Quaternion.identity);
                    newLetter.GetComponent<Word>().letter = letters[letterIndex];
                }
            }
        }

        public bool CoinCube()
        {
            float rand = Random.Range(0, 100f);
            if (rand < 30)
            {
                return true;
            }
            return false;
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
    }
}
