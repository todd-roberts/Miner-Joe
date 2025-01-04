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
    private static Pickaxe _instance;
    private AudioSource _audioSource;

    public AudioClip hitSound;
    public AudioClip levelUpSound;
    public DirectionalPickaxe rightPick;
    public DirectionalPickaxe leftPick;
    public DirectionalPickaxe upPick;
    public DirectionalPickaxe downPick;
    
    [SerializeField]
    private ExperienceManager _experienceManager = new();

    [SerializeField]
    private int _damage = 1;

    private void Awake()
    {
        if (_instance == null) {
            _instance = this;
        }
        _audioSource = GetComponent<AudioSource>();
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


    private void OnBrickHit(Brick brick)
    {
        brick.TakeDamage(_damage);
       _audioSource.PlayOneShot(hitSound);
    }

    private void OnBrickBreak(Brick brick)
    {
        bool leveledUp = _experienceManager.AddExperience(brick.experience);

        _audioSource.PlayOneShot(brick.breakSound);

        if (leveledUp)
        {
            _audioSource.PlayOneShot(levelUpSound);
        }
    }

    public static bool IsMaxLevel()
    {
        return _instance._experienceManager.IsMaxLevel();
    }

    public static int GetCurrentLevel() {
        return _instance._experienceManager.GetCurrentLevel();
    }

    public static float GetLevelCompletionPercentage () {
        return _instance._experienceManager.GetLevelCompletionPercentage();
    }
}
