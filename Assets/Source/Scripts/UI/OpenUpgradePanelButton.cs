using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class OpenUpgradePanelButton : MonoBehaviour
{
    [SerializeField] private RectTransform _targetPanel;

    private Button _button;
    public bool IsPanelActive { get; private set; }

    public event Action<OpenUpgradePanelButton> Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (IsPanelActive)
            Close();
        else
            Open();

        Clicked?.Invoke(this);
    }

    public void Open()
    {
        _targetPanel.gameObject.SetActive(true);
        IsPanelActive = true;
    }

    public void Close()
    {
        _targetPanel.gameObject.SetActive(false);
        IsPanelActive = false;
    }
}
