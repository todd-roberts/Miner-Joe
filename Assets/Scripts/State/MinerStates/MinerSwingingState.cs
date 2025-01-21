using UnityEngine;

class MinerSwingingState : State<Miner>
{
    public override void Enter()
    {
        HandleSwing();
    }

    private void HandleSwing()
    {
        Pickaxe pickaxe = _entity.GetPickaxe();
        Vector2 facing = GameState.Miner.Facing;
        Animator animator = _entity.GetAnimator();

        _entity.PlaySwingSound();

        bool isInitialFacing = GameState.Miner.Facing == Vector2.zero;

        if (isInitialFacing || facing.y < 0)
        {
            pickaxe.Swing(SwingDirection.DOWN);
            animator.Play("SwingDown");
        }
         else if (facing.y > 0)
        {
            pickaxe.Swing(SwingDirection.UP);
            animator.Play("SwingUp");
        }
        else if (facing.x > 0)
        {
            pickaxe.Swing(SwingDirection.RIGHT);
            animator.Play("SwingHorizontal");
        }
        else if (facing.x < 0)
        {
            pickaxe.Swing(SwingDirection.LEFT);
            animator.Play("SwingHorizontal");
        }
    
    }

    public override void Update()
    {
        HandleCompleteSwing();
    }

    private void HandleCompleteSwing()
    {
        Pickaxe pickaxe = _entity.GetPickaxe();

        if (!pickaxe.IsSwinging())
        {
            _entity.SetState(new MinerIdleState());
        }
    }

    public override void FixedUpdate()
    {
        Stand();
    }

    private void Stand()
    {
        Rigidbody2D rb = _entity.GetRigidbody();

        rb.velocity = Vector2.zero;
    }
}
