using MoreMountains.Feedbacks;
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
        public static WordManager instance;
        AudioSource audioSource;
        AudioClip currentClip;
        public List<WordAndSound> words = new List<WordAndSound>();
        public float destroyDelay = 1.0f;
        public AudioClip rickroll;
        bool rickrolled = false;

        public List<MMFeedbacks> wordMatchFeedbacks;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public bool CheckString(string str, List<GameObject> objs)
        {
            for (int i = 1; i <= str.Length; i++)
            {
                string newS = str[..i];
                Debug.Log(newS);
                newS = newS.ToUpper();
                foreach (WordAndSound ws in words)
                {
                    ws.word.ToUpper();
                    if (ws.word == newS)
                    {
                        BroadcastMessage("WordMatched");
                        FindObjectOfType<BossAgent>().OnWordForm();
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
            foreach (MMFeedbacks fb in wordMatchFeedbacks)
            {
                fb.PlayFeedbacks();
            }
            for (int j = 0; j < i; j++)
            {
                Destroy(objs[j]);
            }
        }

        private void Update()
        {
        }
    }
}
