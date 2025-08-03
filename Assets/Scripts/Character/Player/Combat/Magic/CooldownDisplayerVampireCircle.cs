using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CooldownDisplayerVampireCircle : MonoBehaviour
{
    [SerializeField] private VampireCircle _vampireCircle;
    [SerializeField] private float _delta = 10f;

    private float _startValue;
    private float _currentValue;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        
        _slider.minValue = 0f;
        _startValue = _vampireCircle.LifeDrainCooldown;
        _slider.maxValue = _startValue;

        _currentValue = _startValue;

        
        _slider.value = _startValue;
    }

    private void OnEnable()
    {
        _vampireCircle.StartedDrainLife += StartShowingCooldown;
    }

    private void OnDisable()
    {
        _vampireCircle.StartedDrainLife -= StartShowingCooldown;    
    }

    private void StartShowingCooldown()
    {
        StartCoroutine(ShowCooldown());
    }

    private IEnumerator ShowCooldown()
    {
        _currentValue = _startValue;

        while(_slider.value != _vampireCircle.LifeDrainCooldown)
        {
            _slider.value -= Mathf.MoveTowards(_currentValue, _vampireCircle.LifeDrainCooldown, _delta);
            _slider.value = _currentValue;
            yield return null;
        }
    }
}
