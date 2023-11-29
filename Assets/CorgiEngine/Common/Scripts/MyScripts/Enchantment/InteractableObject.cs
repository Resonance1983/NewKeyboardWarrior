using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace KeyboardWarrior
{
    public class InteractableObject : MonoBehaviour
    {
        public GameObject idleObject;
        public GameObject upObject;
        public GameObject downObject;
        public GameObject leftObject;
        public GameObject rightObject;

        public EnchantType defaultType = EnchantType.idle;
        public EnchantType currenttype = EnchantType.idle;

        public EnchantableObject eo;
        public bool singleGO = false;
        public bool returnToDefault = true;
        public float enchantTimer = 0;
        private void Start()
        {
            currenttype = defaultType;
            eo = GetComponent<EnchantableObject>();
            enchantTimer = 0;
        }

        public void OnEnchant(EnchantType enchantType)
        {
            enchantTimer = 0;
            if (eo.enchanted)
            {
                eo.ReteriveEnchantment();
            }
            currenttype = enchantType;
            eo.OnEnchant(enchantType);
        }

        void AutoReterieve()
        {
            PlayerManager.Instance.skillManager.RetrieveEnchantment(currenttype);
            eo.BaseEvent();
            if (returnToDefault) currenttype = defaultType;
        }

        private void Update()
        {
            if (eo.enchanted)
            {
                if (enchantTimer < PlayerManager.Instance.skillManager.enchantLastTime)
                {
                    enchantTimer += Time.deltaTime;
                }
                else
                {
                    AutoReterieve();
                }    
            }
            eo.currentEnchant = currenttype;
            if (singleGO) return;
            idleObject.active = currenttype == EnchantType.idle ? true:false;
            upObject.active = currenttype == EnchantType.up ? true:false;
            downObject.active = currenttype == EnchantType.down ? true:false;
            leftObject.active = currenttype == EnchantType.left ? true:false;
            rightObject.active = currenttype == EnchantType.right ? true:false;
        }
    }
}
