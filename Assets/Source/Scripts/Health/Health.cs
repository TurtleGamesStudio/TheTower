using UnityEngine;
using System;

public class Health// : MonoBehaviour
{
    private FloatParametr _floatParametr;
    private float _maxValue;// => _floatParametr.Value;
    private float _value;

    public event Action Upgraded;
    public event Action<float> Changed;
    public event Action EqualToZero;

    public float Value => _value;
    public float MaxValue => _maxValue;

    public Health(FloatParametr maxHealthData)
    {
        _floatParametr = maxHealthData;
        _maxValue = _floatParametr.Value;
        _value = _maxValue;
        _floatParametr.Upgraded += OnUpgraded;
    }

    public void Increase(float value)
    {
        if (value < 0)
        {
            throw new ArgumentException($"{nameof(value)} must be non negative.");
        }

        _value = Mathf.Min(_value + value, _maxValue);
        Changed?.Invoke(_value);
    }

    public void Decrease(float damage)
    {
        if (damage < 0)
        {
            throw new ArgumentException($"{nameof(damage)} must be non negative.");
        }

        if (_value == 0)
        {
            throw new ArgumentException($"Health equal 0. You cannot decrease health more.");
        }

        if (damage >= _value)
        {
            _value = 0;
            Die();
        }
        else
        {
            _value -= damage;
            Changed?.Invoke(_value);
        }
    }

    private void Die()
    {
        EqualToZero?.Invoke();
    }

    private void OnUpgraded()
    {
        float share = _value / _maxValue;
        _maxValue = _floatParametr.Value;
        _value = _maxValue * share;
        Upgraded?.Invoke();
    }
}