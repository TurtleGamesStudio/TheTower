using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class NextFloatParametrView : MonoBehaviour
{
    private TMP_Text _text;
    private FloatParametr _floatParametr;

    private void OnDisable()
    {
        _floatParametr.Upgraded -= OnUpgraded;
    }

    public void Init(FloatParametr floatParametr)
    {
        _text = GetComponent<TMP_Text>();
        _floatParametr = floatParametr;
        _floatParametr.Upgraded += OnUpgraded;
        OnUpgraded();
    }

    private void OnUpgraded()
    {
        _text.text = _floatParametr.NextValue.ToString();
    }
}