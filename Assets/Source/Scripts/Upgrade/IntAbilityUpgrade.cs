using UnityEngine;
using Finance;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(LevelOfUpgrade))]
public class IntAbilityUpgrade : MonoBehaviour
{
    [SerializeField] private UpgradeViewData _upgradeViewData;
    [SerializeField] private IntParametr _intParametr;
    [SerializeField] private Id _id;

    [Header("Price")]
    [SerializeField] private IntParametr _priceUpgradeSettings;
    [SerializeField] private Currency _currency;

    private Button _button;
    private GameObject _max;
    private LevelOfUpgrade _levelOfUpgrade;
    private Wallet _wallet;
    private UpgradablePrice _upgradablePrice;

    public IntParametr IntParametr => _intParametr;
    public LevelOfUpgrade LevelOfUpgrade => _levelOfUpgrade;
    public Price Price => _upgradablePrice.Price;
    public UpgradeViewData UpgradeViewData => _upgradeViewData;

    public event Action<float> Upgraded;
    public event Action MaxReached;

    public void Init(ContainerInstantiator containerInstantiator, IntUpgradePanel template)
    {
        _levelOfUpgrade = GetComponent<LevelOfUpgrade>();
        _wallet = WalletInitializer.Instance.GetWallet(_currency);
        IntUpgradePanel upgradePanel = Instantiate(template, containerInstantiator.GetContainer());

        _button = upgradePanel.Button;
        _max = upgradePanel.Max;

        _levelOfUpgrade.MaxLevelReached += OnMaxLevelReached;
        _levelOfUpgrade.Init(_id);

        _upgradablePrice = new UpgradablePrice(_levelOfUpgrade.Current, _currency, _id.ToString(), _priceUpgradeSettings);
        _intParametr.Init(_levelOfUpgrade.Current);
        Unlock();


        _wallet.BalanceChanged += OnBalanceChanged;
        _button.onClick.AddListener(Upgrade);

        upgradePanel.Init(this);

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
        _intParametr.UpgradeToLevel(_levelOfUpgrade.Current);
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