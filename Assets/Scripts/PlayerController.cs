using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        #region Declarations --------------------------------------------------
        private Vector2 _feetContactBox;

        [HideInInspector] 
        public Rigidbody2D rigidBody;

        [HideInInspector] 
        public Animator animator;

        [HideInInspector] 
        public Vector2 size;

        [HideInInspector] 
        public LevelManager levelManager;

        [Tooltip("This is how deep the player's feet overlap with the ground")]
        public float groundedSkin = 0.05f;

        public LayerMask groundLayer;
        public LayerMask killZoneLayer;
        public bool isGrounded;
        public bool isInKillZone;
        public bool invulnerable;
        public bool dead;

        [Range(0.1f, 10f)] public float invulnerabilityWindow;

        public Vector3 respawnPosition;



        [Header("Movement Settings")]

        [Range(1, 10)] 
        [Tooltip("Default: 5")]
        public float moveSpeed = 5f;

        [Header("Jump Settings")]

        [Range(1, 10)]
        [Tooltip("Default: 7.5")]
        public float jumpVelocity;
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;
        #endregion


        #region Private/Protected Methods -------------------------------------

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            size = GetComponent<BoxCollider2D>().size;
            respawnPosition = transform.position;
            levelManager = FindObjectOfType<LevelManager>();
            _feetContactBox = new Vector2(size.x, groundedSkin);
        }

        private void Update()
        {
            animator.SetBool("Grounded", isGrounded);
            animator.SetFloat("SpeedX", Math.Abs(rigidBody.velocity.x));
            Move();
            Jump();
        }

        private void FixedUpdate()
        {
            var boxCenter = (Vector2) transform.position + (size.y + _feetContactBox.y) * 0.5f * Vector2.down;

            isGrounded = Physics2D.OverlapBox(boxCenter, _feetContactBox, 0f, groundLayer);
            isInKillZone = Physics2D.OverlapBox(boxCenter, _feetContactBox, 0f, killZoneLayer);
        }

        private void OnTriggerEnter2D(Component other)
        {
            if (other.CompareTag("KillZone")) 
                levelManager.RespawnPlayer();

            if (other.CompareTag("Checkpoint")) 
                respawnPosition = other.transform.position;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("MovingPlatform")) 
                transform.parent = other.transform;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("MovingPlatform")) 
                transform.parent = null;
        }

        private void Move()
        {
            float xScale;
            var speed = moveSpeed;

            if (Input.GetAxisRaw("Horizontal") < 0f) // LEFT
            {
                speed *= -1;
                xScale = -1;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0f) // RIGHT
            {
                xScale = 1;
            }
            else // NOT MOVING
            {
                rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
                return;
            }

            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            transform.localScale = new Vector3(xScale, 1f, 1f);
        }

        private void Jump()
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rigidBody.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                isGrounded = false;
            }

            if (rigidBody.velocity.y < 0)
                rigidBody.gravityScale = fallMultiplier;
            else if (rigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
                rigidBody.gravityScale = lowJumpMultiplier;
            else
                rigidBody.gravityScale = 1f;
        }

        public void Kill()
        {
            gameObject.SetActive(false);
            dead = true;
        }

        public void Respawn()
        {
            gameObject.SetActive(true);
            dead = false;
        }

        #endregion
    }
}