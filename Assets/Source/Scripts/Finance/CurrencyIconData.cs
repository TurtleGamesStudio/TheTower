using System;
using UnityEngine;

namespace Finance
{
    [Serializable]
    public struct CurrencyIconData
    {
        [SerializeField] private Currency _currency;
        [SerializeField] private Sprite _sprite;

        public Currency Currency => _currency;
        public Sprite Sprite => _sprite;
    }
}
