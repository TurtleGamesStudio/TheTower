using UnityEngine;

public class OpenUpgradePanelButtonManager : MonoBehaviour
{
    [SerializeField] private OpenUpgradePanelButton[] _buttons;
    [SerializeField] private CameraMover _cameraMover;

    private OpenUpgradePanelButton _previousButton;
    private bool _isLastStateOpen;

    private void Awake()
    {
        foreach (var button in _buttons)
        {
            button.Close();
        }

        _isLastStateOpen = false;
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

    private void OnClick(OpenUpgradePanelButton button)
    {
        if (_previousButton != null && button != _previousButton)
        {
            _previousButton.Close();
        }

        _previousButton = button;

        if (_isLastStateOpen)
        {
            if (button.IsPanelActive == false)
                _cameraMover.MoveUp();
        }
        else
        {
            if (button.IsPanelActive)
                _cameraMover.MoveDown();
        }

        _isLastStateOpen = button.IsPanelActive;
    }
}
