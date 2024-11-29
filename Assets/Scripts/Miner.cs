using UnityEngine;
using UnityEngine.InputSystem;

public class Miner : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator; 
    private SpriteRenderer spriteRenderer; 

    private Vector2 movementInput;
    private Vector2 facing;

    public GameObject rightPick;
    public GameObject leftPick;
    public GameObject upPick;
    public GameObject downPick;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

        if (movementInput.x > 0)
        {
            facing = new Vector2(1, 0);
        }
        else if (movementInput.x < 0)
        {
            facing = new Vector2(-1, 0);
        }
        else if (movementInput.y > 0)
        {
            facing = new Vector2(0, 1);
        }
        else if (movementInput.y < 0)
        {
            facing = new Vector2(0, -1);
        }
    }

    public void OnFire()
    {
        Debug.Log(movementInput);

        if (facing.x > 0)
        {
            rightPick.SetActive(true);
        }
        else if (facing.x < 0)
        {
            leftPick.SetActive(true);
        }
        else if (facing.y > 0)
        {
            upPick.SetActive(true);
        }
        else if (facing.y < 0)
        {
            downPick.SetActive(true);
        }     
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
