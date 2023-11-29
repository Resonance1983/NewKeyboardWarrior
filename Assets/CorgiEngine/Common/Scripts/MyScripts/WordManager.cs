using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class WordManager : MonoBehaviour
    {
        [System.Serializable]
        public struct WordAndSound
        {
            public string word;
            public AudioClip audio;
        }

        AudioSource audioSource;
        AudioClip currentClip;
        public List<WordAndSound> words = new List<WordAndSound>();
        public float destroyDelay = 1.0f;
        public AudioClip rickroll;
        bool rickrolled = false;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public bool CheckString(string str, List<GameObject> objs)
        {
            for (int i = 1; i <= str.Length; i++)
            {
                string newS = str[..i];
                foreach (WordAndSound ws in words)
                {
                    ws.word.ToUpper();
                    if (ws.word == newS)
                    {
                        BroadcastMessage("WordMatched");
                        if (ws.audio)
                        {
                            currentClip = ws.audio;
                            audioSource.PlayOneShot(currentClip);
                        }
                        words.Remove(ws);
                        StartCoroutine(DestroyWord(objs, i));
                        return true;
                    }
                }
            }

            return false;
        }

        IEnumerator DestroyWord(List<GameObject> objs, int i)
        {
            yield return new WaitForSeconds(destroyDelay);
            for (int j = 0; j < i; j++)
            {
                Destroy(objs[j]);
            }
        }

        private void Update()
        {
        }

        public void Rickroll()
        {
            if (rickrolled) return;
            audioSource.Stop();
            audioSource.PlayOneShot(rickroll);
            rickrolled = true;
        }
    }
}
