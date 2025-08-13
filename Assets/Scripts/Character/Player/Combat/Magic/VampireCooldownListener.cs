using System;
using System.Collections;
using UnityEngine;

public class VampireCooldownListener : MonoBehaviour
{
    [SerializeField] private VampireCircle _vampireCircle;

    private float _delta = 1f;

    private float _startValue = 1f;
    public float CurrentValue { get; private set; }

    public event Action UpdatedCooldown;

    private void Start()
    {
        CurrentValue = _startValue;

        _delta = _startValue / _vampireCircle.LifeDrainCooldown;
    }

    private void OnEnable()
    {
        _vampireCircle.LifeDrainEnded += StartShowingCooldown;
    }

    private void OnDisable()
    {
        _vampireCircle.LifeDrainEnded -= StartShowingCooldown;    
    }

    private void StartShowingCooldown()
    {
        StartCoroutine(ShowCooldown());
    }

    private IEnumerator ShowCooldown()
    {
        CurrentValue = _startValue;

        while(CurrentValue  != 0)
        {
            CurrentValue = Mathf.MoveTowards(CurrentValue, 0, _delta * Time.deltaTime);
            UpdatedCooldown?.Invoke();

            yield return null;
        }
    }
}
