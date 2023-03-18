using UnityEngine;
using System;

namespace Finance
{
    public class Price
    {
        private Currency _currency;
        private string _id;
        private int _value;

        public event Action<int> Changed;
        public event Action<int> Setted;
        //public event Action<string, string, int> Purchased;

        public int Value => _value;
        public Currency Currency => _currency;

        public Price(Currency currency, string purchaseObjectName, int value)
        {
            _currency = currency;
            _id = purchaseObjectName + "_" + currency;
            _value = PlayerPrefs.GetInt(_id, value);
        }

        public void Set(int value)
        {
            _value = value;
            //Save(_value);
            Setted?.Invoke(_value);
        }

        public void Decrease(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(value)} must be non negative.");
            }

            if (value > _value)
            {
                throw new ArgumentException($"{nameof(value)} must be not more than price.");
            }

            _value -= value;

            //Save(_value);
            Changed?.Invoke(_value);
        }

        private void Save(int value)
        {
            PlayerPrefs.SetInt(_id, value);

            if (value == 0)
            {
                //Purchased?.Invoke(_type, _name, _startValue);     //_type,_name, _value
            }
        }
    }
}