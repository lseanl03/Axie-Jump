using DG.Tweening;
using UnityEngine;

public class TrunkController : MonoBehaviour
{
    [SerializeField] private TrunkType trunkType;
    [SerializeField] private Trunk[] trunks;

    private void Awake()
    {
        if (trunks.Length == 0)
            trunks = GetComponentsInChildren<Trunk>();
    }
    private void Start()
    {
        TrunkMoveLoop();
        SpawnFromTrunk();
    }

    #region Get Set
    public int TrunkAmount
    {
        get { return trunks.Length; }
    }
    public TrunkType TrunkType
    {
        get { return trunkType; }
    }
    #endregion

    /// <summary>
    /// Di chuyển gỗ lên xuống tạo hiệu ứng chuyển động
    /// </summary>
    private void TrunkMoveLoop()
    {
        transform.DOLocalMoveY(transform.localPosition.y - 0.2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public Trunk[] GetTrunkRandom()
    {
        if ((trunkType == TrunkType._2TrunkBothSides))
        {
            int randomIndex = Random.Range(0, trunks.Length);
            if (randomIndex == 0) //lấy 1 trunk
            {
                int index = Random.Range(0, trunks.Length);
                return new Trunk[] { trunks[index] };
            }
            else //lấy 2 trunk
            {
                return trunks;
            }
        }
        else
        {
            int trunkRandom = Random.Range(0, trunks.Length);
            return new Trunk[] { trunks[trunkRandom] }; 
        }
    }

    public void SpawnFromTrunk()
    {
        int itemRate = 0;
        int enemyRate = 0;
        int randomRate = Random.Range(0, 100);
        if (GameManager.Instance.Points <= 1000)
        {
            itemRate = 60;
            enemyRate = 20;
        }
        else
        {
            itemRate = 50;
            enemyRate = 30;
        }

        if (randomRate <= itemRate)
        {
            ItemManager.Instance.SpawnItemsFromTrunk(this);
        }
        else if (randomRate > itemRate && randomRate <= itemRate + enemyRate)
        {
            if (trunkType == TrunkType._2TrunkBothSides)
            {
                var lastTrunkType = TrunkManager.Instance.LastTrunk.TrunkType;
                var beforeTrunkType = TrunkManager.Instance.BeforeTrunk.TrunkType;
                if(beforeTrunkType == TrunkType._1TrunkBetween && lastTrunkType == TrunkType._2TrunkBothSides)
                {
                    EnemyManager.Instance.SpawnEnemiesFromTrunk(this);
                }
                else
                {
                    ItemManager.Instance.SpawnItemsFromTrunk(this);
                }
            }
            else
            {
                ItemManager.Instance.SpawnItemsFromTrunk(this);
            }
        }
    }
}
