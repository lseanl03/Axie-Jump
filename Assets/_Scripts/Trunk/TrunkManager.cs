using DG.Tweening;
using UnityEngine;
using static Unity.Collections.Unicode;

public class TrunkManager : MonoBehaviour
{
    #region Properties
    [SerializeField] float lastPosY = 0;

    private Trunk lastTrunk;
    private GameObject trunkHolder;
    private TrunkData InitialTrunkSpawnData;
    private TrunkData RandomTrunkSpawnData;
    #endregion

    private void Awake()
    {
        trunkHolder = transform.GetChild(0).gameObject;

        InitialTrunkSpawnData = Resources.Load<TrunkData>("SOData/InitialTrunkData");
        RandomTrunkSpawnData = Resources.Load<TrunkData>("SOData/RandomTrunkData");
    }
    private void Start()
    {
        SpawnListTrunkInitial();
    }

    private void OnEnable()
    {
        EventManager.onTransitionTrunk += TransitionTrunkDown;
        EventManager.onTransitionTrunk += SpawnNextTrunk;
        EventManager.onNormalJump += TransitionTrunk;
    }

    private void OnDisable()
    {
        EventManager.onTransitionTrunk -= TransitionTrunkDown;
        EventManager.onTransitionTrunk -= SpawnNextTrunk;
        EventManager.onNormalJump -= TransitionTrunk;
    }

    /// <summary>
    /// Spawn gỗ dựa vào dữ liệu ban đầu
    /// </summary>
    public void SpawnListTrunkInitial()
    {
        foreach(var trunk in InitialTrunkSpawnData.trunkPrefabs)
        {
            SpawnTrunkInitital(trunk);
        }
    }

    /// <summary>
    /// Khởi tạo gỗ ban đầu
    /// </summary>
    /// <param name="trunk"></param>
    public void SpawnTrunkInitital(Trunk trunk)
    {
        Trunk trunkPrefab = Instantiate(trunk, trunkHolder.transform);
        trunkPrefab.transform.position = new Vector2(0, lastPosY);
        lastTrunk = trunkPrefab;
        lastPosY += GameConfig.distanceTrunkSpawn;
    }

    /// <summary>
    /// Spawn gỗ
    /// </summary>
    /// <param name="trunk"></param>
    public void SpawnTrunk(Trunk trunk)
    {
        Trunk trunkPrefab = Instantiate(trunk, trunkHolder.transform);
        trunkPrefab.transform.position = new Vector2(0, lastPosY);
        lastPosY = lastTrunk.transform.position.y + GameConfig.distanceTrunkSpawn;
        lastTrunk = trunkPrefab;
    }

    /// <summary>
    /// Chuyển tiếp gỗ đi xuống
    /// </summary>
    public void TransitionTrunk()
    {
        if (!GameManager.Instance.GameStarted) return;
        EventManager.TransitionTrunkAction();
    }

    /// <summary>
    /// Chuyển Gỗ đi xuống 
    /// </summary>
    private void TransitionTrunkDown()
    {
        trunkHolder.transform.DOMoveY(
            trunkHolder.transform.position.y - 5,
            GameConfig.trunkTransitionTime);
    }
    /// <summary>
   /// Spawn gỗ kế tiếp dựa vào gỗ cuối cùng
    /// </summary>
    private void SpawnNextTrunk()
    {
        TrunkType[] listTrunkCanSpawn = GetNextTrunkTypeCanSpawn();
        TrunkType trunkType = RandomTrunkType(listTrunkCanSpawn);
        SpawnTrunk(GetTrunkWithTrunkType(trunkType));
    } 
    /// <summary>
    /// Trả về danh sách kiểu gỗ tiếp theo có thể spawn
    /// </summary>
    /// <returns></returns>
    private TrunkType[] GetNextTrunkTypeCanSpawn()
    {
        TrunkType[] listTrunkCanSpawn = null;
        
        switch (lastTrunk.TrunkType)
        {
            case TrunkType._1TrunkLeft:
                listTrunkCanSpawn = new TrunkType[]
                {
                    TrunkType._1TrunkLeft,
                    TrunkType._1TrunkBetween,
                    TrunkType._2TrunkBothSides,
                    TrunkType._2TrunkRight
                };
                break;
            case TrunkType._1TrunkRight:
                listTrunkCanSpawn = new TrunkType[]
                {
                    TrunkType._1TrunkRight,
                    TrunkType._1TrunkBetween,
                    TrunkType._2TrunkBothSides,
                    TrunkType._2TrunkLeft
                };
                break;
            case TrunkType._1TrunkBetween:
                listTrunkCanSpawn = new TrunkType[]
                {
                    TrunkType._1TrunkLeft,
                    TrunkType._1TrunkRight,
                };
                break;
            case TrunkType._2TrunkLeft:
                listTrunkCanSpawn = new TrunkType[]
                {
                    TrunkType._1TrunkRight,
                };
                break;
            case TrunkType._2TrunkRight:
                listTrunkCanSpawn = new TrunkType[]
                {
                    TrunkType._1TrunkLeft,
                };
                break;
            case TrunkType._2TrunkBothSides:
                listTrunkCanSpawn = new TrunkType[]
                {
                    TrunkType._1TrunkBetween
                };
                break;
            default:
                Debug.Log("Cant find Trunk Type");
                break;
        }
        return listTrunkCanSpawn;
    }

    /// <summary>
    /// Spawn gỗ ngẫu nhiên theo danh sách kiểu gỗ 
    /// </summary>
    /// <param name="listTrunkCanSpawn"></param>
    /// <returns></returns>
    private TrunkType RandomTrunkType(TrunkType[] listTrunkCanSpawn)
    {
        int randomIndex = Random.Range(0, listTrunkCanSpawn.Length);
        return listTrunkCanSpawn[randomIndex];
    }

    /// <summary>
    /// Trả về gỗ dựa vào kiểu gỗ
    /// </summary>
    /// <param name="trunkType"></param>
    /// <returns></returns>
    private Trunk GetTrunkWithTrunkType(TrunkType trunkType)
    {
        foreach (var trunk in RandomTrunkSpawnData.trunkPrefabs)
        {
            if (trunk.TrunkType == trunkType)
            {
                return trunk;
            }
        }
        return null;
    }
}
