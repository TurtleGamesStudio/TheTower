using Finance;
using System;
using UnityEngine;

[Serializable]
public struct Reward
{
    [SerializeField] private Currency _currency;
    [SerializeField] private int _value;

    public Currency Currency => _currency;
    public int Value => _value;
}
