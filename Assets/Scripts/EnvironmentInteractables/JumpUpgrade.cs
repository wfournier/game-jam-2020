using Assets.Scripts.Controllers;
using UnityEngine;

public class JumpUpgrade : MonoBehaviour
{
    private PlayerController _player;

    public GameObject toUnhide;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.canJump = true;
            _player.Jump(true);

            toUnhide.SetActive(true);
            Destroy(gameObject);
        }
    }
}