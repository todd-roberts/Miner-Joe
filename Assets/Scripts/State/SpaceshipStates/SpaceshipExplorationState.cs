using System;
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
        Vector3 currentShipPosition = _entity.transform.position;

        if (!ShipWrappedAround())
        {
            Vector3 movementThisFrame = GameState.Spaceship.Position - currentShipPosition;
            background.Offset(movementThisFrame);
        }

        GameState.Spaceship.Position = currentShipPosition;
    }

    private bool ShipWrappedAround()
    {
        Vector3 currentShipPosition = _entity.transform.position;
        Vector3 savedShipPosition = GameState.Spaceship.Position;

        bool nearEdgeX = Math.Abs(savedShipPosition.x) > Universe.GetSize() / 2;
        bool nearEdgeY = Math.Abs(savedShipPosition.y) > Universe.GetSize() / 2;

        bool wrappedHorizontal = nearEdgeX && Math.Sign(currentShipPosition.x) != Math.Sign(savedShipPosition.x);
        bool wrappedVertical = nearEdgeY && Math.Sign(currentShipPosition.y) != Math.Sign(savedShipPosition.y);

        return wrappedHorizontal || wrappedVertical;
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