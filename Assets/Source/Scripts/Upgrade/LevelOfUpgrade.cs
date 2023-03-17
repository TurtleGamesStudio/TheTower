using System;
using UnityEngine;

public class LevelOfUpgrade : MonoBehaviour
{
    [SerializeField, Min(1)] private int _max;

    private Id _id;
    private int _current;

    public event Action<int> Changed;
    public event Action MaxLevelReached;

    public int Current => _current;
    public int Max => _max;

    public void Init(Id id)
    {
        _id = id;
        _current = PlayerPrefs.GetInt(_id.ToString(), 0);
    }

    public void Upgrade()
    {
        UpgradeToLevel(_current + 1);
    }

    public void UpgradeToLevel(int level)
    {
        if (level <= _current)
        {
            throw new ArgumentException("Inputing level must be more than current", nameof(level));
        }

        if (level > _max)
        {
            throw new ArgumentException("Inputing level more than max", nameof(level));
        }

        _current = level;

        //PlayerPrefs.SetInt(_id.ToString(), _current);
        Changed?.Invoke(_current);

        if (_current == _max)
        {
            MaxLevelReached?.Invoke();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt(_id.ToString(), _current);
    }
}