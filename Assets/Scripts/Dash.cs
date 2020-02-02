using Assets.Scripts.Managers;
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
        public const float DashDuration = 0.1f;
        public const float DashCooldown = 1.5f;
        private DashState _currentDashState = DashState.Ready;
        private float _currentDashTime;
        private float _currentTimeUntilReady;

        private LevelManager _levelManager;
        private Vector2 _previousVelocity;
        public ProgressBar DashCooldownProgressBar;

        public GameObject DashTrails;

        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();

            DashCooldownProgressBar.SetMinValue(0);
            DashCooldownProgressBar.SetMaxValue(DashCooldown);
        }

        private void Update()
        {
            if (_currentDashState == DashState.Cooldown || _currentDashState == DashState.Dashing ||
                !InputManager.DashButton) return;

            DashStart();
        }

        public void DashStart()
        {
            _currentDashState = DashState.Dashing;
        }

        private void LateUpdate()
        {
            if (_currentDashState == DashState.Ready)
                return;

            if (_currentDashState == DashState.Dashing)
                TriggerDash();

            else
                DecreaseCooldownTimer();
        }

        private void TriggerDash()
        {
            DashTrails.SetActive(true);
            _currentDashTime += Time.fixedDeltaTime;

            var playerDirection = _levelManager.player.transform.localScale.x;
            _levelManager.player.rigidBody.AddForce(Vector2.right * (ForceModifier * playerDirection),
                ForceMode2D.Impulse);

            if (_currentDashTime >= DashDuration)
            {
                DashTrails.SetActive(false);
                _levelManager.player.rigidBody.velocity = _previousVelocity;

                _currentDashTime = 0.0f;
                _currentTimeUntilReady = DashCooldown;
                _currentDashState = DashState.Cooldown;
            }
        }

        private void DecreaseCooldownTimer()
        {
            _currentTimeUntilReady -= Time.fixedDeltaTime;
            DashCooldownProgressBar.Show();
            DashCooldownProgressBar.SetValue(DashCooldown - _currentTimeUntilReady);

            if (_currentTimeUntilReady <= 0.0f)
            {
                _currentTimeUntilReady = 0.0f;
                DashCooldownProgressBar.SetValue(0);
                DashCooldownProgressBar.Hide();
                _currentDashState = DashState.Ready;
            }
        }
    }
}