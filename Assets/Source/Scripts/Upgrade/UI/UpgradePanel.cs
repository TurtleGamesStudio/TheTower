using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Finance;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _max;
    [SerializeField] private FloatParametrView _floatParametrView;
    [SerializeField] private PriceView _priceView;
    [SerializeField] private TMP_Text _description;

    public Button Button => _button;
    public GameObject Max => _max;

    public void Init(AbilityUpgrade abilityUpgrade)
    {
        _floatParametrView.Init(abilityUpgrade.FloatParametr);
        _priceView.Init(abilityUpgrade.Price);

        _description.text = abilityUpgrade.UpgradeViewData.Description;
    }

    private void OnMaxLevelReached()
    {
        _button.gameObject.SetActive(false);
        _max.gameObject.SetActive(true);
        //enabled = false;
    }
}
