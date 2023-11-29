using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class PlayerKeyboardManager : MonoBehaviour 
    {
        public bool canUseW = true;
        public bool canUseA = true;
        public bool canUseS = true;
        public bool canUseD = true;

        public void UseKey(EnchantType eType)
        {
            switch (eType)
            {
                case EnchantType.up:
                    canUseW = false;
                    break;
                case EnchantType.down:
                    canUseS = false;
                    break;
                case EnchantType.left:
                    canUseA = false;
                    break;
                case EnchantType.right:
                    canUseD = false;
                    break;
            }
        }

        public void UnuseKey(EnchantType eType)
        {
            switch (eType)
            {
                case EnchantType.up:
                    canUseW = true;
                    break;
                case EnchantType.down:
                    canUseS = true;
                    break;
                case EnchantType.left:
                    canUseA = true;
                    break;
                case EnchantType.right:
                    canUseD = true;
                    break;
            }
        }
    }
}
