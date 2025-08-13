using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VampireCooldownDisplayer : MonoBehaviour
{
    [SerializeField] private VampireCooldownListener _listener;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _listener.UpdatedCooldown += CooldownUIUpdater;
    }

    private void OnDisable()
    {
        _listener.UpdatedCooldown -= CooldownUIUpdater;
    }

    private void CooldownUIUpdater()
    {
        _slider.value = _listener.CurrentValue;
    }
}
