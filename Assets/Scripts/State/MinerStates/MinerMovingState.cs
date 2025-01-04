using UnityEngine;

class MinerMovingState : State<Miner>
{
    public override void Enter() { 
        SetWalkingAnimation();
    }

    private void SetWalkingAnimation()
    {
        Animator animator = _entity.GetAnimator();
        animator.SetBool("isWalking", true);
    }
        
    public override void Update()
    {
        CheckForIdle();
        HandleFacing();
        HandleSpriteFlip();
    }

    private void CheckForIdle()
    {
        Vector2 movementInput = _entity.GetMovementInput();

        if (movementInput == Vector2.zero)
        {
            _entity.SetState(new MinerIdleState());
        }
    }

    private void HandleFacing()
    {
        Vector2 movementInput = _entity.GetMovementInput();
        Vector2 facing = _entity.GetFacing();

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

        _entity.SetFacing(facing);
    }

    private void HandleSpriteFlip()
    {
        Vector2 movementInput = _entity.GetMovementInput();
        SpriteRenderer spriteRenderer = _entity.GetSpriteRenderer();

        if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public override void FixedUpdate()
    {
        Move();
    }

    private void Move() {
       Rigidbody2D rb = _entity.GetRigidbody();
       
       rb.velocity = _entity.GetMovementInput() * _entity.GetMoveSpeed();
    }
}