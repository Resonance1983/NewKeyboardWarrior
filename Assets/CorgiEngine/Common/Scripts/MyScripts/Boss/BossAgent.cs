using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public enum BossStates
    {
        Intro,
        Letter,
        Laser,
        Death,
    }
    public class BossAgent : MonoBehaviour
    {
        public BossStates defaultState = BossStates.Intro;
        [SerializeField] BossStates currentState;
        bool inLetterProcess = false;
        bool inLaserProcess = false;
        bool inDeathProcess = false;

        public int bossHealth = 5;
        public GameObject introKK;
        public AudioSource bgmSource;
        public AudioClip rickrollClip;
        public AudioClip duelClip;
        public GameObject mechanicList;
        [Header("Boss Sub Objects")]
        public GameObject typeWriter;
        public GameObject laserGenerator;
        public GameObject playerDialogue;

        [Header("Boss Skill References")]
        [SerializeField] LaserController laserController;
        [SerializeField] LetterGenerator letterGenerator;

        [Header("Intro Phase")]
        public float introPhaseTime = 1f;

        [Header("Letter Phase")]
        public float letterPhaseTime = 5f;
        int letterGenetated = 0;
        public float minPosTime = 0.2f;

        RandomPosTrigger randomPos;
        RandomDirTrigger randomDir;

        [Header("Laser Phase")]
        public float laserPosTime = 1f;
        public float laserPrepTime = 5f;
        public float laserLastTime = 3f;
        public int letterGenerateEachRound = 2;

        [Header("Death Phase")]
        public float deathPhaseTime = 5f;

        [Header("Triggers")]
        public List<EnchantTrigger> enchantTriggers;

        private void Start()
        {
            currentState = defaultState;
            laserController = laserGenerator.GetComponent<LaserController>();
            letterGenerator = typeWriter.GetComponent<LetterGenerator>();
            randomPos = FindObjectOfType<RandomPosTrigger>();
            randomDir = FindObjectOfType<RandomDirTrigger>();
            letterGenetated = 0;
            StartCoroutine(IntroProcess());
        }

        IEnumerator IntroProcess()
        {
            typeWriter.SetActive(false);
            laserGenerator.SetActive(false);
            introKK.SetActive(true);
            while (introKK.activeSelf)
            {
                yield return null;
            }
            playerDialogue.SetActive(true);
            bgmSource.clip = rickrollClip;
            bgmSource.Play();
            while (playerDialogue.activeSelf)
            {
                yield return null;
            }
            mechanicList.SetActive(true);
            float defaultVolume = bgmSource.volume;
            for (float i = defaultVolume; i > 0; i -= Time.deltaTime * defaultVolume / 2f)
            {
                bgmSource.volume = i;
                yield return null;
            }
            bgmSource.Stop();
            bgmSource.clip = duelClip;
            bgmSource.Play();
            for (float i = 0; i <= defaultVolume; i += Time.deltaTime * defaultVolume / 2f)
            {
                bgmSource.volume = i;
                yield return null;
            } 
            typeWriter.SetActive(true);
            laserGenerator.SetActive(true);  
            randomDir = FindObjectOfType<RandomDirTrigger>();
            randomPos = FindObjectOfType<RandomPosTrigger>();
            currentState = BossStates.Letter;
        }

        private void Update()
        {
            if (bossHealth <= 0)
            {
                if (inDeathProcess) return;
                currentState = BossStates.Death;
                inDeathProcess = true;
                StopAllCoroutines();
                StartCoroutine(DeathProcess());
            }
            if (inLaserProcess || inLetterProcess || inDeathProcess)
            {
                return;
            }
            switch (currentState)
            {
                case BossStates.Intro:
                    return;
                case BossStates.Letter:
                    if (inLetterProcess) return;
                    inLetterProcess = true;
                    StartCoroutine(LetterProcess());
                    return;
                case BossStates.Laser:
                    if (inLaserProcess) return;
                    inLaserProcess = true;
                    StartCoroutine(LaserProcess());
                    return;
                case BossStates.Death:
                    if (inDeathProcess) return;
                    inDeathProcess = true;
                    StartCoroutine(DeathProcess());
                    return;
            }
        }

        IEnumerator LetterProcess()
        {
            randomDir.RandomDir();
            randomPos.RandomPos();
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < letterGenerateEachRound; i++)
            {
                letterGenetated++;
                letterGenerator.GenerateLetter();
                yield return new WaitForSeconds(letterPhaseTime/ letterGenerateEachRound);
            }     
            currentState = BossStates.Laser;
            inLetterProcess = false;
        }

        IEnumerator LaserProcess()
        {
            float tempPosTime = laserPosTime / letterGenetated;
            if (tempPosTime < minPosTime) tempPosTime = minPosTime;
            for (int i = 0 ; i < letterGenetated; i++)
            {
                laserController.RandomPosition();
                yield return new WaitForSeconds(tempPosTime);
            }
            laserController.LaserReady();
            yield return new WaitForSeconds(laserPrepTime);
            laserController.EnableLaser();
            yield return new WaitForSeconds(laserLastTime);
            laserController.DisableLaser();
            currentState = BossStates.Letter;
            inLaserProcess = false;
        }

        IEnumerator DeathProcess()
        {
            yield return new WaitForSeconds(1.5f);
            FindObjectOfType<EndGameCutscene>().StartCutscene();
        }

        public void OnWordForm()
        {
            bossHealth--;
            StartCoroutine(hitEffect());
        }

        IEnumerator hitEffect()
        {
            yield return null;
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(1f);
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
}

