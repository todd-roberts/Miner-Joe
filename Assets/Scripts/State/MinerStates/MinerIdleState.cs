using UnityEngine;

class MinerIdleState : State<Miner>
{
    public override void Enter()
    {
        SetIdleAnimation();
    }

    public override void Update(){
        HandleMovement();
    }

    private void HandleMovement()
    {
       Vector2 movementInput = _entity.GetMovementInput();

        if (movementInput != Vector2.zero)
        {
            _entity.SetState(new MinerMovingState());
        }
    }

    private void SetIdleAnimation()
    {
        Animator animator = _entity.GetAnimator();
        animator.SetBool("isWalking", false);
    }
}
