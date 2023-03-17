using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private RectTransform _upgradePanel;
    [SerializeField] private Transform _UIResizer;

    private float _deflection;

    private void Start()
    {
        _deflection = _upgradePanel.sizeDelta.y * _UIResizer.transform.localScale.y / Screen.height * 10;
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
