using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 20f;

    private Rigidbody2D _rigidbody;
    private Coroutine _moving;
    private float _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void Init(float damage)
    {
        _damage = damage;
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Die());
    }

    public void Move()
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(Moving());
    }

    public void Stop()
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }
    }

    private IEnumerator Moving()
    {
        while (true)
        {
            _rigidbody.velocity = _speed * transform.up;
            yield return null;
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
