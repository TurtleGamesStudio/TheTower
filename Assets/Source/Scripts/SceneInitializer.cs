using Finance;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Base _base;
    [SerializeField] private BaseView _baseView;
    [SerializeField] private AbilityUpgradeInitializer _weaponUpgradeInitializer;
    [SerializeField] private AbilityUpgradeInitializer _baseUpgradeInitializer;

    [Header("Wallets")]
    [SerializeField] private WalletInitializer _walletInitializer;
    [SerializeField] private WalletViews _walletViews;

    private void Start()
    {
        _walletInitializer.Init();
        WalletInitializer.Instance.GetWallet(Currency.Dollar).Set(80);
        _walletViews.Init();

        _weaponUpgradeInitializer.Init();
        _baseUpgradeInitializer.Init();

        _base.Init();
        _baseView.Init(_base.Weapon.Damage, _base.Regenerator.RestoringValue, _base.Health);
        _enemySpawner.Init(_base);
    }
}
