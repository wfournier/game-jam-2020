using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class ActivateAnimationAndSound : MonoBehaviour
    {
        #region Declarations --------------------------------------------------

        public GameObject backgrounds;
        public GameObject effect;
        public AudioSource backgroundMusic;

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

                var effectPosition =
                    new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                _levelManager.PlayEffect(effect, effectPosition, effect.transform.rotation);

                backgroundMusic.Play();
                Destroy(gameObject);
            }
        }

        #endregion
    }
}