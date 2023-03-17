using Finance;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Base _base;
    [SerializeField] private AbilityUpgradeInitializer _upgradeInitializer;

    [Header("Wallets")]
    [SerializeField] private WalletInitializer _walletInitializer;
    [SerializeField] private WalletViews _walletViews;

    private void Start()
    {
        _walletInitializer.Init();
        WalletInitializer.Instance.GetWallet(Currency.Dollar).Set(80);
        _walletViews.Init();

        _upgradeInitializer.Init();

        _base.Init();
        _enemySpawner.Init(_base);
    }
}
