using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHealthDisplayer : HealthDisplayer
{
    protected Slider Slider;

    protected virtual void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    protected override void Start()
    {
        Slider.maxValue = Health.Max;
        Slider.minValue = 0;
        base.Start();
    }

    protected override IEnumerator UpdateHealthDisplay()
    {
        Slider.value = Health.Current;

        yield return null;
    }
}
