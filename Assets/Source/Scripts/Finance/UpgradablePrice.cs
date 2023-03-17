namespace Finance
{
    public class UpgradablePrice
    {
        private IntParametr _upgradeSettings;
        private Price _price;

        public Price Price => _price;

        public UpgradablePrice(int level, Currency currency, string upgradeName, IntParametr upgradeSettings)
        {
            _upgradeSettings = upgradeSettings;
            _upgradeSettings.Init(level);
            _price = new Price(currency, upgradeName, _upgradeSettings.Value);
        }

        public void Upgrade(int level)
        {
            _upgradeSettings.UpgradeToLevel(level);
            _price.Set(_upgradeSettings.Value);
        }
    }
}
