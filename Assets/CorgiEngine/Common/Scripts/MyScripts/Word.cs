using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class Word : MonoBehaviour
    {
        [SerializeField] WordManager wordManager;
        public string letter;
        public bool canDestroy = true;
        private void Start()
        {
            wordManager = FindObjectOfType<WordManager>();
            letter = letter.ToUpper();
        }
        private void Update()
        {
            if (!wordManager)
            {
                wordManager = FindAnyObjectByType<WordManager>();
                return;
            }
            HorizontalDetection();
            VerticalDetection();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + 100, transform.position.y + 0));
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + 0, transform.position.y - 100));
        }

        void HorizontalDetection()
        {
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, transform.right, 100.0F);
            List<GameObject> gos = new List<GameObject>();
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.GetComponent<Word>())
                    gos.Add(hit.collider.gameObject);
            }
            string finalStr = "";
            foreach (GameObject go in gos)
            {
                finalStr += go.GetComponent<Word>().letter;
            }

            wordManager.CheckString(finalStr, gos);
        }

        void VerticalDetection()
        {
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, -transform.up, 100.0F);
            List<GameObject> gos = new List<GameObject>();
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.GetComponent<Word>())
                    gos.Add(hit.collider.gameObject);
            }
            string finalStr = "";
            foreach (GameObject go in gos)
            {
                finalStr += go.GetComponent<Word>().letter;
            }

            wordManager.CheckString(finalStr, gos);
        }
    }
}
