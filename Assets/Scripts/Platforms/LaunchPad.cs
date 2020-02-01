using Assets.Scripts.Controllers;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Platforms
{
    public class LaunchPad : MonoBehaviour
    {
		public float jumpMagnitude = 20;
        public AudioClip jumpSound;
        private LevelManager _levelManager;

        void Start()
        {
            this._levelManager = FindObjectOfType<LevelManager>();
        }

        void Update()
        {
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Vector2 contactPoint = other.GetContact(0).normal;

            if (contactPoint == Vector2.left || contactPoint == Vector2.right)
            {
                return;
            }

             this._levelManager.player.Jump(true, this.jumpMagnitude * 2);

            if (jumpSound != null)
            {
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            }
        }
    }
}
