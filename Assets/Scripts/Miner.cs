using UnityEngine;
using UnityEngine.InputSystem;

public class Miner : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    private Vector2 movementInput;
    private Vector2 facing;

    public Pickaxe pickaxe;

    public AudioClip nyahSound;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    private void HandleFacing()
    {
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
        HandleSwing();
    }

    private void HandleSwing()
    {
        if (pickaxe.IsSwinging())
        {
            return;
        }

        audioSource.PlayOneShot(nyahSound);

        if (IsInitialFacing() || facing.x > 0)
        {
            SwingRight();
        }
        else if (facing.x < 0)
        {
            SwingLeft();
        }
        else if (facing.y > 0)
        {
            SwingUp();
        }
        else if (facing.y < 0)
        {
            SwingDown();
        }
    }

    private bool IsInitialFacing() => facing.x == 0 && facing.y == 0;

    private void SwingRight()
    {
        pickaxe.Swing(SwingDirection.RIGHT);
        animator.Play("SwingHorizontal");
    }

    private void SwingLeft()
    {
        pickaxe.Swing(SwingDirection.LEFT);
        animator.Play("SwingHorizontal");
    }

    private void SwingUp()
    {
        pickaxe.Swing(SwingDirection.UP);
        animator.Play("SwingUp");
    }

    private void SwingDown()
    {
        pickaxe.Swing(SwingDirection.DOWN);
        animator.Play("SwingDown");
    }

    private void Update()
    {
        if (!pickaxe.IsSwinging())
        {
            HandleFacing();
            HandleAnimation();
            HandleSpriteFlip();
        }
    }

    // FixedUpdate is used instead of Update when working with physics (Rigidbody)
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = pickaxe.IsSwinging() ? Vector2.zero : movementInput * moveSpeed;

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
