using UnityEngine;
using UnityEngine.InputSystem;

public class Miner : Entity<Miner>
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private Vector2 _movementInput;
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _facing;

    [SerializeField]
    private Pickaxe _pickaxe;

    [SerializeField]
    private AudioClip _swingSound;

    private void Awake()
    {
        _stateMachine = new StateMachine<Miner>(this);
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override State<Miner> GetInitialState() => new MinerIdleState();

    public Rigidbody2D GetRigidbody() => _rb;

    public Animator GetAnimator() => _animator;

    public AudioSource GetAudioSource() => _audioSource;

    public SpriteRenderer GetSpriteRenderer() => _spriteRenderer;
    public Vector2 GetMovementInput() => _movementInput;

    public float GetMoveSpeed() => _moveSpeed;

    public Vector2 GetFacing() => _facing;

    public void SetFacing(Vector2 facing) => _facing = facing;

    public Pickaxe GetPickaxe() => _pickaxe;

    public void OnMove(InputValue value) => _movementInput = value.Get<Vector2>();

    public void OnFire() =>
        _stateMachine.SetNovelState(new MinerSwingingState());

    public bool IsInitialFacing() => _facing.x == 0 && _facing.y == 0;

    public void PlaySwingSound() => _audioSource.PlayOneShot(_swingSound);
}
