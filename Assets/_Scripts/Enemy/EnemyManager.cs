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
        var enemy = GetEnemyWithType(enemyType);
        if (enemy != null)
        {
            var enemyPrefab = Instantiate(enemy, transform);
            enemyPrefab.transform.position = transform.position;
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

    public void SpawnEnemiesFromTrunk(TrunkController trunkController)
    {
        Trunk[] trunks = trunkController.GetTrunkRandom();
        var randomIndex = Random.Range(0, trunks.Length);

        SpawnEnemy(GetRandomEnemy().EnemyType, trunks[randomIndex].transform);
    }

    public Enemy GetRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemyData.enemyPrefabs.Length);
        Enemy enemy = enemyData.enemyPrefabs[randomIndex];
        return enemy;
    }
}
