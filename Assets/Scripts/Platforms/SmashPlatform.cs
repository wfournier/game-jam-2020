using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float actualWaitTime;
    public float waitTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        actualWaitTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (InputManager.VerticalDir == InputManager.VerticalDirections.Down)
        {
            
            if (actualWaitTime <= 0){
                effector.rotationalOffset = 180f;
                actualWaitTime = waitTime;
            } else {
                actualWaitTime -= Time.deltaTime;
            }
        }
        if (InputManager.JumpButton)
        {
            effector.rotationalOffset = 0;
        }

    }
}
