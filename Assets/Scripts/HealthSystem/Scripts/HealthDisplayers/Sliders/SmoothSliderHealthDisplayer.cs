using System.Collections;
using UnityEngine;

public class SmoothSliderHealthDisplayer : SliderHealthDisplayer
{
    [SerializeField] private float _healthDelta = 100;
    
    private float _displayedHealth;

    protected override void Start()
    {
        base.Start();

        _displayedHealth = Health.Current;
        Slider.value = _displayedHealth;
    }

    protected override IEnumerator UpdateHealthDisplay()
    {
        while (_displayedHealth != Health.Current)
        {
            _displayedHealth = Mathf.MoveTowards(_displayedHealth, Health.Current, _healthDelta *Time.deltaTime);
            Slider.value = _displayedHealth;
            yield return null;
        }

    }
}
