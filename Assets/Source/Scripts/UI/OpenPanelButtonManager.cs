using UnityEngine;
using System;

public class OpenPanelButtonManager : MonoBehaviour
{
    [SerializeField] private OpenPanelButton[] _buttons;

    private OpenPanelButton _previousButton;
    private bool _isLastStateOpen;

    public event Action Hided;
    public event Action Showed;

    public bool IsLastStateOpen => _isLastStateOpen;

    private void Awake()
    {
        _buttons[0].Open();
        _previousButton = _buttons[0];

        for (int i = 1; i < _buttons.Length; i++)
            _buttons[i].Close();

        _isLastStateOpen = true;
    }

    private void OnEnable()
    {
        foreach (var button in _buttons)
        {
            button.Clicked += OnClick;
        }
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            button.Clicked -= OnClick;
        }
    }

    private void OnClick(OpenPanelButton button)
    {
        if (_previousButton != null && button != _previousButton)
        {
            _previousButton.Close();
        }

        _previousButton = button;

        if (_isLastStateOpen)
        {
            if (button.IsPanelActive == false)
                Hided?.Invoke();
        }
        else
        {
            if (button.IsPanelActive)
                Showed?.Invoke();
        }

        _isLastStateOpen = button.IsPanelActive;
    }
}
