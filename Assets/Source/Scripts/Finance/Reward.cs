using Finance;
using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private Currency _currency;
    [SerializeField] private int _value;

    public Currency Currency => _currency;
    public int Value => _value;
}
