namespace Finance
{
    public class SavableWallet :Wallet
    {
        public SavableWallet(Currency currency) : base(currency)
        {
        }

        protected override void OnChanged()
        {
            Save();
        }
    }
}