using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    [SerializeField] private FloatParametr _maxHealthParametr;

    [Header("Weapon")]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private RangeView _rangeView;

    private Health _health;

    public event Action Died;

    private void OnDisable()
    {
        _health.EqualToZero -= Die;
    }

    public void Init()
    {
        _weapon.Init();
        _rangeView.Init(_weapon);
        _maxHealthParametr.Init(0);
        _health = new Health(_maxHealthParametr);
        _health.EqualToZero += Die;
    }

    public void TakeDamage(float damage)
    {
        _health.Decrease(damage);
    }

    private void Die()
    {
        Died?.Invoke();
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
