using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

[RequireComponent(typeof(CircleCollider2D))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private FloatParametr _range;
    [SerializeField] private FloatParametr _damage;
    [SerializeField] private FloatParametr _reloadTime;
    [SerializeField] private Bullet _bulletTemplate;

    private Coroutine _shooting;
    private List<Enemy> _enemies;
    private CircleCollider2D _circleCollider;

    public event Action<float> RangeChanged;

    public bool CanShoot { get; private set; }
    public float Range => _range.Value;

    private void OnDisable()
    {
        _range.Upgraded -= OnRangeUpgraded;

        foreach (Enemy enemy in _enemies)
            enemy.Died -= OnDied;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _enemies.Add(enemy);
            enemy.Died += OnDied;

            if (_enemies.Count == 1)
            {
                StartShooting();
            }
        }
    }

    public void Init()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _range.Upgraded += OnRangeUpgraded;
        _circleCollider.radius = _range.Value;
        _enemies = new List<Enemy>();
        CanShoot = true;
    }

    public void StartShooting()
    {
        if (_shooting != null)
        {
            StopCoroutine(_shooting);
        }

        _shooting = StartCoroutine(Shooting());
    }

    public void StopShooting()
    {
        if (_shooting != null)
        {
            StopCoroutine(_shooting);
        }
    }

    private IEnumerator Shooting()
    {
        while (_enemies.Count != 0)
        {
            if (CanShoot)
            {
                Enemy target = _enemies.First();
                Vector2 direction = (Vector2)target.transform.position - (Vector2)transform.position;
                Shoot(direction);
            }

            yield return null;
        }
    }

    private IEnumerator Reloading()
    {
        float time = 0;

        while (time < _reloadTime.Value)
        {
            time += Time.deltaTime;
            yield return null;
        }

        CanShoot = true;
    }

    private void Shoot(Vector2 direction)
    {
        Bullet bullet = Instantiate(_bulletTemplate, transform.position, Quaternion.LookRotation(Vector3.forward, direction));
        bullet.Init(_damage.Value);
        bullet.Move();

        CanShoot = false;
        StartCoroutine(Reloading());
    }

    private void OnRangeUpgraded()
    {
        _circleCollider.radius = _range.Value;
        RangeChanged?.Invoke(Range);
    }

    private void OnDied(Enemy enemy)
    {
        enemy.Died -= OnDied;
        _enemies.Remove(enemy);
    }
}
