using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class LevelView : MonoBehaviour
{
    private TMP_Text _text;
    private LevelOfUpgrade _levelOfUpgrade;

    private void OnDisable()
    {
        _levelOfUpgrade.Changed -= OnChanged;
    }

    public void Init(LevelOfUpgrade levelOfUpgrade)
    {
        _text = GetComponent<TMP_Text>();
        _levelOfUpgrade = levelOfUpgrade;
        _levelOfUpgrade.Changed += OnChanged;
        OnChanged(_levelOfUpgrade.Current);
    }

    private void OnChanged(int value)
    {
        _text.text = "Level " + value.ToString();
    }
}
