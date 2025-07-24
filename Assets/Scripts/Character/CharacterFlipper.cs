using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterFlipper : MonoBehaviour
{
    private readonly Vector2 FlippedRotation = new Vector3(0,180,0);
    private readonly Vector2 BaseRotation = new Vector3(0, 0,0);

    private float _threshold = 0.1f;
    private bool _isFacingRight;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _isFacingRight = true;
    }

    private void Update()
    {
        UpdateFacingDirection();
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.rotation = Quaternion.Euler(_isFacingRight ? BaseRotation : FlippedRotation);
    }

    public void UpdateFacingDirection()
    {
        if(_rigidbody.linearVelocity.x > _threshold)
        {
            if(_isFacingRight == false)
            {
                Flip();
            }
        }
        
        if(_rigidbody.linearVelocity.x < -_threshold)
        {
            if(_isFacingRight == true)
            {
                Flip();
            }      
        }
    }
}
