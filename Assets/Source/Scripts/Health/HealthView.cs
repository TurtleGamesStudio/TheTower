using UnityEngine;
using TMPro;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private TMP_Text _maxValue;

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
        _value.text = ((int)value).ToString();// value.ToString("N");
    }

    private void OnUpgraded()
    {
        _maxValue.text = ((int)_health.MaxValue).ToString(); //_health.MaxValue.ToString();
        OnChanged(_health.Value);
    }
}
