using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private FloatParametr _maxHealthParametr;
    [SerializeField] private float _damage = 1;

    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private List<Reward> _rewardList;

    private EnemyMovement _enemyMovement;
    private Health _health;

    public event Action<Enemy> Died;

    public IReadOnlyList<Reward> RewardList => _rewardList;
    public ParticleSystem Explosion => _explosion;

    private void OnDisable()
    {
        _health.EqualToZero -= Die;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Base @base))
        {
            @base.TakeDamage(_damage);
        }
    }

    public void Init(Transform target)
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyMovement.Init();
        _enemyMovement.Move(target);
        _maxHealthParametr.Init(0);
        _health = new Health(_maxHealthParametr);
        _health.EqualToZero += Die;
    }

    public void TakeDamage(float damage)
    {
        _health.Decrease(damage);
    }

    public void Stop()
    {
        _enemyMovement.Stop();
    }

    private void Die()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
