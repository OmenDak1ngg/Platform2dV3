using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyMeleeAttacker : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _attackRange;

    private Vector3 _attackScale;

    private void Awake()
    {
        _attackScale = new Vector3(transform.localScale.x + _attackRange, transform.localScale.y, transform.localScale.z);
    }

    public void OnAttack()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, _attackScale, 0f, _mask);

        if (collider == null) 
            return;

        if(collider.TryGetComponent(out Player player) == false)
                return;

        player.Health.TakeDamage(_damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, _attackScale);
    }
}
