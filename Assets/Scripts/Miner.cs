using UnityEngine;
using UnityEngine.InputSystem;

public class Miner : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator; 
    private SpriteRenderer spriteRenderer; 

    private Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    private void Update()
    {
        HandleAnimation();
        HandleSpriteFlip();
    }

    // FixedUpdate is used instead of Update when working with physics (Rigidbody)
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = movementInput * moveSpeed;
    }

    private void HandleAnimation()
    {
        bool isWalking = movementInput.magnitude > 0;
        animator.SetBool("isWalking", isWalking);
    }

    private void HandleSpriteFlip()
    {
        if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
