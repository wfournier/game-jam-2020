using Assets.Scripts.Managers;
using UnityEngine;

public class UnlockWallJump : MonoBehaviour
{
    private LevelManager _levelManager;

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _levelManager.player.wallJumpUnlocked = true;
            Destroy(gameObject);
        }
    }
}