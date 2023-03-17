using Finance;
using UnityEngine;

public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private RewardView _rewardTemplate;

    private void OnEnable()
    {
        _enemySpawner.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemySpawner.Died -= OnDied;
    }

    private void OnDied(Enemy enemy)
    {
        Wallet wallet = WalletInitializer.Instance.GetWallet(enemy.Reward.Currency);
        wallet.PutIn(enemy.Reward.Value);

        RewardView reward = Instantiate(_rewardTemplate, enemy.transform.position, Quaternion.identity);
        reward.Show(enemy.Reward.Value);
    }
}
