using System.Linq;
using UnityEngine;

namespace Finance
{
    public class WalletInitializer : MonoBehaviour
    {
        [SerializeField] private Currency[] _currencies;
        [SerializeField] private CurrencyIconsDatabase _currenciesDatabase;

        private Wallet[] _wallets;
        public static WalletInitializer Instance;

        public void Init()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;

                _wallets = new Wallet[_currencies.Length];

                for (int i = 0; i < _currencies.Length; i++)
                {
                    if (_currencies[i] == Currency.Dollar)
                        _wallets[i] = new Wallet(_currencies[i]);
                    else
                        _wallets[i] = new SavableWallet(_currencies[i]);
                }
            }
        }

        public Wallet GetWallet(Currency currency)
        {
            return _wallets.First(targetWallet => targetWallet.Currency == currency);
        }

        public Sprite GetSprite(Currency currency)
        {
            return _currenciesDatabase.GetIcon(currency);
        }
    }
}
