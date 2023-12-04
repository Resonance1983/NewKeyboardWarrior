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
        [Header("Boss Sub Objects")]
        public GameObject typeWriter;
        public GameObject laserGenerator;

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
            typeWriter.SetActive(true);
            laserGenerator.SetActive(true);
            currentState = BossStates.Letter;
        }

        private void Update()
        {
            if (inLaserProcess || inLetterProcess || inDeathProcess)
            {
                return;
            }
            if (bossHealth <= 0)
            {
                currentState = BossStates.Death;
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
            for (int i = 0; i < 3 ; i++)
            {
                letterGenetated++;
                letterGenerator.GenerateLetter();
                yield return new WaitForSeconds(letterPhaseTime/3);
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
            yield return null;
            FindObjectOfType<EndGameCutscene>().StartCutscene();
        }

        void RandomTriggerDirection()
        {
            foreach (EnchantTrigger trigger in enchantTriggers)
            {
                trigger.RandomDirection();
            }
        }

        public void OnWordForm()
        {
            bossHealth--;
        }
    }
}

