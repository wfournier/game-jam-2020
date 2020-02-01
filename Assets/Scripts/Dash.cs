using Assets.Scripts.Managers;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }

    public class Dash : MonoBehaviour
    {
        public const float ForceModifier = 10.0f;
        public const KeyCode DashKeyCode = KeyCode.LeftShift;
        public const float DashCooldown = 2.0f;

        private LevelManager _levelManager = null;
        private float _currentTimeUntilReady = 0.0f;
        private DashState _currentDashState = DashState.Ready;

        private GameObject _dashEffect = null;

        void Start()
        {
            this._levelManager = FindObjectOfType<LevelManager>();
        }

        void Update()
        {
            if (this._currentDashState == DashState.Cooldown || this._currentDashState == DashState.Dashing)
            {
                return;
            }

            bool wasTriggerKeyPressed = Input.GetKeyDown(Dash.DashKeyCode);

            if (!wasTriggerKeyPressed)
            {
                return;
            }

            this._currentDashState = DashState.Dashing;
        }

        private void FixedUpdate()
        {
            // Current state is READY, return and wait for next input.
            if (this._currentDashState == DashState.Ready)
            {
                return;
            }

            // Current state is DASHING, trigger dash.
            else if (this._currentDashState == DashState.Dashing)
            {
                int playerDirectionX = Math.Sign(this._levelManager.player.rigidBody.velocity.x);

                if (playerDirectionX == 0 || playerDirectionX == 1)
                {
                    this._levelManager.player.rigidBody.AddForce(Vector2.right * Dash.ForceModifier, ForceMode2D.Impulse);
                }

                else
                {
                    this._levelManager.player.rigidBody.AddForce(Vector2.left * Dash.ForceModifier, ForceMode2D.Impulse);
                }

                this._currentTimeUntilReady = Dash.DashCooldown;
                this._currentDashState = DashState.Cooldown;
            }

            // Current state is COOLDOWN, decrease timer and reset state if needed.
            else
            {
                this._currentTimeUntilReady -= Time.fixedDeltaTime;

                if (this._currentTimeUntilReady <= 0.0f)
                {
                    this._currentTimeUntilReady = 0.0f;
                    this._currentDashState = DashState.Ready;
                }
            }
        }
    }
}
