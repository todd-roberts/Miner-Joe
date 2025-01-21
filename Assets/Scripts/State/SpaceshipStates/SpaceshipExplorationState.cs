using UnityEngine;

class SpaceshipExplorationState : State<Spaceship>
{
    public override void Enter() { }

    public override void Update()
    {
        UpdateUniversePosition();
        CheckNearbyPlanets();
    }

    private void UpdateUniversePosition()
    {
        Background background = _entity.GetBackground();
        Vector3 shipPosition = _entity.transform.position;

        background.Offset(GameState.Spaceship.Position - shipPosition);
        GameState.Spaceship.Position = shipPosition;
    }

    private void CheckNearbyPlanets()
    {
        float planetDetectionRange = _entity.GetPlanetDetectionRange();
        float planetInViewRange = _entity.GetPlanetInViewRange();

        SpacePlanet nearestPlanet = Universe.GetNearestActivePlanet();

        if (nearestPlanet == null) return;

        string message = "OnNoPlanetNearby";

        Vector2 direction = Vector2.zero;

        float distance = Vector2.Distance(_entity.transform.position, nearestPlanet.transform.position);

        bool isNearbyButNotVisible = distance < planetDetectionRange && distance > planetInViewRange;

        if (isNearbyButNotVisible)
        {
            message = "OnPlanetNearby";
            direction = (nearestPlanet.transform.position - _entity.transform.position).normalized;
        }

        GameManager.Broadcast(message, direction);
    }


    public override void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Rigidbody2D rb = _entity.GetRigidbody();
        Vector2 movementInput = _entity.GetMovementInput();
        float moveSpeed = _entity.GetMoveSpeed();

        rb.velocity = movementInput * moveSpeed;

        RotateSprite();
    }

    private void RotateSprite()
    {
        Vector2 movementInput = _entity.GetMovementInput();
        Transform spriteTransform = _entity.GetSpriteTransform();
        float rotationSpeed = _entity.GetRotationSpeed();

        if (movementInput != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle + 90);

            spriteTransform.rotation = Quaternion.Lerp(
                spriteTransform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }
}