using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
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

        public bool testAsMobileDevice;

        public static HorizontalDirections HorizontalDir { get; set; }
        public static VerticalDirections VerticalDir { get; set; }
        public static bool JumpButton { get; set; }
        public static bool AttackButton { get; set; }
        public static bool DashButton { get; set; }


        // Start is called before the first frame update
        private void Start()
        {
            HorizontalDir = HorizontalDirections.Idle;
            VerticalDir = VerticalDirections.Idle;
            JumpButton = false;
            DashButton = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer || testAsMobileDevice) return;

            SetHorizontalDirection();
            SetVerticalDirection();
            JumpButton = Input.GetButtonDown("Jump");
//            AttackButton = Input.GetButtonDown("Attack");
            DashButton = Input.GetButtonDown("Dash");
        }

        public void Jump(bool jumping)
        {
            JumpButton = jumping;
        }

        public void Attack(bool attacking)
        {
            AttackButton = attacking;
        }

        public void Dash(bool dashing)
        {
            DashButton = dashing;
        }

        private void SetHorizontalDirection()
        {
            if (Input.GetAxisRaw("Horizontal") < -0.7f)
                HorizontalDir = HorizontalDirections.Left;
            else if (Input.GetAxisRaw("Horizontal") > 0.7f)
                HorizontalDir = HorizontalDirections.Right;
            else
                HorizontalDir = HorizontalDirections.Idle;
        }

        private void SetVerticalDirection()
        {
            if (Input.GetAxisRaw("Vertical") < -0.7f)
                VerticalDir = VerticalDirections.Up;
            else if (Input.GetAxisRaw("Vertical") > 0.7f)
                VerticalDir = VerticalDirections.Down;
            else
                VerticalDir = VerticalDirections.Idle;
        }
    }
}