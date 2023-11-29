using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

namespace KeyboardWarrior
{
    public class PlayerEnchantment : EnchantableObject
    {
        PlayerDash playerDash;
        private void Start()
        {
            playerDash = GetComponent<PlayerDash>();
        }
        public override void OnEnchant(EnchantType enchantType)
        {
            base.OnEnchant(enchantType);
        }

        public override void UpEvent()
        {
            base.UpEvent();
            playerDash.enchantType = EnchantType.up;
            playerDash.InitiateDash();
        }

        public override void DownEvent()
        {
            base.DownEvent();
            playerDash.enchantType = EnchantType.down;
            playerDash.InitiateDash();
        }

        public override void LeftEvent()
        {
            base.LeftEvent();
            playerDash.enchantType = EnchantType.left;
            playerDash.InitiateDash();
        }

        public override void RightEvent()
        {
            base.RightEvent();
            playerDash.enchantType = EnchantType.right;
            playerDash.InitiateDash();
        }

        public override void BaseEvent()
        {
            base.BaseEvent();
        }
    }
}

