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
        Vector2 facing = _entity.GetFacing();
        Animator animator = _entity.GetAnimator();

        _entity.PlaySwingSound();

        if (_entity.IsInitialFacing() || facing.x > 0)
        {
            pickaxe.Swing(SwingDirection.RIGHT);
            animator.Play("SwingHorizontal");
        }
        else if (facing.x < 0)
        {
            pickaxe.Swing(SwingDirection.LEFT);
            animator.Play("SwingHorizontal");
        }
        else if (facing.y > 0)
        {
            pickaxe.Swing(SwingDirection.UP);
            animator.Play("SwingUp");
        }
        else if (facing.y < 0)
        {
            pickaxe.Swing(SwingDirection.DOWN);
            animator.Play("SwingDown");
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
