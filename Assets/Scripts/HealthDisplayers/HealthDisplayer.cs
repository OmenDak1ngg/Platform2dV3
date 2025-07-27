using System.Collections;
using UnityEngine;

abstract public class HealthDisplayer : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [SerializeField] private Transform reference;

    private void OnEnable()
    {
        Health.Changed += StartUpdatetingHealth;
    }

    private void OnDisable()
    {
        Health.Changed -= StartUpdatetingHealth;
    }

    protected virtual void Start()
    {
        StartCoroutine(UpdateHealthDisplay());
    }

    private void Update()
    {
        transform.position = reference.position;
    }

    protected void StartUpdatetingHealth()
    {
        StartCoroutine(UpdateHealthDisplay());
    }

    protected abstract IEnumerator UpdateHealthDisplay();
}
