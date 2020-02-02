using Assets.Scripts.Managers;
using UnityEngine;

public class SmashPlatform : MonoBehaviour
{
    private float actualWaitTime;
    private PlatformEffector2D effector;
    private Collider2D playerCollision;

    public float waitTime = 0.3f;

    // Start is called before the first frame update
    private void Start()
    {
        playerCollision = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        effector = GetComponent<PlatformEffector2D>();
        actualWaitTime = waitTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (InputManager.VerticalDir == InputManager.VerticalDirections.Down &&
            playerCollision.IsTouching(gameObject.GetComponent<Collider2D>()))
        {
            if (actualWaitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                actualWaitTime = waitTime;
            }
            else
            {
                actualWaitTime -= Time.deltaTime;
            }
        }

        if (InputManager.JumpButton) effector.rotationalOffset = 0;
    }
}