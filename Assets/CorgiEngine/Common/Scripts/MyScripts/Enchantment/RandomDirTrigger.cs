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
                InteractableObject io = trigger.GetComponent<InteractableObject>();
                io.returnToDefault = false;
                switch (rand)
                {
                    case 0:
                        io.OnEnchant(EnchantType.up);
                        break;
                    case 1:
                        io.OnEnchant(EnchantType.down);
                        break;
                    case 2:
                        io.OnEnchant(EnchantType.left);
                        break;
                    case 3:
                        io.OnEnchant(EnchantType.right);
                        break;
                }
            }
        }
    }
}

