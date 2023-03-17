using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FloatParametr", menuName = "FloatParametr")]
public class FloatParametr : ScriptableObject
{
    [SerializeField, Min(0)] private float _startValue;
    [SerializeField] private float _upgradeStep;
    [SerializeField] private UpgradeLogic.Types _type;

    //public event Action<float> Changed;
    public event Action Upgraded;

    public float NextValue { get; private set; }

    public float Value { get; private set; }

    public void Init(int level)
    {
        UpgradeToLevel(level);
    }

    public void UpgradeToLevel(int level)
    {
        Value = UpgradeLogic.CalculateValue(_startValue, level, _upgradeStep, _type);
        NextValue = UpgradeLogic.CalculateValue(_startValue, level + 1, _upgradeStep, _type);
        Upgraded?.Invoke();
    }
}