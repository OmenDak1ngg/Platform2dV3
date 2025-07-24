using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerAnimator _animatorController;

    private float _threshold = 0.5f;
    private Rigidbody2D _rigidbody;

    protected void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputReader.Attacked += Attack;
        _inputReader.Jumped += Jump;
        Health.Dead += Death;
    }

    private void OnDisable()
    {
        _inputReader.Attacked += Attack;
        _inputReader.Jumped -= Jump;
        Health.Dead -= Death;
    }

    public void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            Move();
        }
    }

    private void Move()
    {
        _playerMover.Move(_inputReader.Direction);

        bool isRunning = Mathf.Abs(_rigidbody.linearVelocity.x) >= _threshold;
        _animatorController.RunAnimation(isRunning);      
    }

    private void Jump()
    {
        _playerMover.Jump();
    }

    private void Attack()
    {
        _animatorController.AttackAnimation();
    }

    protected override void Death()
    {
        _animatorController.KilledAnimation();
    }

    public void OnDeath()
    {
        base.Death();
    }
}
