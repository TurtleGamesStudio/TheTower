using UnityEngine;
using TMPro;
using DG.Tweening;

public class RewardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _icon;
    [SerializeField] private TMP_Text _text;

    [Header("Animation")]
    [SerializeField] private float _height = 1;
    [SerializeField] private float _duration = 1;

    public void Show(int value)
    {
        _text.text = value.ToString();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + _height, _duration));
        sequence.Insert(0, _text.DOFade(0, _duration).SetEase(Ease.InQuart));
        sequence.Insert(0, _icon.DOFade(0, _duration).SetEase(Ease.InQuart));
        sequence.OnComplete(() => Destroy(gameObject));
    }
}
