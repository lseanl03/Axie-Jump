using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private EnemyData enemyData;
    private GameObject enemyHolder;

    protected override void Awake()
    {
        base.Awake();
        enemyHolder = transform.GetChild(0).gameObject;
        enemyData = Resources.Load<EnemyData>("SOData/EnemyData");
    }

    /// <summary>
    /// Spawn enemy theo kiểu enemy và vị trí spawn
    /// </summary>
    /// <param name="enemyType"></param>
    /// <param name="pos"></param>
    public void SpawnEnemy(EnemyType enemyType, Transform pos)
    {
        var enemy = GetEnemyWithType(enemyType);
        if (enemy != null)
        {
            var enemyPrefab = Instantiate(enemy, enemyHolder.transform);
            enemyPrefab.transform.position = pos.position;
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
}
