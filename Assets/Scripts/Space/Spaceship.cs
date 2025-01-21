using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : Entity<Spaceship>
{
    // Components
    private Rigidbody2D _rb;

    // Settings
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _planetDetectionRange = 25f;
    [SerializeField] private float _planetInViewRange = 5f;
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private Background _background;

    // Temporary State    
    private Vector2 _movementInput;

    // Lifecycle Methods
    protected override void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        base.Awake();
    }

    // State Machine Setup
    public override State<Spaceship> GetInitialState() => new SpaceshipExplorationState();

    // Component Getters
    public Rigidbody2D GetRigidbody() => _rb;

    // Setting Getters
    public float GetMoveSpeed() => _moveSpeed;
    public float GetRotationSpeed() => _rotationSpeed;
    public float GetPlanetDetectionRange() => _planetDetectionRange;
    public float GetPlanetInViewRange() => _planetInViewRange;
    public Transform GetSpriteTransform() => _spriteTransform;
    public Background GetBackground() => _background;

    // Temporary State Getters
    public Vector2 GetMovementInput() => _movementInput;

    // Input Handlers
    public void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();

        BroadcastMessage("OnShipMove", _movementInput);
    }

    // Event Handlers
    private void OnSceneLoaded()
    {
        _stateMachine.SetState(new SpaceshipFrozenState());
    }

    private void OnSceneReady()
    {
        _stateMachine.SetState(new SpaceshipExplorationState());
    }
}
