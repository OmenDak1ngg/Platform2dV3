using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Player _player;

    private Collider2D _mainCollider;

    private void Start()
    {
        _mainCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            _wallet.AddCoin();
            coin.Interract();
        }

        if(other.TryGetComponent(out AidKit aidKit))
        {
            _player.Health.TakeHeal(aidKit.HeatlCount);
            aidKit.Interract();
        }
    }
}
