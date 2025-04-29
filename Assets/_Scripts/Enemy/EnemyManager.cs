using UnityEngine;
using static Unity.Collections.Unicode;

public class EnemyManager : Singleton<EnemyManager>
{
    private EnemyData enemyData;
    private GameObject enemyHolder;

    protected override void Awake()
    {
        base.Awake();
        if(!enemyData)
            enemyData = Resources.Load<EnemyData>("SOData/EnemyData");
    }

    /// <summary>
    /// Spawn enemy theo kiểu enemy và vị trí spawn
    /// </summary>
    /// <param name="enemyType"></param>
    /// <param name="pos"></param>
    public void SpawnEnemy(EnemyType enemyType, Transform transform)
    {
        var enemyPrefab = GetEnemyWithType(enemyType);
        if (enemyPrefab != null)
        {
            var prefab = PoolManager.Instance.GetObjFromTrunk(
                enemyPrefab.PoolType, transform.position, transform);
            var enemy = prefab.GetComponent<Enemy>();
            enemy.SetInitialDir();
            enemy.SetInitialPosY();

        }
    }

    /// <summary>
    /// Trả về enemy với kiểu enemy
    /// </summary>
    /// <param name="enemyType"></param>
    /// <returns></returns>
    private Enemy GetEnemyWithType(EnemyType enemyType)
    {
        foreach (var enemy in enemyData.enemyPrefabs)
        {
            if (enemy.EnemyType == enemyType)
            {
                return enemy;
            }
        }
        return null;
    }

    /// <summary>
    /// Spawn enemy từ trunk
    /// </summary>
    /// <param name="trunkController"></param>
    public void SpawnEnemiesFromTrunk(TrunkController trunkController)
    {
        Trunk[] trunks = trunkController.GetTrunkRandom();
        var randomIndex = Random.Range(0, trunks.Length);

        SpawnEnemy(GetRandomEnemy().EnemyType, trunks[randomIndex].transform);
    }

    /// <summary>
    /// Lấy enemy ngẫu nhiên từ enemyData
    /// </summary>
    /// <returns></returns>
    public Enemy GetRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemyData.enemyPrefabs.Length);
        Enemy enemy = enemyData.enemyPrefabs[randomIndex];
        Debug.Log($"Enemy: {enemy.EnemyType}");
        return enemy;
    }
}
