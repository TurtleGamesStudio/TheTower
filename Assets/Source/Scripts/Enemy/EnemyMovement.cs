using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private Rigidbody2D _rigidbody;
    private Coroutine _moving;

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Transform target)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(Moving(target));
    }

    public void Stop()
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _rigidbody.velocity = Vector2.zero;
    }

    private IEnumerator Moving(Transform target)
    {
        while (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            _rigidbody.velocity = _speed * direction;
            yield return null;
        }
    }
}
