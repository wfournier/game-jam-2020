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

    }
}
