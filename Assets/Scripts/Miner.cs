using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Miner : Entity<Miner>
{
    private static Miner _instance;
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

    private HashSet<int> _keys = new HashSet<int>();

    private int? _doorEntered = null;

    protected override void OnAwake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        _stateMachine = new StateMachine<Miner>(this);
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public static void Broadcast<TData>(string message, TData data)
    {
        Debug.Log(_instance);
        _instance.BroadcastMessage(message, data);
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

    public void AddKey(int keyId)
    {
        _keys.Add(keyId);
    }

    public void TryOpenDoor(Door door)
    {
        if (CanOpenDoor(door))
        {
            EnterDoor(door);

        }
    }

    private bool CanOpenDoor(Door door) => _keys.Contains(door.GetId());

    private void EnterDoor(Door door)
    {
        _doorEntered = door.GetId();
        door.Open();
        //_stateMachine.SetState(new MinerEnterDoorState());
    }


    private void OnSceneLoaded()
    {
       HandleExitDoor();
    }

    private void HandleExitDoor() {
        if (_doorEntered == null) return;

        Door doorEntered = Door.GetDoorById(_doorEntered.Value);

        transform.position = doorEntered.transform.position - new Vector3(0, 1.5f);

        _doorEntered = null;
    }
}
