using Assets.Scripts.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

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

        public GameObject DashTrails;
        public ProgressBar DashCooldownProgressBar; 

        private LevelManager _levelManager = null;
        private Vector2 _previousVelocity;
        private float _currentDashTime = 0.0f;
        private float _currentTimeUntilReady = 0.0f;
        private DashState _currentDashState = DashState.Ready;


        void Start()
        {
            this._levelManager = FindObjectOfType<LevelManager>();

            this.DashCooldownProgressBar.SetMinValue(0);
            this.DashCooldownProgressBar.SetMaxValue(Dash.DashCooldown);
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
                this.TriggerDash();
            }

            // Current state is COOLDOWN, decrease timer and reset state if needed.
            else
            {
                this.DecreaseCooldownTimer();
            }
        }

        private void TriggerDash()
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

        private void DecreaseCooldownTimer()
        {
            this._currentTimeUntilReady -= Time.fixedDeltaTime;
            this.DashCooldownProgressBar.Show();
            this.DashCooldownProgressBar.SetValue(Dash.DashCooldown - this._currentTimeUntilReady);

            if (this._currentTimeUntilReady <= 0.0f)
            {
                this._currentTimeUntilReady = 0.0f;
                this.DashCooldownProgressBar.SetValue(0);
                this.DashCooldownProgressBar.Hide();
                this._currentDashState = DashState.Ready;
            }
        }
    }
}
