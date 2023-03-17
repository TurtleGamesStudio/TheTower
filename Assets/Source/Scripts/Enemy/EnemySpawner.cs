using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _radius;
    //[SerializeField] private float _spawnInterval;
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private FloatParametr _spawnInterval;
    [SerializeField] private LevelOfUpgrade _levelOfUpgrade;
    [SerializeField] private float _difficultIncreaseInterval = 5;
    [SerializeField] private float _maxShareOfTimeDeflection;

    private const string _waveNumberKey = "WaveNumber";

    [SerializeField] private Wave[] _waves;

    private Base _base;
    private int _waveNumber;
    private List<Enemy> _enemies;
    private Quaternion _startRotation;
    private Coroutine _spawning;

    public event Action<Enemy> Died;

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Died -= OnDied;
        }

        _base.Died -= OnBaseDied;
        _levelOfUpgrade.Changed -= OnLevelChanged;
    }

    public void Init(Base @base)
    {
        _startRotation = Quaternion.Euler(0, -90, 0);
        _base = @base;
        _enemies = new List<Enemy>();
        _waveNumber = PlayerPrefs.GetInt(_waveNumberKey, 0);

        _levelOfUpgrade.Init(Id.SpawnInterval);
        _spawnInterval.Init(_levelOfUpgrade.Current);
        _levelOfUpgrade.Changed += OnLevelChanged;


        //Spawn();
        StartCoroutine(IncreaseDifficult());
        _spawning = StartCoroutine(Spawning());
        _base.Died += OnBaseDied;
    }

    private void OnLevelChanged(int value)
    {
        _spawnInterval.UpgradeToLevel(value);
    }

    private IEnumerator IncreaseDifficult()
    {
        WaitForSeconds waiting = new WaitForSeconds(_difficultIncreaseInterval);

        while (_levelOfUpgrade.Current != _levelOfUpgrade.Max)
        {
            _levelOfUpgrade.Upgrade();
            yield return waiting;
        }
    }

    private IEnumerator Spawning()
    {
        float time = 0;
        float spawnInterval = GetSpawnTime();

        while (true)
        {
            time += Time.deltaTime;

            if (time > spawnInterval)
            {
                Vector2 direction = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector2.right;
                Enemy enemy = Instantiate(_enemyTemplate, (Vector2)_base.transform.position + direction * _radius, Quaternion.identity);
                enemy.Init(_base.transform);
                enemy.Died += OnDied;
                spawnInterval = GetSpawnTime();
                time -= spawnInterval;
            }

            yield return null;
        }
    }

    private float GetSpawnTime()
    {
        return _spawnInterval.Value * Random.Range(1 - _maxShareOfTimeDeflection, 1 + _maxShareOfTimeDeflection);
    }

    private void Spawn()
    {
        Wave wave = _waves[_waveNumber];

        foreach (SpawnData spawnData in wave.SpawnDatas)
        {
            Vector2 direction = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector2.right;
            Enemy enemy = Instantiate(spawnData.Enemy, (Vector2)_base.transform.position + direction * _radius, _startRotation);
            _enemies.Add(enemy);
            enemy.Init(_base.transform);
            enemy.Died += OnDied;
        }
    }

    private void OnDied(Enemy enemy)
    {
        enemy.Died -= OnDied;
        _enemies.Remove(enemy);
        Died?.Invoke(enemy);

        //if (_enemies.Count == 0)
        //{
        //    if (_waveNumber != _waves.Length - 1)
        //    {
        //        _waveNumber++;
        //    }

        //    Spawn();
        //}
    }

    private void OnBaseDied()
    {
        StopCoroutine(_spawning);

        foreach (Enemy enemy in _enemies)
        {
            enemy.Stop();
        }
    }
}
