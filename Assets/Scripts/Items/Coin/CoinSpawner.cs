using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{

    [SerializeField] private Coin _prefab;
    [SerializeField] private SpawnPointCoin[] _spawnpoints;
    [SerializeField] private float _spawnRate = 2f;
    [SerializeField] private int _capacity = 5;

    private List<SpawnPointCoin> _avalaibleSpawnpoints;
    private ObjectPool<Coin> _pool;
    private float _spawnpointCheckRadius = 0.5f;

    private void Start()
    {
        _avalaibleSpawnpoints = new List<SpawnPointCoin>(_spawnpoints);

        InitializePool();

        SpawnCoinsAmount(_spawnpoints.Length);
    }

    private void InitializePool() 
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => InstantiateCoin(),
            actionOnGet: (coin) => GetCoin(coin),
            actionOnRelease: (coin) => coin.gameObject.SetActive(false),
            actionOnDestroy: (coin) => DestroyCoin(coin),
            defaultCapacity: _capacity
            );
    }

    private void SpawnCoinsAmount(int countOfCoins)
    {
        for(int i = 0; i < countOfCoins; i++)
        {
            _pool.Get();
        }
    }

    private IEnumerator SpawnCoin()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnRate);
        yield return delay;
        _pool.Get();
    }

    private Coin InstantiateCoin()
    {
        Coin newCoin = Instantiate(_prefab);
        newCoin.Taked += ReleaseCoin;
        return newCoin;
    }

    private void GetCoin(Coin coin)
    {
        SpawnPointCoin takableSpawnpoint = GetAvalaibleSpawnpoint();

        coin.transform.position = takableSpawnpoint.transform.position;
        coin.gameObject.SetActive(true);
    }

    private SpawnPointCoin GetAvalaibleSpawnpoint()
    {
        if (_avalaibleSpawnpoints.Count == 0)
            return null;

        SpawnPointCoin takableSpawnpoint = _avalaibleSpawnpoints[Random.Range(0, _avalaibleSpawnpoints.Count)];
        _avalaibleSpawnpoints.Remove(takableSpawnpoint);
        return takableSpawnpoint;
    }

    private void ReleaseCoin(Coin coin)
    {
        Collider2D collider = Physics2D.OverlapCircle(coin.transform.position, _spawnpointCheckRadius);

        if (collider.TryGetComponent(out SpawnPointCoin spawnpoint))
        {
            _avalaibleSpawnpoints.Add(spawnpoint.GetComponent<SpawnPointCoin>());
            _pool.Release(coin);
            StartCoroutine(SpawnCoin());
        }
    }   

    private void DestroyCoin(Coin coin)
    {
        coin.Taked -= ReleaseCoin;
        Destroy(coin.gameObject);
    }
}
