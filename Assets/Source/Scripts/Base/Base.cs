using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    [SerializeField] private FloatParametr _maxHealthParametr;
    [SerializeField] private HealthRegenerator _regenerator;

    [Header("Weapon")]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private RangeView _rangeView;

    private Health _health;

    public event Action Died;

    public Health Health => _health;
    public Weapon Weapon => _weapon;
    public HealthRegenerator Regenerator => _regenerator;

    private void OnDisable()
    {
        _health.EqualToZero -= Die;
    }

    public void Init()
    {
        _weapon.Init();
        _rangeView.Init(_weapon);
        _health = new Health(_maxHealthParametr);
        _health.EqualToZero += Die;

        _regenerator.Init(_health);
        _regenerator.StartHeal();
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
