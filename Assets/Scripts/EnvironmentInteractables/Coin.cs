using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.EnvironmentInteractables
{
    public class Coin : MonoBehaviour
    {
        #region Declarations --------------------------------------------------

        private LevelManager _levelManager;

        public int coinValue = 1;

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
                if (!_levelManager.coinsEnabled)
                    _levelManager.coinsEnabled = true;

                _levelManager.AddCoins(coinValue);
                Destroy(gameObject);
            }
        }

        #endregion
    }
}