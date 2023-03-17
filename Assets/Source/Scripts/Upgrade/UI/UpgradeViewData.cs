using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeViewData", menuName = "UpgradeViewData")]
public class UpgradeViewData : ScriptableObject
{
    [SerializeField] private string _description;

    public string Description => _description;
}
