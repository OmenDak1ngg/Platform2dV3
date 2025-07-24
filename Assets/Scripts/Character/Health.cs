using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _max;

    public float Max => _max;
    public float Current { get; private set; }

    public event Action Dead;
    public event Action Changed;

    private void Awake()
    {
        Current = Max;
    }

    public void TakeDamage(float amount)
    {
        if (amount < 0)
            return;

        if (Current < amount)
            Current = 0;
        else
            Current -= amount;

        if(Current <= 0)
            Dead?.Invoke();

        Changed?.Invoke();
    }

    public void TakeHeal(float amount)
    {
        if (amount < 0)
            return;

        if (Current + amount > Max)
            Current = Max;
        else
            Current += amount;

        Changed?.Invoke();
    }
}
