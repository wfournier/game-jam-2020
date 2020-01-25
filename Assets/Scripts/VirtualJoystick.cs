using Assets.Scripts;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour
{
    private bool _touchStart;
    private Vector2 _pointA;
    private Vector2 _currentPoint;
    private Vector2 _pointB;
    private bool _isOnRightSide = false;

    public Transform circle;
    public Transform outerCircle;

    // Update is called once per frame
    void Update()
    {
        //Making sure interaction happens on the right side of the screen
        var screenMiddle = Screen.width / 2;
        if (!(Input.mousePosition.x < screenMiddle))
        {
            if (!Input.GetMouseButton(0))
            {
                circle.GetComponent<SpriteRenderer>().enabled = false;
                outerCircle.GetComponent<SpriteRenderer>().enabled = false;
                InputManager.HorizontalDir = InputManager.HorizontalDirections.Idle;
                InputManager.VerticalDir = InputManager.VerticalDirections.Idle;
            }

            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            _pointA = (new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            outerCircle.transform.localPosition = _pointA;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
            circle.transform.localPosition = _pointA;
            circle.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (Input.GetMouseButton(0))
        {
            _touchStart = true;
            _pointB = (new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        }
        else
        {
            _touchStart = false;
        }
    }
    private void FixedUpdate()
    {
        //resetting input
        InputManager.HorizontalDir = InputManager.HorizontalDirections.Idle;
        InputManager.VerticalDir = InputManager.VerticalDirections.Idle;

        var direction = new Vector2();

        if (_touchStart)
        {
            var offset = _pointB - _pointA;
            direction = Vector2.ClampMagnitude(offset, 25.0f);

            circle.transform.localPosition = new Vector2(_pointA.x + (direction.x ), _pointA.y + (direction.y ));
        }
        else
        {
            circle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }


        //Horizontal Input
        if (direction.x > 20f)
        {
            InputManager.HorizontalDir = InputManager.HorizontalDirections.Right;
        }
        if (direction.x < -20f)
        {
            InputManager.HorizontalDir = InputManager.HorizontalDirections.Left;
        }

        //Vertical Input
        if (direction.y > 0.9f)
        {
            InputManager.VerticalDir = InputManager.VerticalDirections.Up;
        }
        if (direction.y < -0.9f)
        {
            InputManager.VerticalDir = InputManager.VerticalDirections.Down;
        }
    }
}
