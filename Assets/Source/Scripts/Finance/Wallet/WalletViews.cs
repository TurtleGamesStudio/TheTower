using UnityEngine;

namespace Finance
{
    public class WalletViews : MonoBehaviour
    {
        private WalletView[] _views;

        public void Init()
        {
            _views = GetComponentsInChildren<WalletView>(true);

            foreach (WalletView view in _views)
            {
                Wallet wallet = WalletInitializer.Instance.GetWallet(view.Currency);
                view.Init(wallet);
            }
        }
    }
}