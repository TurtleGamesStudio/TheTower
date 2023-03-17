using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class IntParametrView : MonoBehaviour
{
    private TMP_Text _text;
    private IntParametr _intParametr;

    private void OnDisable()
    {
        _intParametr.Upgraded -= OnUpgraded;
    }

    public void Init(IntParametr intParametr)
    {
        _text = GetComponent<TMP_Text>();
        _intParametr = intParametr;
        _intParametr.Upgraded += OnUpgraded;
        OnUpgraded();
    }

    private void OnUpgraded()
    {
        _text.text = _intParametr.Value.ToString();
    }
}