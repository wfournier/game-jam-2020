using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Platforms
{
    public class LaunchPad : MonoBehaviour
    {
		public float jumpMagnitude = 20;
        public AudioClip jumpSound;

        private GameObject _player;

        void Start()
        {
            _player = GameObject.FindWithTag("Player");
        }

        void Update()
        {
            if (_player.GetComponent<BoxCollider2D>()
                .IsTouching(gameObject.GetComponent<BoxCollider2D>()))
            {
                _player.GetComponent<PlayerController>().Jump(true, jumpMagnitude);

                if (jumpSound != null)
                    AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            }
        }
	}
}
