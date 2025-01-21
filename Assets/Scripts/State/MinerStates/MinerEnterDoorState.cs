using UnityEngine;

class MinerEnterDoorState : State<Miner>
{
    private Door _door;
    private Vector3 _startPosition;
    private float _lerpProgress = 0f;
    private const float _lerpDuration = .25f;

    public MinerEnterDoorState(Door door)
    {
        _door = door;
    }

    public Door GetDoor() => _door;

    public override void Enter()
    {
        _startPosition = _entity.transform.position;
        SetIdleAnimation();
        _entity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void SetIdleAnimation()
    {
        Animator animator = _entity.GetAnimator();
        animator.SetBool("isWalking", false);
    }

    public override void Update()
    {
        LerpPositionToDoor();
    }

    private void LerpPositionToDoor()
    {
        if (_lerpProgress < 1.0f)
        {
            _lerpProgress += Time.deltaTime / _lerpDuration;
            _entity.transform.position = Vector3.Lerp(_startPosition, _door.transform.position, _lerpProgress);

            if (_lerpProgress >= 1.0f)
            {
                _entity.transform.position = _door.transform.position;
                OnReachedDoor();
            }
        }
    }

    private void OnReachedDoor()
    {
        _door.Open();
    }
}
