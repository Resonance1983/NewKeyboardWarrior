using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class PlayerSkillManager : MonoBehaviour
    {
        public GameObject wUI;
        public GameObject aUI;
        public GameObject sUI;
        public GameObject dUI;

        PlayerKeyboardManager keyboardManager;

        private void Start()
        {
            keyboardManager = PlayerManager.Instance.keyboardManager;
            Dragable[] dragables = FindObjectsOfType<Dragable>();
            foreach (Dragable dragable in dragables)
            {
                switch (dragable.enchantmentDirection)
                {
                    case (EnchantType.up):
                        wUI = dragable.gameObject;
                        break;
                    case EnchantType.down:
                        sUI = dragable.gameObject;
                        break;
                    case EnchantType.left:
                        aUI = dragable.gameObject;
                        break;
                    case EnchantType.right:
                        dUI = dragable.gameObject;
                        break;
                }
            }
        }

        public float enchantLastTime = 5f;

        public void RetrieveEnchantment(EnchantType enchantType)
        {
            keyboardManager.UnuseKey(enchantType);
        }

        private void Update()
        {
            wUI.SetActive(keyboardManager.canUseW);
            aUI.SetActive(keyboardManager.canUseA);
            sUI.SetActive(keyboardManager.canUseS);
            dUI.SetActive(keyboardManager.canUseD);
        }
    }
}
