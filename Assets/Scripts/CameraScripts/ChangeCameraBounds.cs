using System;
using Assets.Scripts.Controllers;
using UnityEngine;

public class ChangeCameraBounds : MonoBehaviour
{

    private CameraController activeCamera;
    public Collider2D leftBound;
    public Collider2D rightBound;
    
    void Start()
    {
        activeCamera = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        
    }

    private void ChangeBounds(int direction)
    {
        activeCamera.cameraBounds = direction < 0 ? leftBound : rightBound;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeBounds(Math.Sign(other.transform.localScale.x));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeBounds(Math.Sign(other.transform.localScale.x));
        }
    }
}
