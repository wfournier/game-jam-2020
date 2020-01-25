using UnityEngine;

namespace Assets.Scripts
{
    public class InputManager : MonoBehaviour
    {
        public bool testAsMobileDevice;

        public enum HorizontalDirections
        {
            Left,
            Right,
            Idle
        }
        public enum VerticalDirections
        {
            Up,
            Down,
            Idle
        }

        public static HorizontalDirections HorizontalDir { get; set; }
        public static VerticalDirections VerticalDir { get; set; }
        public static bool JumpButton { get; set; }


        // Start is called before the first frame update
        void Start()
        {
            HorizontalDir = HorizontalDirections.Idle;
            VerticalDir = VerticalDirections.Idle;
            JumpButton = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || testAsMobileDevice) return;

            SetHorizontalDirection();
            SetVerticalDirection();
            JumpButton = Input.GetButtonDown("Jump");
        }

        public void Jump(bool jumping)
        {
            JumpButton = jumping;
        }

        private void SetHorizontalDirection()
        {
            if (Input.GetAxisRaw("Horizontal") < 0f)
                HorizontalDir = HorizontalDirections.Left;
            else if (Input.GetAxisRaw("Horizontal") > 0f)
                HorizontalDir = HorizontalDirections.Right;
            else
                HorizontalDir = HorizontalDirections.Idle;
        }

        private void SetVerticalDirection()
        {
            if (Input.GetAxisRaw("Vertical") < 0f)
                VerticalDir = VerticalDirections.Up;
            else if (Input.GetAxisRaw("Vertical") > 0f)
                VerticalDir = VerticalDirections.Down;
            else
                VerticalDir = VerticalDirections.Idle;
        }
    }
}
