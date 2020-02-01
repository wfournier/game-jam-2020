using System;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class HurtPlayer : MonoBehaviour
    {
        #region Declarations --------------------------------------------------

        private LevelManager _levelManager;
        public int damage;

        #endregion


        #region Private/Protected Methods -------------------------------------

        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void Update()
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_levelManager.player.invulnerable) return;

                _levelManager.RemoveHealth(damage);

                var playerVelocity = _levelManager.player.GetComponent<Rigidbody2D>().velocity;
                var directionX = Math.Sign(playerVelocity.x);
                var directionY = Math.Sign(playerVelocity.y);

                _levelManager.player.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(-playerVelocity.x - 10 * directionX, -playerVelocity.y - 10 * directionY), ForceMode2D.Impulse);
            }
        }

        #endregion
    }
}