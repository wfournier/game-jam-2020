using System;
using Assets.Scripts.CameraScripts;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class ActivateAnimationAndSound : MonoBehaviour
    {
        #region Declarations --------------------------------------------------
        public GameObject backgrounds;

        private LevelManager _levelManager;
        private GameObject _player;
        private GameObject _camera;
        #endregion


        #region Private/Protected Methods -------------------------------------

        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
            _player = GameObject.FindWithTag("Player");
            _camera = GameObject.FindWithTag("MainCamera");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _player.GetComponent<Animator>().enabled = true;
                backgrounds.SetActive(true);
                _levelManager.deathEffectEnabled = true;
                _levelManager.isSoundEnabled = true;
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
