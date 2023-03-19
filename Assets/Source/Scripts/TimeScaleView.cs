using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TimeScaleView : MonoBehaviour
{
    [SerializeField] private GameSpeed _gameSpeed;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        OnSpeedChanged(Time.timeScale);
        _gameSpeed.Changed += OnSpeedChanged;
    }

    private void OnDisable()
    {
        _gameSpeed.Changed -= OnSpeedChanged;
    }

    private void OnSpeedChanged(float value)
    {
        _text.text = "x" + value.ToString("F1");
    }
}
