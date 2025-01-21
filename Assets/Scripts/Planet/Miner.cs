using UnityEngine;
using UnityEngine.InputSystem;

public class Miner : Entity<Miner>
{
    // Components
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    // Settings
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _exitDoorOffset = 1f;
    [SerializeField] private AudioClip _swingSound;
    [SerializeField] private AudioClip _keyPickupSound;
    [SerializeField] private Pickaxe _pickaxe;

    // Temporary State
    private Vector2 _movementInput;

    // Lifecycle Methods
    protected override void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        base.Awake();
    }

    // State Machine Setup
    public override State<Miner> GetInitialState() => new MinerIdleState();
    
    // Component Getters
    public Rigidbody2D GetRigidbody() => _rb;
    public Animator GetAnimator() => _animator;
    public SpriteRenderer GetSpriteRenderer() => _spriteRenderer;

    // Setting Getters
    public float GetMoveSpeed() => _moveSpeed;
    public Pickaxe GetPickaxe() => _pickaxe;

    // Temporary State Getters
    public Vector2 GetMovementInput() => _movementInput;

    // Input Handlers
    private void OnMove(InputValue value) => _movementInput = value.Get<Vector2>();
    private void OnFire() => _stateMachine.SetNovelState(new MinerSwingingState());

    // Event Handlers
    private void OnSceneLoaded()
    {
        _stateMachine.SetState(new MinerFrozenState());
        HandleExitDoor();
    }

    private void HandleExitDoor()
    {
        if (GameState.Miner.DoorEntered == null) return;

        int doorId = GameState.Miner.DoorEntered.GetValueOrDefault();

        Door returnDoor = Door.GetDoorById(doorId);

        GameState.Miner.DoorEntered = null;

        transform.position = returnDoor.transform.position - new Vector3(0, _exitDoorOffset);
    }

    private void OnSceneReady()
    {
        _stateMachine.SetState(new MinerIdleState());
    }

    // Helpers
    public void PlaySwingSound() => _audioSource.PlayOneShot(_swingSound);

    public void AddKey(int keyId)
    {
        _audioSource.PlayOneShot(_keyPickupSound);
        GameState.Miner.Keys.Add(keyId);
    }

    public void TryOpenDoor(Door door)
    {
        bool canOpenDoor = door.IsReturnDoor() || GameState.Miner.Keys.Contains(door.GetId());

        if (canOpenDoor)
        {
            GameState.Miner.DoorEntered = door.GetId();
            _stateMachine.SetState(new MinerEnterDoorState(door));
        }
    }
}
