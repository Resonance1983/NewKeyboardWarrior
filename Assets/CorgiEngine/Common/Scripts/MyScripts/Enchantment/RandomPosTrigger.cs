using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class RandomPosTrigger : MonoBehaviour
    {
        public List<EnchantTrigger> enchantTriggers;

        public void RandomPos()
        {
            int rangeMax = enchantTriggers.Count;
            int rand1 = Random.Range(0, rangeMax);
            int rand2 = Random.Range(0, rangeMax);
            while (rand2 == rand1)
            {
                rand2 = Random.Range(0, rangeMax);
            }

            for (int i = 0; i < rangeMax; i++)
            {
                if (i == rand1 || i == rand2)
                {
                    enchantTriggers[i].gameObject.SetActive(true);
                }
                else
                {
                    enchantTriggers[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

