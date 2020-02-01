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

        public bool inverseXDirection = false;
        #endregion


        #region Private/Protected Methods -------------------------------------

        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void Update()
        {
        }

//        private void OnCollisionEnter2D(Collision2D collision)
//        {
//            if (collision.otherCollider.CompareTag("Player"))
//            {
//                if (_levelManager.player.invulnerable) return;
//
//                _levelManager.RemoveHealth(damage);
//
//                var playerVelocity = _levelManager.player.GetComponent<Rigidbody2D>().velocity;
//                var directionX = Math.Sign(playerVelocity.x);
//                var directionY = Math.Sign(playerVelocity.y);
//                
//                var normalVector = collision.GetContact(0).normal;
//                var speedVector = Vector2.Reflect(playerVelocity, normalVector);
//                speedVector += 10 * speedVector.normalized;
//
//                _levelManager.player.GetComponent<Rigidbody2D>().AddForce(
//                    speedVector, ForceMode2D.Impulse);
//            }
//        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_levelManager.player.invulnerable) return;

                _levelManager.RemoveHealth(damage);

                var playerVelocity = _levelManager.player.GetComponent<Rigidbody2D>().velocity;
                var directionX = Math.Sign(playerVelocity.x);
                var directionY = Math.Sign(playerVelocity.y);
                if (playerVelocity.x == 0 && playerVelocity.y == 0)
                {
                    _levelManager.RemoveHealth(10*damage);
                    //directionX = -Math.Sign(_levelManager.player.transform.position.x - gameObject.transform.position.x);
                    //playerVelocity.x = -12*directionX;
                    //playerVelocity.x = 20;
                    //playerVelocity.x = 5 * directionX;
                    return;
                }
                    

                var tempPlayerVelocityX = -playerVelocity.x;

                if (inverseXDirection)
                    tempPlayerVelocityX *= -1;

                _levelManager.player.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(tempPlayerVelocityX - 10 * directionX, -playerVelocity.y - 10 * directionY), ForceMode2D.Impulse);
            }
        }

        #endregion
    }
}