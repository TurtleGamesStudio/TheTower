using UnityEngine;
using Finance;
using UnityEngine.UI;
using System;

public class MainMenuAbility : MonoBehaviour, IAbilityUpgrade
{
    [SerializeField] private LevelOfUpgrade _levelOfUpgrade;
    [SerializeField] private UpgradeViewData _upgradeViewData;
    [SerializeField] private FloatParametr _floatParametr;
    [SerializeField] private Id _id;

    [Header("Price")]
    [SerializeField] private IntParametr _priceUpgradeSettings;
    [SerializeField] private Currency _currency;

    private Button _button;
    private GameObject _max;
    private Wallet _wallet;
    private UpgradablePrice _upgradablePrice;

    public FloatParametr FloatParametr => _floatParametr;
    public LevelOfUpgrade LevelOfUpgrade => _levelOfUpgrade;
    public Price Price => _upgradablePrice.Price;
    public UpgradeViewData UpgradeViewData => _upgradeViewData;

    public event Action<float> Upgraded;
    public event Action MaxReached;

    public void Init(ContainerInstantiator containerInstantiator, UpgradePanel template)
    {
        _wallet = WalletInitializer.Instance.GetWallet(_currency);
        UpgradePanel upgradePanel = Instantiate(template, containerInstantiator.GetContainer());

        _button = upgradePanel.Button;
        _max = upgradePanel.Max;

        _levelOfUpgrade.MaxLevelReached += OnMaxLevelReached;
        _levelOfUpgrade.Init(_id);

        _upgradablePrice = new UpgradablePrice(_levelOfUpgrade.Current, _currency, _id.ToString(), _priceUpgradeSettings);
        _floatParametr.Init(_levelOfUpgrade.Current);
        Unlock();


        _wallet.BalanceChanged += OnBalanceChanged;
        _button.onClick.AddListener(Upgrade);

        upgradePanel.Init(_floatParametr, _upgradablePrice.Price, _upgradeViewData.Description);

        if (_levelOfUpgrade.Current == _levelOfUpgrade.Max)
        {
            OnMaxLevelReached();
        }
    }

    private void OnDisable()
    {
        _levelOfUpgrade.MaxLevelReached -= OnMaxLevelReached;
        _wallet.BalanceChanged -= OnBalanceChanged;
        _button.onClick.RemoveListener(Upgrade);
    }

    public void Upgrade()
    {
        UpgradeToLevel(_levelOfUpgrade.Current + 1);
    }

    private void UpgradeToLevel(int level)
    {
        _levelOfUpgrade.UpgradeToLevel(level);
        int expendeture = Price.Value;
        _upgradablePrice.Upgrade(level);
        _wallet.Withdraw(expendeture);
        _floatParametr.UpgradeToLevel(_levelOfUpgrade.Current);

        _levelOfUpgrade.Save();
        //_wallet.Save();

        Upgraded?.Invoke(_levelOfUpgrade.Current);
    }

    private void OnMaxLevelReached()
    {
        _button.gameObject.SetActive(false);
        _max.gameObject.SetActive(true);
        MaxReached?.Invoke();
        //enabled = false;
    }

    private void OnBalanceChanged(int _)
    {
        Unlock();
    }

    private void Unlock()
    {
        _button.interactable = Price.Value <= _wallet.Value;
    }
}