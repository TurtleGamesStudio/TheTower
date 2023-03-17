using System.Collections;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private float _lifeTime = 2f;

    private WaitForSeconds _waiting;

    private void Awake()
    {
        _waiting = new WaitForSeconds(_lifeTime);
    }

    private void OnEnable()
    {
        _enemySpawner.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemySpawner.Died -= OnDied;
    }

    private void OnDied(Enemy enemy)
    {
        Explode(enemy.Explosion, enemy.transform.position);
    }

    private void Explode(ParticleSystem template, Vector3 position)
    {
        ParticleSystem particleSystem = Instantiate(template, position, Quaternion.identity);
        StartCoroutine(Die(particleSystem));
    }

    private IEnumerator Die(ParticleSystem particleSystem)
    {
        yield return _waiting;
        Destroy(particleSystem.gameObject);
    }
}
