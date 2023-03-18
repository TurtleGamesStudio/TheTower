using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private RectTransform _upgradePanel;
    [SerializeField] private Transform _UIResizer;
    [SerializeField] private OpenPanelButtonManager _panelButtonManager;

    private float _deflection;

    private void Start()
    {
        _deflection = _upgradePanel.sizeDelta.y * _UIResizer.transform.localScale.y / Screen.height * 10;

        if (_panelButtonManager.IsLastStateOpen)
        {
            MoveDown();
        }
    }

    private void OnEnable()
    {
        _panelButtonManager.Showed += MoveDown;
        _panelButtonManager.Hided += MoveUp;
    }

    private void OnDisable()
    {
        _panelButtonManager.Showed -= MoveDown;
        _panelButtonManager.Hided -= MoveUp;
    }

    public void MoveUp()
    {
        transform.position += Vector3.up * _deflection;
    }

    public void MoveDown()
    {
        transform.position -= Vector3.up * _deflection;
    }
}
