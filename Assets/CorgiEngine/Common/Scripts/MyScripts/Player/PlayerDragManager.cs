using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class PlayerDragManager : MonoBehaviour
    {
        public float placeRadius = 5f;
        public float returnRadius = 10f;

        public GameObject indicatorObject;
        public bool dragging = false;

        private void Update()
        {
            //indicatorObject.SetActive(dragging);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, placeRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, returnRadius);
        }
    }
}
