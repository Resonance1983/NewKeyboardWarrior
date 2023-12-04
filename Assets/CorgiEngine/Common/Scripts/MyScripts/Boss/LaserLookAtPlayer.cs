using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace KeyboardWarrior
{
    public class LaserLookAtPlayer : MonoBehaviour
    {
        Transform playerTransform;

        private void Start()
        {
            playerTransform = PlayerManager.Instance.transform;
        }

        private void Update()
        {
            transform.right = playerTransform.position - transform.position;
        }
    }
}

