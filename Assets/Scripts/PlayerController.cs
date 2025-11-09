using UnityEngine;

// NUnit constraints using statements are not needed for this game logic
// using NUnit.Framework.Constraints; 
// using Unity.VisualScripting; // Not used in the provided code

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody_;
    private Animator animator_;
    private bool in_air = false;
    
    [SerializeField] private float jump_force = 6f;
    public GameObject bulletPrefab; // Still needed if you use it elsewhere
    public Transform firePoint;     // Still needed if you use it elsewhere
    private bool canDoubleJump = true;
    private bool is_falling;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody_ = GetComponent<Rigidbody2D>();
        animator_ = GetComponent<Animator>();

        if (rigidBody_ == null)
        {
            Debug.LogError("PlayerController: Rigidbody2D component missing!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float verticalVelocity = rigidBody_.linearVelocity.y;

        // --- Jump Input Logic ---
        if (Input.GetButtonDown("Jump"))
        {
            if(in_air && canDoubleJump == false)
            {
                // do nothing
            }
            else
            {
                Jump();
            }
        }
        
        // --- Animation State Logic ---
        if (verticalVelocity > 0.1f)
        {
            // Going up (jumping)
            animator_.SetBool("in_air", true);
            animator_.SetBool("is_falling", false);
            in_air = true;
            is_falling = false;
        }
        else if (verticalVelocity < -0.1f)
        {
            // Falling down
            is_falling = true;
            in_air = true;
            animator_.SetBool("in_air", true);
            animator_.SetBool("is_falling", true);
        }
        else
        {
            // Near zero velocity (on ground or peak of jump)
            is_falling = false;
            // Only set animation bools if we are truly grounded
            if (!in_air) 
            {
                animator_.SetBool("is_falling", false);
                animator_.SetBool("in_air", false);
            }
        }

        // --- Shoot Input Logic ---
        if (Input.GetButtonDown("shoot"))
        {
            Shoot();
        }
    }

    private void Jump()
    {
        if(in_air == true && canDoubleJump == true)
        {
            canDoubleJump = false;
        }

        // FIX: This line overwrites the existing Y velocity entirely.
        // This prevents stacking momentum from the previous frame's velocity.
        rigidBody_.linearVelocity = new Vector2(rigidBody_.linearVelocity.x, jump_force);
        in_air = true;
    }

    private void Shoot()
    {
        animator_.SetTrigger("shoot");
        // Add your bullet instantiation logic here if needed
        // Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // FIX: Explicitly zero out vertical velocity upon impact.
            // This eliminates any residual bounce that might make the physics engine think
            // you still have a slight upward 'push' when you land and immediately jump again.
            rigidBody_.linearVelocity = new Vector2(rigidBody_.linearVelocity.x, 0f);

            in_air = false;
            canDoubleJump = true;
            animator_.SetBool("in_air", false);
            animator_.SetBool("is_falling", false); // Ensure falling is also off
        }
    }
}
