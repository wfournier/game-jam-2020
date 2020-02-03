using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Platforms
{
    public class LaunchPad : MonoBehaviour
    {
        private LevelManager _levelManager;
        public float jumpMagnitude = 20;
        public AudioClip jumpSound;
    }
}