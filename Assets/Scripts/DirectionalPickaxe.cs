using UnityEngine;

public class DirectionalPickaxe : MonoBehaviour
{
    public SwingDirection swingDirection;
    private Animator _animator;
    private Collider2D _collider;

    private Pickaxe parentPickaxe;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        DisableCollider();
        parentPickaxe = GetComponentInParent<Pickaxe>();
    }

    private void EnableCollider()
    {
        _collider.enabled = true;
    }

    private void DisableCollider()
    {
        _collider.enabled = false;
    }

    public void Swing()
    {
        string animationName = GetAnimationName();

        _animator.Play(animationName);
    }

    private string GetAnimationName()
    {
        string animationName = "";

        switch (swingDirection)
        {
            case SwingDirection.UP:
                animationName = "SwingUp";
                break;
            case SwingDirection.DOWN:
                animationName = "SwingDown";
                break;
            case SwingDirection.LEFT:
            case SwingDirection.RIGHT:
                animationName = "SwingHorizontal";
                break;
        }

        return animationName;
    }

    public bool IsSwinging()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        bool isDoneSwinging = stateInfo.normalizedTime >= 1.0f && !_animator.IsInTransition(0);

        if (isDoneSwinging)
        {
            DisableCollider();
        }

        return !isDoneSwinging;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Brick"))
        {
           HandleHitBrick(collision);
        }
    }

    private void HandleHitBrick(Collider2D collision)
    {
        Brick brick = collision.GetComponent<Brick>();
        Miner.Broadcast("OnBrickHit", brick);
    }
}
