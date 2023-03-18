using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Finance
{
    /// <summary>
    ///Must be disabled before using
    /// </summary>
    public class PriceView : MonoBehaviour
    {
        [SerializeField] private RectTransform _textRectTransform;
        [SerializeField] private float _charWidth = 27.6f;
        [SerializeField] private int _maxSteps = 3;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;

        private Price _price;

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
            _icon.sprite = WalletInitializer.Instance.GetSprite(price.Currency);
            enabled = true;
        }

        private void OnChanged(int value)
        {
            _text.text = value.ToString();

            int rank = MyMathf.GetRank(value);
            int steps = Mathf.Min(rank, _maxSteps);
            float width = _charWidth * steps;

            _textRectTransform.sizeDelta = new Vector2(width, _textRectTransform.sizeDelta.y);
        }
    }
}
