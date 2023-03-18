using Finance;
using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{
    [Header("Wallets")]
    [SerializeField] private WalletInitializer _walletInitializer;
    [SerializeField] private WalletViews _walletViews;

    [SerializeField] private AbilityUpgradeInitializer _weaponUpgradeInitializer;
    [SerializeField] private AbilityUpgradeInitializer _baseUpgradeInitializer;

    private void Start()
    {
        _walletInitializer.Init();
        _walletViews.Init();

        _weaponUpgradeInitializer.Init();
        _baseUpgradeInitializer.Init();
    }
}
