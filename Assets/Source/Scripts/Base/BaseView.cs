using UnityEngine;

public class BaseView : MonoBehaviour
{
    [SerializeField] private FloatParametrView _attackView;
    [SerializeField] private HealthRegenerationView _healthRegenerationView;

    [SerializeField] private HealthView _healthView;
    [SerializeField] private HealthBar _healthBar;

    public void Init(FloatParametr attackParametr, FloatParametr regenerationParametr, Health health)
    {
        _attackView.Init(attackParametr);
        _healthRegenerationView.Init(regenerationParametr);

        _healthView.Init(health);
        _healthBar.Init(health);
    }
}
