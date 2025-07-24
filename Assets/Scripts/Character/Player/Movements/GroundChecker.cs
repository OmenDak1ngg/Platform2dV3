using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _sizeX;
    [SerializeField] private float _sizeY;
    [SerializeField] private Transform _refernce;
    [SerializeField] private LayerMask _groundMask;

    public bool IsGrounded()
    {
        Collider2D hit = Physics2D.OverlapBox(_refernce.position, new Vector2(_sizeX, _sizeY), 0f, _groundMask);

        return hit != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(_refernce.position, new Vector3(_sizeX, _sizeY));
    }
}
