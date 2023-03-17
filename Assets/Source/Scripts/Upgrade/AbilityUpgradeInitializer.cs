using UnityEngine;

public class AbilityUpgradeInitializer : MonoBehaviour
{
    [SerializeField] private ContainerInstantiator _containerInstantiator;
    [SerializeField] private UpgradePanel _upgradePanelTemplate;
    [SerializeField] private IntUpgradePanel _intUpgradePanelTemplate;

    public void Init()
    {
        AbilityUpgrade[] abilities = GetComponentsInChildren<AbilityUpgrade>();

        foreach (AbilityUpgrade ability in abilities)
        {
            ability.Init(_containerInstantiator, _upgradePanelTemplate);
        }

        IntAbilityUpgrade[] intAbilities = GetComponentsInChildren<IntAbilityUpgrade>();

        foreach (IntAbilityUpgrade ability in intAbilities)
        {
            ability.Init(_containerInstantiator, _intUpgradePanelTemplate);
        }

        _containerInstantiator.UpdateHeight();
    }
}
