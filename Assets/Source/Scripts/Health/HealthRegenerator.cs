using System.Collections;
using UnityEngine;

public class HealthRegenerator : MonoBehaviour
{
    [SerializeField] private float _healInterval = 1.0f;
    [SerializeField] private FloatParametr _restoringValue;

    private Health _health;
    private Coroutine _healing;
    private WaitForSeconds _waiting;

    public FloatParametr RestoringValue => _restoringValue;

    public void Init(Health health)
    {
        _health = health;
        _waiting = new WaitForSeconds(_healInterval);
    }

    public void StartHeal()
    {
        if (_healing != null)
            StopCoroutine(_healing);

        _healing = StartCoroutine(Healing());
    }

    public void StopHeal()
    {
        if (_healing != null)
            StopCoroutine(_healing);
    }

    private IEnumerator Healing()
    {
        while (_health.Value != 0)
        {
            if (_health.Value != _health.MaxValue)
            {
                _health.Increase(_restoringValue.Value);
            }

            yield return _waiting;
        }
    }
}
