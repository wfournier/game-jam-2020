using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Dash : MonoBehaviour
    {
        public const KeyCode DashKeyCode = KeyCode.LeftShift;
        public const float ForceModifier = 150.0f;

        private LevelManager _levelManager = null;
        private bool _wasTriggerKeyPressed = false;

        void Start()
        {
            this._levelManager = FindObjectOfType<LevelManager>();
        }

        void Update()
        {
            this._wasTriggerKeyPressed = Input.GetKeyDown(Dash.DashKeyCode);
        }

        private void FixedUpdate()
        {
            if (!this._wasTriggerKeyPressed)
            {
                return;
            }

            this._isDashing = true;
            Vector2 previousVelocity = this._levelManager.Player.rigidBody.velocity;
            int playerDirectionX = Math.Sign(previousVelocity.x);

            if (playerDirectionX == 0 || playerDirectionX == 1)
            {
                this._levelManager.Player.rigidBody.AddForce(Vector2.right * Dash.ForceModifier, ForceMode2D.Impulse);
            }

            else
            {
                this._levelManager.Player.rigidBody.AddForce(Vector2.left * Dash.ForceModifier, ForceMode2D.Impulse); ;
            }
        }
    }
}
