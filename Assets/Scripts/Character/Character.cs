using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Health _health;

    public Health Health => _health;

    private void OnEnable()
    {
        _health.Changed += Death;
    }

    private void OnDisable()
    {
        _health.Changed -= Death;
    }

    protected virtual void Death()
    {
        if(_health.Current <= 0)
            Destroy(transform.parent.gameObject);
    }
}
