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
        public const float DashDuration = 0.1f;
        public const float DashCooldown = 1.5f;

        public GameObject DashTrails = null;

        private LevelManager _levelManager = null;
        private Vector2 _previousVelocity;
        private float _currentDashTime = 0.0f;
        private float _currentTimeUntilReady = 0.0f;
        private DashState _currentDashState = DashState.Ready;


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
                this.DashTrails.SetActive(true);
                this._currentDashTime += Time.fixedDeltaTime;

                float playerDirection = this._levelManager.player.transform.localScale.x;
                this._levelManager.player.rigidBody.AddForce(Vector2.right * (Dash.ForceModifier * playerDirection), ForceMode2D.Impulse);

                if (this._currentDashTime >= Dash.DashDuration)
                {
                    this.DashTrails.SetActive(false);
                    this._levelManager.player.rigidBody.velocity = this._previousVelocity;

                    this._currentDashTime = 0.0f;
                    this._currentTimeUntilReady = Dash.DashCooldown;
                    this._currentDashState = DashState.Cooldown;
                }
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
