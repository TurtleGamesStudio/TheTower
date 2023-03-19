using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PanelMover : MonoBehaviour
{
    [SerializeField] private RectTransform _upgradePanel;
    [SerializeField] private OpenPanelButtonManager _panelButtonManager;

    private float _deflection;
    private RectTransform _selfRectTransform;

    private void Awake()
    {
        _selfRectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _deflection = _upgradePanel.sizeDelta.y;

        if (_panelButtonManager.IsLastStateOpen == false)
        {
            MoveDown();
        }
    }

    private void OnEnable()
    {
        _panelButtonManager.Showed += MoveUp;
        _panelButtonManager.Hided += MoveDown;
    }

    private void OnDisable()
    {
        _panelButtonManager.Showed -= MoveUp;
        _panelButtonManager.Hided -= MoveDown;
    }

    public void MoveUp()
    {
        _selfRectTransform.anchoredPosition += Vector2.up * _deflection;
    }

    public void MoveDown()
    {
        _selfRectTransform.anchoredPosition -= Vector2.up * _deflection;
    }
}
