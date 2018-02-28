using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFTP.Level;
using System;

namespace ProjectFTP
{
    public class Character : MonoBehaviour, IActor
    {
        public enum Action { JUMP, ATTACK, TAKE_DAMAGE, DIE };

        #region
        private static List<Vector2> directions = new List<Vector2>() { Vector2.up, Vector2.left, Vector2.down, Vector2.right };
        #endregion

        #region event handlers
        public delegate void OnCharacterEvent(Action action);

        public event OnCharacterEvent ActionHandler;
        #endregion

        #region attributes
        [Range(1f, 10f)]
        public float runSpeed = 5.0f;
        [Range(1f, 10f)]
        public float climbSpeed = 2.5f;
        [Range(0f, 2f)]
        public float jumpForce = 1.8f;
        [Range(0, 5)]
        public int maxJumps = 2;
        public IAttack attack;

        private int health = 1;
        private bool climbing = false;
        private bool grounded = true;
        private int jumps = 0;
        private Direction facing = Direction.EAST;
        private Vector2 movement;
        #endregion

        #region referenced components
        private Rigidbody2D rb2d;
        #endregion

        #region methods
        // Move the character
        public void Move(Vector2 movement)
        {
            // determine which direction the character is facing.
            facing = DetermineDirection(movement);

            // Scale movement with movement speeds.
            movement.Scale(new Vector2(runSpeed, climbing ? climbSpeed : 0f));

            // Store movement for animator
            this.movement = movement;

            // Counterbalance the horizontal force of the character.
            CounterBalanceForces(movement);

            // Apply movement.
            Rigidbody2D.position += movement;
        }

        public void Attack()
        {
            if (attack != null)
            {
                attack.Trigger(this);
                OnCharacterAction(Action.ATTACK);
            }
        }

        public void Jump()
        {
            if (jumps < maxJumps)
            {
                jumps++;
                Rigidbody2D.velocity = -Physics2D.gravity * jumpForce;
                OnCharacterAction(Action.JUMP);
            }
        }

        public void Climb()
        {
            climbing = !climbing && IsOnClimbableSurface;
            // Disable gravity while climbing
            if (climbing)
            {
                Rigidbody2D.gravityScale = 0.0f;
                Rigidbody2D.velocity = Vector2.zero;
            }
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            OnCharacterAction(Action.TAKE_DAMAGE);
            if (health < 0)
            {
                OnCharacterAction(Action.DIE);
            }
        }

        // determine which direction the character is facing.
        private Direction DetermineDirection(Vector2 movement)
        {
            if (movement.x < 0)
            {
                return Direction.WEST;
            }
            else if (movement.x > 0)
            {
                return Direction.EAST;
            }
            return facing;
        }

        // Counterbalance the horizontal force of the character.
        private void CounterBalanceForces(Vector2 movement)
        {
            Rigidbody2D rb2d = Rigidbody2D;
            // Check if direction of force is oposite of the direction the the movement
            if ((rb2d.velocity.x / Mathf.Abs(rb2d.velocity.x)) != (movement.x / Mathf.Abs(movement.x)))
            {
                // TODO Add time delay after force has been added. (Lerp?)
                float ratio = (((rb2d.velocity.x + runSpeed) / (runSpeed * 2)) - 0.5f) / 0.5f;
                ratio = 1 - (movement.x < 0 ? -ratio : ratio);
                ratio = Mathf.Abs(ratio) < 1 ? 0 : ratio;
                rb2d.velocity += movement * ratio;
                rb2d.velocity.Scale(new Vector2((Mathf.Abs(rb2d.velocity.x) < 0.1f) ? 0f : 1f, 1f));
            }
        }

        protected virtual void OnCharacterAction(Action action)
        {
            OnCharacterEvent handler = ActionHandler;
            if (handler != null)
            {
                handler(action);
            }
        }
        #endregion

        #region properties
        // Is the character alive
        public bool IsAlive
        {
            get { return health > 0; }
        }

        // Is te character grounded
        public bool IsGrounded
        {
            get
            {
                RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                Vector2.down,
                1.2f, // TODO Remove magic number
                LayerMask.GetMask(
                    TileType.Ground.ToString(),
                    TileType.Climbable.ToString()
                )
                );
                return hit.collider != null;
            }
        }

        // Is the character climbing
        public bool IsClimbing
        {
            get { return climbing; }
        }

        // Which direction is the character facing
        public Direction Facing
        {
            get { return facing; }
        }

        // Helper method to see if character is on climbable surface
        private bool IsOnClimbableSurface
        {
            get
            {
                Vector2 hit = directions.Find(delegate (Vector2 direction)
                {
                    RaycastHit2D rayHit = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask(TileType.Climbable.ToString()));
                    return rayHit.collider != null;
                });
                return hit.magnitude != 0.0f;
            }
        }

        // helper property to ensure that the gameObject has a 2D rigidbody
        private Rigidbody2D Rigidbody2D
        {
            get
            {
                if (rb2d == null)
                {
                    rb2d = GetComponent<Rigidbody2D>();
                    // Ensure the character gameObject has a 2D rigidbody
                    if (rb2d == null)
                    {
                        rb2d = gameObject.AddComponent<Rigidbody2D>();
                    }
                }
                return rb2d;
            }
        }

        public Vector2 Velocity
        {
            get { return Rigidbody2D.velocity; }
        }

        public Vector2 Movement {
            get { return movement;  }
        }
        #endregion

        #region Unity Runtime methods
        void Start()
        {

            if (GetComponent<CharacterAnimator>() == null)
            {
                gameObject.AddComponent<CharacterAnimator>();
            }

            // Add CharacterController if platform is not mobile 
#if UNITY_ANDROID
#elif UNITY_IOS
#else
            if (GetComponent<CharacterController>() == null)
            {
                gameObject.AddComponent<CharacterController>();
            }
#endif
        }

        void FixedUpdate()
        {
            if (!IsAlive)
            {
                return;
            }

            // Check if the character is grounded.
            if (IsGrounded)
            {
                if (!grounded || jumps > 1)
                {
                    jumps = 0;
                    grounded = true;
                }
            }
            else
            {
                grounded = false;
            }

            if (IsOnClimbableSurface)
            {
                Rigidbody2D.gravityScale = 0;
                // Stick to surface while climbing
                Rigidbody2D.velocity = Vector2.zero;
                climbing = true;

            } else if (climbing) {
                climbing = false;
                Rigidbody2D.gravityScale = 1;
            }
            /*
             * TODO Fix climbing with button?
            if (!climbing || (climbing && !IsOnClimbableSurface))
            {
                climbing = false;
                Rigidbody2D.gravityScale = 1;
            }

            if (climbing)
            {
                Rigidbody2D.velocity = Vector2.zero;
            }
            */
        }
        #endregion
    }
}
