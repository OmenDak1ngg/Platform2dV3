using System;
using System.Collections;
using System.Runtime.InteropServices;
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

    [Header("lifeDrainPerInterval in seconds")]
    [SerializeField] private float _lifeDrainInterval;

    public event Action StartedDrainLife;

    private bool _lifeDrainEnded;

    private float _currentLifeDrainDuration;
    private float _lifeDrainDuration = 6f;

    public float LifeDrainCooldown => _lifeDrainCooldown;

    private float _startAlpha = 0.3f;
    private float _maxAlpha = 1f;
    private Color _currentColor;

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;

    private void Start()
    {

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

        StartedDrainLife?.Invoke();
        _collider.gameObject.SetActive(true);
        StartCoroutine(StartLifeDrain());
    }


    private IEnumerator StartLifeDrain()
    {
        _lifeDrainEnded = false;

        ChangeAlpha(_maxAlpha);

        while(_currentLifeDrainDuration >= 0)
        {
            yield return StartCoroutine(DrainLife());

            _currentLifeDrainDuration -= _lifeDrainInterval;
        }

        ChangeAlpha(_startAlpha);

        _currentLifeDrainDuration = _lifeDrainDuration;

        yield return StartCoroutine(ReloadLifeDrain());
    }

    private IEnumerator DrainLife()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _collider.radius * transform.localScale.x, _enemyLayerMask);

        if (colliders.Length == 0)
            yield return null;


        foreach(Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.Health.TakeDamage(_lifeDrainDamage);
            _player.Health.TakeHeal(_lifeDrainDamage);
        }

        yield return new WaitForSeconds(_lifeDrainInterval);
    }

    private IEnumerator ReloadLifeDrain()
    {
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
