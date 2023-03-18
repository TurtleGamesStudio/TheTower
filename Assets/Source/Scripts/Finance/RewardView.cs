using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Finance;

public class RewardView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;

    [Header("Animation")]
    [SerializeField] private float _height = 1;
    [SerializeField] private float _duration = 1;

    public void Show(Reward reward)//(int value)
    {
        _text.text = reward.Value.ToString();
        _icon.sprite = WalletInitializer.Instance.GetSprite(reward.Currency);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + _height, _duration));
        sequence.Insert(0, _text.DOFade(0, _duration).SetEase(Ease.InQuart));
        sequence.Insert(0, _icon.DOFade(0, _duration).SetEase(Ease.InQuart));
        sequence.OnComplete(() => Destroy(gameObject));
    }
}
