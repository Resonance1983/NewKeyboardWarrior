using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class EnchantTrigger : EnchantableObject
    {
        public override void OnEnchant(EnchantType enchantType)
        {
            enchanted = true;
            currentEnchant = enchantType;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<InteractableObject>() != null)
            {
                other.transform.position = transform.position;
                InteractableObject io = other.GetComponent<InteractableObject>();
                io.OnEnchant(currentEnchant);
            }
        }
    }
}

