using UnityEngine;
using TMPro;

namespace Finance
{
    /// <summary>
    ///Must be disabled before using
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class PriceView : MonoBehaviour
    {
        private Price _price;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            OnChanged(_price.Value);
            _price.Changed += OnChanged;
            _price.Setted += OnChanged;
        }

        private void OnDisable()
        {
            _price.Changed -= OnChanged;
            _price.Setted -= OnChanged;
        }

        public void Init(Price price)
        {
            _price = price;
            enabled = true;
        }

        private void OnChanged(int value)
        {
            _text.text = "$ " + value.ToString();
        }
    }
}
