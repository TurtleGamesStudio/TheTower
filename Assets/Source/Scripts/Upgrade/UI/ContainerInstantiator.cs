using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerInstantiator : MonoBehaviour
{
    [SerializeField] private RectTransform _selfRectTransform;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    [SerializeField] private Container _containerTemplate;
    [SerializeField, Min(1)] private int _capacity = 1;

    private int _requestsCount;
    private int _containersCount;
    private Container _lastContainer;
    private IReadOnlyList<Transform> _slots;

    public Transform GetContainer()
    {
        if (_requestsCount % _capacity == 0)
        {
            _lastContainer = Instantiate(_containerTemplate, transform);
            _slots = _lastContainer.CreateSlots(_capacity);
            _containersCount++;
        }

        int index = _requestsCount % _capacity;
        Transform targetTransform = _slots[index];

        _requestsCount++;

        return targetTransform;
    }

    public void UpdateHeight()
    {
        float containerHeight = _lastContainer.GetComponent<RectTransform>().sizeDelta.y;
        float height = _verticalLayoutGroup.padding.vertical + _containersCount * containerHeight + (_containersCount - 1) * _verticalLayoutGroup.spacing;
        _selfRectTransform.sizeDelta = new Vector2(_selfRectTransform.sizeDelta.x, height);
        _selfRectTransform.anchoredPosition = new Vector2(_selfRectTransform.anchoredPosition.x, -height / 2);
    }
}
