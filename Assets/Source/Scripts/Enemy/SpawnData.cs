using UnityEngine;

public class SpawnData : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public Enemy Enemy => _enemy;
}
