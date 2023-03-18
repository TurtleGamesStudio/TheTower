using System.Collections.Generic;
using UnityEngine;
using System;

namespace Finance
{
    [CreateAssetMenu(fileName = "CurrencyIconsDatabase", menuName = "CurrencyIconsDatabase", order = 1000)]
    public class CurrencyIconsDatabase : ScriptableObject
    {
        [SerializeField] private List<CurrencyIconData> _datas;

        public Sprite GetIcon(Currency currency)
        {
            foreach (CurrencyIconData data in _datas)
                if (data.Currency == currency)
                    return data.Sprite;

            throw new NotImplementedException(nameof(currency));
        }
    }
}
