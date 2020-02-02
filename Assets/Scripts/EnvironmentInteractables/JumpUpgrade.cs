using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controllers;
using TMPro;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class JumpUpgrade : MonoBehaviour
{

    private PlayerController _player;

    public GameObject toUnhide;
    
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    void Update()
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
