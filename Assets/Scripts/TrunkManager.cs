using DG.Tweening;
using UnityEngine;

public class TrunkManager : MonoBehaviour
{
    [SerializeField] float lastPosY = 0;

    [SerializeField] private Trunk nextTrunk;

    [SerializeField] private TrunkData InitialTrunkSpawnData;
    [SerializeField] private TrunkData RandomTrunkSpawnData;


    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnTrunkInit(i);
        }
    }

    public void SpawnTrunkInit(int index)
    {
        Trunk trunk = Instantiate(InitialTrunkSpawnData.trunkPrefabs[index], transform);
        trunk.transform.position = new Vector2(0, lastPosY);
        lastPosY += GameConfig.distanceTrunkSpawn;
    }

    public void SpawnTrunkRandom()
    {
        int randomIndex = Random.Range(0, RandomTrunkSpawnData.trunkPrefabs.Length);
        Trunk trunkCanSpawn = RandomTrunkSpawnData.trunkPrefabs[randomIndex];

        Trunk trunkCanSpawned = Instantiate(RandomTrunkSpawnData.trunkPrefabs[randomIndex], transform);
    }
    public void TransitionTrunk()
    {
        if (!GameManager.Instance.GameStarted) return;
        transform.DOMoveY(transform.position.y - GameConfig.distanceTrunkTransition, GameConfig.trunkTransitionTime);
    }
}
