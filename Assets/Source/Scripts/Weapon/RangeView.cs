using System.Collections;
using UnityEngine;

public class RangeView : MonoBehaviour
{
    [SerializeField] private float _speed = 1000f;

    private Weapon _weapon;
    private Coroutine _changing;

    private void OnDisable()
    {
        _weapon.RangeChanged -= OnRangeChanged;
    }

    public void Init(Weapon weapon)
    {
        _weapon = weapon;
        transform.localScale = Vector2.one * _weapon.Range;
        _weapon.RangeChanged += OnRangeChanged;
    }

    private void OnRangeChanged(float target)
    {
        if (_changing != null)
        {
            StopCoroutine(_changing);
        }

        _changing = StartCoroutine(Changing(target));
    }

    private IEnumerator Changing(float target)
    {
        while (transform.localScale.x != target)
        {
            transform.localScale = Vector2.one * Mathf.MoveTowards(transform.localScale.x, target, _speed * Time.deltaTime);
            yield return null;
        }
    }
}
