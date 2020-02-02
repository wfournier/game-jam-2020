using Assets.Scripts.Managers;
using UnityEngine;

public class ActivateHealth : MonoBehaviour
{
    #region Declarations --------------------------------------------------
    private LevelManager _levelManager;
    //private GameObject _player;
    #endregion


    #region Private/Protected Methods -------------------------------------

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        //_player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _levelManager.healthBarEnabled = true;
            Destroy(gameObject);
        }
    }

    #endregion
}
