using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

namespace Finance
{
    public class WalletInitializer : MonoBehaviour
    {
        [SerializeField] private Currency[] _currencies;

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
                    _wallets[i] = new Wallet(_currencies[i]);
                }
            }
        }

        public Wallet GetWallet(Currency currency)
        {
            return _wallets.First(targetWallet => targetWallet.Currency == currency);
        }
    }
}
