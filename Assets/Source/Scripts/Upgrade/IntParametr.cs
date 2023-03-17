using UnityEngine;
using System;

[CreateAssetMenu(fileName = "IntParametr", menuName = "IntParametr")]
public class IntParametr : ScriptableObject
{
    [SerializeField, Min(0)] private int _startValue;
    [SerializeField, Min(0)] private float _upgradeStep;
    [SerializeField] private UpgradeLogic.Types _type;

    //public event Action<float> Changed;
    public event Action Upgraded;

    public int NextValue { get; private set; }

    public int Value { get; private set; }

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