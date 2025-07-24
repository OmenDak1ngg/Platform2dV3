using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        _rigidbody.linearVelocity = new Vector2(_speed * direction, _rigidbody.linearVelocity.y);
    }

    public void Jump()
    {

        if( _groundChecker.IsGrounded())
            _rigidbody.AddForce(new Vector2(0f, _jumpForce),ForceMode2D.Impulse);
    }
}
