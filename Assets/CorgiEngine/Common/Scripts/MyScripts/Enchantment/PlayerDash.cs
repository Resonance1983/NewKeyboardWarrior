using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyboardWarrior;

namespace MoreMountains.CorgiEngine
{
    public class PlayerDash : CharacterDash
    {
        public EnchantType enchantType = EnchantType.idle;

        protected override void ComputeDashDirection()
        {
            switch (enchantType)
            {
                case EnchantType.idle:
                    break;
                case EnchantType.up:
                    _dashDirection = Vector2.up;
                    break;
                case EnchantType.down:
                    _dashDirection = Vector2.down;
                    break;
                case EnchantType.left: 
                    _dashDirection = Vector2.left;
                    break;
                case EnchantType.right:
                    _dashDirection = Vector2.right;
                    break;
            }
            CheckAutoCorrectTrajectory();

        }
    }
}

