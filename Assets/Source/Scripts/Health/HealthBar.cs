using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private Health _health;

    private void OnDisable()
    {
        _health.Changed -= OnChanged;
        _health.Upgraded -= OnUpgraded;
    }

    public void Init(Health health)
    {
        _health = health;
        OnUpgraded();
        _health.Changed += OnChanged;
        _health.Upgraded += OnUpgraded;
    }

    private void OnChanged(float value)
    {
        _slider.value = _health.Value/_health.MaxValue;
    }

    private void OnUpgraded()
    {
        _slider.value = _health.Value / _health.MaxValue;
    }
}
