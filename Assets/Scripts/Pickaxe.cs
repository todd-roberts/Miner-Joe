using UnityEngine;

public enum SwingDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Pickaxe : MonoBehaviour
{
    public GameObject rightPick;
    public GameObject leftPick;
    public GameObject upPick;
    public GameObject downPick;



    private void Update()
    {
        HandleCompleteSwing();
    }

    private void HandleCompleteSwing() {
        Animator animator = null;

        if (rightPick.activeSelf)
            animator = rightPick.GetComponent<Animator>();
        else if (leftPick.activeSelf)
            animator = leftPick.GetComponent<Animator>();
        else if (upPick.activeSelf)
            animator = upPick.GetComponent<Animator>();
        else if (downPick.activeSelf)
            animator = downPick.GetComponent<Animator>();

        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Check if the animation is done playing
            if (stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0))
            {
                TurnOffPicks();
            }
        }
    }

    public void Swing(SwingDirection direction)
    {
        string animationName = "";
        GameObject pick = null;

        if (direction == SwingDirection.DOWN)
        {
            pick = downPick;
            animationName = "SwingDown";
        }

        if (direction == SwingDirection.UP)
        {
            pick = upPick;
            animationName = "SwingUp";
        }

        if (direction == SwingDirection.LEFT)
        {
            pick = leftPick;
            animationName = "SwingHorizontal";
        }

        if (direction == SwingDirection.RIGHT)
        {
            pick = rightPick;
            animationName = "SwingHorizontal";
        }

        pick.SetActive(true);
        pick.GetComponent<Animator>().Play(animationName);

    }

    private void TurnOffPicks()
    {
        rightPick.SetActive(false);
        leftPick.SetActive(false);
        upPick.SetActive(false);
        downPick.SetActive(false);
    }

    public bool IsSwinging() {
        return rightPick.activeSelf || leftPick.activeSelf || upPick.activeSelf || downPick.activeSelf;
    }
}
