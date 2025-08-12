using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CooldownDisplayerVampireCircle : MonoBehaviour
{
    [SerializeField] private VampireCircle _vampireCircle;

    private float _delta = 1f;

    private float _startValue = 1f;
    private float _currentValue;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();

        _slider.minValue = 0f;

        _currentValue = _startValue;

        _slider.value = _startValue;

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
        _currentValue = _startValue;

        while(_currentValue  != 0)
        {
            _currentValue = Mathf.MoveTowards(_currentValue, 0, _delta * Time.deltaTime);
            _slider.value = _currentValue;
            yield return null;
        }
    }
}
