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
    private AudioSource audioSource;

    public AudioClip hitSound;
    public DirectionalPickaxe rightPick;
    public DirectionalPickaxe leftPick;
    public DirectionalPickaxe upPick;
    public DirectionalPickaxe downPick;

    [SerializeField]
    private int damage = 1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleCompleteSwing();
    }

    private void HandleCompleteSwing()
    {
        DirectionalPickaxe pickaxe = GetActivePickaxe();

        if (pickaxe == null)
            return;

        if (!pickaxe.IsSwinging())
        {
           pickaxe.gameObject.SetActive(false);
        }
    }

    private DirectionalPickaxe GetActivePickaxe()
    {
        if (rightPick.gameObject.activeSelf)
            return rightPick;
        else if (leftPick.gameObject.activeSelf)
            return leftPick;
        else if (upPick.gameObject.activeSelf)
            return upPick;
        else if (downPick.gameObject.activeSelf)
            return downPick;

        return null;
    }

    public void Swing(SwingDirection direction)
    {
        DirectionalPickaxe pickaxe = GetPickaxeByDirection(direction);

        pickaxe.gameObject.SetActive(true);

        pickaxe.Swing();
    }

    private DirectionalPickaxe GetPickaxeByDirection(SwingDirection direction)
    {
        switch (direction)
        {
            case SwingDirection.DOWN:
                return downPick;
            case SwingDirection.UP:
                return upPick;
            case SwingDirection.LEFT:
                return leftPick;
            case SwingDirection.RIGHT:
                return rightPick;
            default:
                return null;
        }
    }

    public bool IsSwinging()
    {
        DirectionalPickaxe pickAxe = GetActivePickaxe();

        if (pickAxe == null) {
            return false;
        }

        return pickAxe.IsSwinging();
    }

    public int GetDamage()
    {
        return damage;
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
}
