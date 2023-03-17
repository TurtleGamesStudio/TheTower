using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ModelResizer : MonoBehaviour
{
    [SerializeField] private float _scale;
    [SerializeField] private Transform _model;

    private BoxCollider2D _boxCollider;
    private Vector2 _boxColliderInitialScale;
    private Vector2 _modelInitialScale;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxColliderInitialScale = _boxCollider.size;
        _modelInitialScale = transform.localScale;

        _boxCollider.size = _boxColliderInitialScale * _scale;
        _model.transform.localScale = _modelInitialScale * _scale;
    }
}
