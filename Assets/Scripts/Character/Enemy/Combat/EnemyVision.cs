using System;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float _sizeY;
    [SerializeField] private float _sizeX;
    [SerializeField] private Transform _reference;
    [SerializeField] private LayerMask _playerLayerMask;

    public event Action<Player> PlayerDetected;
    public event Action PlayerNotVisible;

    private void Update()
    {
        TryDetectPlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(_reference.position, new Vector2(_sizeX, _sizeY));
    }

    private void TryDetectPlayer()
    {
        Collider2D collider = Physics2D.OverlapBox(_reference.position, new Vector2(_sizeX, _sizeY), 0f, _playerLayerMask);

        if (collider == null)
        {
            PlayerNotVisible?.Invoke();
            return;
        }

        PlayerDetected?.Invoke(collider.GetComponent<Player>());
    }
}
