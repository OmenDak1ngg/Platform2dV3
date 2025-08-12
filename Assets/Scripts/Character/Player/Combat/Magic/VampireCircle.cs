using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class VampireCircle : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _enemyLayerMask;

    [SerializeField] private int _lifeDrainDamage;
    [SerializeField] private float _lifeDrainCooldown = 4f;
    [SerializeField] private float _lifeDrainInterval;

    public event Action LifeDrainEnded;
    public float LifeDrainCooldown => _lifeDrainCooldown;

    private bool _lifeDrainEnded;

    private float _currentLifeDrainDuration;
    private float _lifeDrainDuration = 6f;

    private float _startAlpha = 0f;
    private float _maxAlpha = 1f;
    private Color _currentColor;

    private Collider2D[] _cachedColliders;

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;

    private WaitForSeconds _tickInterval;

    private void Start()
    {
        _cachedColliders = new Collider2D[10];

        _tickInterval = new WaitForSeconds(_lifeDrainInterval);
        _lifeDrainEnded = true;
        _currentLifeDrainDuration = _lifeDrainDuration;
        _collider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        ChangeAlpha(_startAlpha);
    }

    private void OnEnable()
    {
        _inputReader.Vampired += ActivateVampire;
    }

    private void OnDisable()
    {
        _inputReader.Vampired -= ActivateVampire;
    }

    private void ActivateVampire()
    {
        if (_lifeDrainEnded == false)
            return;

        _collider.gameObject.SetActive(true);

        StartCoroutine(PerformLifeDrain());
    }


    private IEnumerator PerformLifeDrain()
    {
        _lifeDrainEnded = false;

        ChangeAlpha(_maxAlpha);

        while(_currentLifeDrainDuration >= 0)
        {
            DrainLife();

            yield return _tickInterval;

            _currentLifeDrainDuration -= _lifeDrainInterval;
        }

        ChangeAlpha(_startAlpha);

        _currentLifeDrainDuration = _lifeDrainDuration;

        yield return StartCoroutine(ReloadLifeDrain());
    }

    private void DrainLife()
    {
        int collidersCount = Physics2D.OverlapCircleNonAlloc(transform.position, _collider.radius * transform.localScale.x, _cachedColliders, _enemyLayerMask);

        if (collidersCount == 0)
            return;

        for (int i = 0; i < collidersCount; i++)
        {
            Collider2D collider = _cachedColliders[i];

            if (collider.TryGetComponent<Enemy>(out Enemy enemy) == false)
                return;

            if (enemy.Health.Current - _lifeDrainDamage < 0)
                _player.Health.TakeHeal(enemy.Health.Current);
            else
                _player.Health.TakeHeal(_lifeDrainDamage);

            enemy.Health.TakeDamage(_lifeDrainDamage);
        }
    }

    private IEnumerator ReloadLifeDrain()
    {
        LifeDrainEnded?.Invoke();

        yield return new WaitForSeconds(LifeDrainCooldown);
        
        _lifeDrainEnded = true;
    }

    private void ChangeAlpha(float alpha)
    {
        _currentColor = _spriteRenderer.color;
        _currentColor.a = alpha;
        _spriteRenderer.color = _currentColor;
    }
}
