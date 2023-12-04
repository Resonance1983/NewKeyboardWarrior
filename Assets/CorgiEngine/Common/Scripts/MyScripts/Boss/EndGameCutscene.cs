using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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

        [Header("Cutscene Related Waypoints")]
        public Transform centerPoint;
        public Transform keyboardStart;
        public Transform keyboardEnd;

        [Header("PlayerUI")]
        public GameObject WASD;
        public GameObject WASD_New;
        public GameObject tbcObject;

        public PlayableDirector director;

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
            boss.SetActive(false);
            yield return new WaitForSeconds(1f);
            // a new KK appears in the center
            newKK.SetActive(true);
            newKK.transform.position = centerPoint.position;
            yield return new WaitForSeconds(1f);
            // lasers appear around the scene
            foreach (GameObject go in lasers)
            {
                go.SetActive(true);
                yield return new WaitForSeconds(1f);
            }
            // lasers are ready to fire
            foreach (ParticleSystem ps in laserStartEffects)
            {
                ps.Play();
            }
            yield return new WaitForSeconds(2f);
            // A keyboard appears and start moving to the center
            GameObject keyboardObj = CoinManager.Instance.enoughCoinForGoldKeyboard ? goldKeyboard : whiteKeyboard;
            keyboardObj.SetActive(true);
            keyboardObj.transform.position = keyboardStart.position;
            keyboardObj.GetComponent<Rigidbody2D>().velocity = Vector2.up * 1f;
            // keyboard accends
            while (keyboardObj.transform.position.y < keyboardEnd.position.y)
            {
                yield return null;
            }
            keyboardObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            yield return new WaitForSeconds(1f);
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
        }
    }
}

