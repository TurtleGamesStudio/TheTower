using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private SpawnData[] _spawnDatas;

    public IEnumerable<SpawnData> SpawnDatas => _spawnDatas;
}
