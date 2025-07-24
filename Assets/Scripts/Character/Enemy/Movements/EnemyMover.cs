using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Patroller _patroller;
    [SerializeField] private float _threshold = 0.1f;
    [SerializeField] private float _restTime = 2f;
    [SerializeField] private float _playerSearchTime =5f;

    private Rigidbody2D _rigidbody;
    private Waypoint _currentWaypoint;
    private WaitForSeconds _restDelay;
    private bool _isResting;

    private bool _isSearching;
    private WaitForSeconds _searchTime;
    private bool _isChasing;
    private Player _chasingPlayer;

    private Coroutine _restCoroutine;
    private Coroutine _searchCoroutine;

    public event Action ReachedWaypoint;

    private void Start()
    {
        _isSearching = false;
        _searchTime = new WaitForSeconds(_playerSearchTime);
        _isChasing = false;
        _isResting = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _restDelay = new WaitForSeconds(_restTime);
    }

    private void FixedUpdate()
    {
        if (_isChasing)
        {
            ChasePlayer();
            return;
        }

        if (_isResting || _isSearching)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            return;
        }

        Move();
    }

    private void Move()
    {
        if((transform.position - _currentWaypoint.transform.position).sqrMagnitude < _threshold)
        {
            _restCoroutine = StartCoroutine(Rest());
            ReachedWaypoint?.Invoke();
            return;
        }

        float direction = Mathf.Sign(transform.position.x - _currentWaypoint.transform.position.x);

        if (direction < 0)
        {
            _rigidbody.linearVelocity = new Vector2(_speed, 0);
        }
        else if(direction > 0)
        {
            _rigidbody.linearVelocity = new Vector2(-_speed, 0);
        }
    }

    private IEnumerator Rest()
    {
        _isResting =  true;

        yield return _restDelay;

        _isResting = false;    
    }

    private IEnumerator SearchPlayer()
    {
        yield return _searchTime;

        _isSearching = false;
        _patroller.FindClosestWaypoint();
    }

    public void StartChasingPlayer(Player player)
    {
        if (_isChasing)
            return;

        if(_restCoroutine != null)  StopCoroutine(_restCoroutine);

        _isResting = false;
        _isSearching = false;
        _isChasing = true;
        _chasingPlayer = player;
    }

    private void ChasePlayer()
    {
        Vector2 directoin = (_chasingPlayer.transform.position - transform.position).normalized;
        directoin = new Vector2(directoin.x, 0);
        _rigidbody.linearVelocity = directoin * _speed;
    }

    public void LostPlayer()
    {
        if (_isChasing == false)
            return;

        if (_searchCoroutine != null) StopCoroutine(_searchCoroutine);

        _isChasing = false;
        _isSearching = true;
        _searchCoroutine = StartCoroutine(SearchPlayer());
    }

    public void SetNewWaypoint(Waypoint waypoint)
    {
        _currentWaypoint = waypoint;
    }
}
