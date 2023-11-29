using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyboardWarrior
{
    public class MovingPlatform : EnchantableObject
    {
        Rigidbody2D rb;
        public float moveSpeed;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public override void UpEvent()
        {
            base.UpEvent();
            currentEnchant = EnchantType.up;
        }
        public override void DownEvent()
        {
            base.DownEvent();
            currentEnchant = EnchantType.down;
        }
        public override void LeftEvent()
        {
            base.LeftEvent();
            currentEnchant = EnchantType.left;
        }
        public override void RightEvent()
        {
            base.RightEvent();
            currentEnchant = EnchantType.right;
        }
        public override void BaseEvent()
        {
            base.BaseEvent();
            currentEnchant = EnchantType.idle;
        }

        private void Update()
        {
            Vector2 velocity = Vector2.zero;
            Vector2 scale = Vector2.one;
            switch (currentEnchant)
            {
                case EnchantType.up:
                    velocity.y = 1;
                    break;
                case EnchantType.down:
                    velocity.y = -1;
                    break;
                case EnchantType.left:
                    velocity.x = -1;
                    break;
                case EnchantType.right:
                    velocity.x = 1;
                    break;
                default:
                    velocity = Vector2.zero;
                    break;
            }
            rb.velocity = velocity * moveSpeed;
            transform.localScale = scale;
        }
    }
}
