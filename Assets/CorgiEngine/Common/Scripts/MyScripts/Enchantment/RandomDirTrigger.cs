using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class RandomDirTrigger : MonoBehaviour
    {
        public List<EnchantTrigger> triggers;

        public void RandomDir()
        {
            foreach (EnchantTrigger trigger in triggers)
            {
                int rand = Random.Range(0, 4);
                switch (rand)
                {
                    case 0:
                        trigger.currentEnchant = EnchantType.up;
                        break;
                    case 1:
                        trigger.currentEnchant = EnchantType.down;
                        break;
                    case 2:
                        trigger.currentEnchant = EnchantType.left;
                        break;
                    case 3:
                        trigger.currentEnchant = EnchantType.right;
                        break;
                }
            }
        }
    }
}

