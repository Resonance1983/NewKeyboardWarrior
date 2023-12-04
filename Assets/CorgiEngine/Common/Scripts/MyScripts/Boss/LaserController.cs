using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class LaserController : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public Transform muzzle;

        public GameObject startVFX;
        public GameObject endVFX;

        public List<ParticleSystem> startParticles;
        public List<ParticleSystem> particleSystems;

        public GameObject laserTarget;
        public List<Transform> laserPosList;
        SpriteRenderer sprite;
        [SerializeField]GameObject currentHit;
        public LayerMask ignoreLayer;

        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            lineRenderer = GetComponentInChildren<LineRenderer>();
            if (startVFX && endVFX)
            {
                FillLists();
            }
            DisableLaser();
            RandomPosition();      
        }

        private void OnEnable()
        {
            DisableLaser();
        }

        private void Update()
        {
            laserTarget = PlayerManager.Instance.gameObject;
            if (sprite && laserTarget)
            {
                transform.right = laserTarget.transform.position - transform.position;
            }
            if (!lineRenderer.enabled) return;
            RaycastHit2D hit = Physics2D.Raycast(muzzle.position, laserTarget.transform.position - transform.position, 1000f, ~ignoreLayer);
            Vector2 start = muzzle.position;
            Vector2 end = start;
            
            if (hit.collider != null)
            {
                end = hit.point;
                currentHit = hit.collider.gameObject;
            }
            else
            {
                end.x += 1000;
                currentHit = null;
            }
            startVFX.transform.position = start;
            endVFX.transform.position = end;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }

        public void LaserReady()
        {
            foreach (ParticleSystem ps in startParticles)
            {
                ps.Play();
            }
        }
        void FillLists()
        {
            for(int i = 0; i < startVFX.transform.childCount; i++)
            {
                var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    startParticles.Add(ps);
                    particleSystems.Add(ps);
                }
            }

            for (int i = 0; i < endVFX.transform.childCount; i++)
            {
                var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    particleSystems.Add(ps);
                }
            }
        }

        public void EnableLaser()
        {
            lineRenderer.enabled = true;
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Play();
            }
        }

        public void DisableLaser()
        {
            if (currentHit != null)
            {
                if (currentHit.GetComponent<Word>())
                {
                    if (currentHit.GetComponent<Word>().canDestroy)
                        Destroy(currentHit);
                }
                if (currentHit.GetComponent<CoinCube>())
                {
                    currentHit.GetComponent<CoinCube>().OnDestroy();
                    Destroy(currentHit);
                }
                if (currentHit.GetComponent<Health>())
                {
                    Debug.Log("Hit Player");
                    currentHit.GetComponent<Health>().Damage(100f, this.gameObject, 0, 0, Vector3.zero);
                }
            }
            lineRenderer.enabled=false;
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Stop();
            } 
        }

        public void RandomPosition()
        {
            int rand = Random.Range(0, laserPosList.Count-1);

            transform.position = laserPosList[rand].position;
        }
    }
}
