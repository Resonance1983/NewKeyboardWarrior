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

        [Header("Laser Phase")]
        public float laserPosTime = 1f;
        public float laserPrepTime = 5f;
        public float laserLastTime = 3f;

        [Header("Death Phase")]
        public float deathPhaseTime = 5f;

        private void Start()
        {
            currentState = defaultState;
            laserController = laserGenerator.GetComponent<LaserController>();
            letterGenerator = typeWriter.GetComponent<LetterGenerator>();
            letterGenetated = 0;
            StartCoroutine(IntroProcess());
        }

        IEnumerator IntroProcess()
        {
            yield return new WaitForSeconds(introPhaseTime); //Do Something
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
            letterGenetated++;
            letterGenerator.GenerateLetter();
            yield return new WaitForSeconds(letterPhaseTime);
            currentState = BossStates.Laser;
            inLetterProcess = false;
        }

        IEnumerator LaserProcess()
        {
            for (int i = 0 ; i < letterGenetated; i++)
            {
                laserController.RandomPosition();
                yield return new WaitForSeconds(laserPosTime);
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
            yield return new WaitForSeconds(deathPhaseTime);
            Destroy(gameObject);
        }
    }
}

