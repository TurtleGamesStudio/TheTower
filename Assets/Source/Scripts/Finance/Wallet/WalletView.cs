using UnityEngine;
using TMPro;
using System.Collections;

namespace Finance
{
    [RequireComponent(typeof(TMP_Text))]
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private Currency _currency;

        [SerializeField] private AnimationCurve _countDurationDependency;

        private TMP_Text _text;
        private int _value;
        private Coroutine _valueChangingCoroutine;
        private Wallet _wallet;

        public Currency Currency => _currency;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Init(Wallet wallet)
        {
            _wallet = wallet;
            _value = wallet.Value;
            _text.text = _value.ToString();
            wallet.BalanceChanged += OnBalanceChanged;
        }

        private void OnDisable()
        {
            if (_valueChangingCoroutine != null)
            {
                StopCoroutine(_valueChangingCoroutine);
            }

            _wallet.BalanceChanged -= OnBalanceChanged;
        }

        private void SetValue(int value)
        {
            _value = value;
            _text.text = _value.ToString();
        }

        private void OnBalanceChanged(int target)
        {
            if (_valueChangingCoroutine != null)
            {
                StopCoroutine(_valueChangingCoroutine);
            }

            _valueChangingCoroutine = StartCoroutine(Change(target));
        }

        private IEnumerator Change(int target)
        {
            int startValue = _value;
            int difference = target - _value;
            int distance = Mathf.Abs(target - _value);

            float animationTime = _countDurationDependency.Evaluate(distance);

            float time = 0;

            while (time < animationTime)
            {
                time += Time.deltaTime;
                float progress = time / animationTime;
                _value = startValue + MyMathf.Evaluate(difference, progress);
                _text.text = _value.ToString();

                yield return null;
            }
        }
    }
}