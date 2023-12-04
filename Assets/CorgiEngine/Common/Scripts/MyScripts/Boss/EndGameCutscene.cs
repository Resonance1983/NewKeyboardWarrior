using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace KeyboardWarrior
{
    public class EndGameCutscene : MonoBehaviour
    {
        bool cutsceneStarted = false;

        [Header("Cutscene Related Objects")]
        public GameObject boss;
        public GameObject newKK;
        public List<GameObject> lasers;
        public List<ParticleSystem> laserStartEffects;
        public GameObject whiteKeyboard;
        public GameObject goldKeyboard;
        public GameObject blackhole;
        public int blackKKLayerID;

        [Header("Cutscene Related Waypoints")]
        public Transform centerPoint;
        public Transform keyboardStart;
        public Transform keyboardEnd;

        [Header("PlayerUI")]
        public GameObject WASD;
        public GameObject WASD_New;
        public GameObject tbcObject;

        public PlayableDirector director;
        public AudioClip tbcClip;
        public AudioSource bgmAudioSource;

        public GameObject keyboardShineUI;
        public GameObject bossSpeechUI;
        public GameObject endgameUI;

        public bool debug = false;

        private void Start()
        {
            //Instantiate particle
            foreach (GameObject go in lasers)
            {
                ParticleSystem[] particleSystems = go.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in particleSystems)
                {
                    laserStartEffects.Add(particle);
                }
            }

            director = GetComponent<PlayableDirector>();

            if (debug)
            {
                StartCutscene();
            }
        }
        public void StartCutscene()
        {
            if (cutsceneStarted) return;
            cutsceneStarted = true;
            StartCoroutine(EndGameProcess());
        }

        IEnumerator EndGameProcess()
        {
            // boss fight ends
            InputManager.Instance.InputDetectionActive = false;
            endgameUI.SetActive(true);
            LetterCube[] letterCubes = FindObjectsOfType<LetterCube>();
            foreach (LetterCube letter in letterCubes)
            {
                Destroy(letter.gameObject);
            }
            EnchantTrigger[] triggers = FindObjectsOfType<EnchantTrigger>();
            foreach (EnchantTrigger trigger in triggers)
            {
                Destroy(trigger.gameObject);
            }

            bgmAudioSource.Stop();
            bgmAudioSource.loop = false;
            bgmAudioSource.clip = tbcClip;
            bgmAudioSource.time = 24f;
            bgmAudioSource.Play();
            
            boss.SetActive(false);
            yield return new WaitForSeconds(1f);
            // a new KK appears in the center
            newKK.SetActive(true);
            newKK.transform.position = centerPoint.position;
            bossSpeechUI.SetActive(true);
            yield return new WaitForSeconds(1f);
            // lasers appear around the scene
            keyboardShineUI.SetActive(true);
            foreach (GameObject go in lasers)
            {
                go.SetActive(true);
                yield return new WaitForSeconds(1f);
            }
            bossSpeechUI.SetActive(false);
            // lasers are ready to fire
            foreach (ParticleSystem ps in laserStartEffects)
            {
                ps.Play();
            }
            yield return new WaitForSeconds(2f);
            keyboardShineUI.SetActive(false);
            // A keyboard appears and start moving to the center
            GameObject keyboardObj = CoinManager.Instance && CoinManager.Instance.enoughCoinForGoldKeyboard ? goldKeyboard : whiteKeyboard;
            keyboardObj.SetActive(true);
            keyboardObj.transform.position = keyboardStart.position;
            keyboardObj.GetComponent<Rigidbody2D>().velocity = Vector2.up * 1f;
            // keyboard accends
            while (keyboardObj.transform.position.y < keyboardEnd.position.y)
            {
                yield return null;
            }
            Debug.Log(newKK.GetComponentInChildren<SpriteRenderer>().sortingLayerID);
            keyboardObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            yield return new WaitForSeconds(0.5f);
            //Blackhole appears
            blackhole.SetActive(true);
            // All the lasers move towards the keyboard
            foreach (GameObject go in lasers)
            {
                if ((go.transform.position - blackhole.transform.position).magnitude > 0.5f)
                {
                    go.GetComponent<Rigidbody2D>().velocity = (blackhole.transform.position - go.transform.position) * .5f;
                }
                else
                {
                    go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }

            yield return new WaitForSeconds(2);
            //Destroy all lasers
            foreach (GameObject go in lasers)
            {
                Destroy(go);
            }
            Destroy(newKK);
            tbcObject.SetActive(true);
            WASD_New.SetActive(true);
            WASD.SetActive(false);
            director.Play();
            yield return new WaitForSeconds(8f);
            float defaultVolume = bgmAudioSource.volume;
            for (float i = defaultVolume; i > 0; i -= Time.deltaTime * defaultVolume / 2f)
            {
                bgmAudioSource.volume = i;
                yield return null;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}

