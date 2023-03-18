using Finance;
using UnityEngine;

public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private RewardView _rewardTemplate;
    [SerializeField] private float _offsetY = 0.3f;

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
        for (int i = 0; i < enemy.RewardList.Count; i++)
        {
            Reward reward = enemy.RewardList[i];
            Wallet wallet = WalletInitializer.Instance.GetWallet(reward.Currency);
            wallet.PutIn(reward.Value);


            RewardView rewardView = Instantiate(_rewardTemplate, enemy.transform.position + Vector3.up * _offsetY * i, Quaternion.identity);
            rewardView.Show(reward);
        }
    }
}
