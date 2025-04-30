using DG.Tweening;
using UnityEngine;

public class TrunkController : MonoBehaviour
{
    [SerializeField] private PoolType poolType;
    [SerializeField] private TrunkType trunkType;
    [SerializeField] private Trunk[] trunks;
    [SerializeField] private HiddenBox[] hiddenBoxes;

    private Tween moveLoopTween;

    private void Awake()
    {
        if (trunks.Length == 0)
            trunks = GetComponentsInChildren<Trunk>();

        if (hiddenBoxes.Length == 0)
            hiddenBoxes = GetComponentsInChildren<HiddenBox>();
    }
    private void OnDisable()
    {
        ReturnObjFromTrunks();
        ReturnEnemyFromTrunks();
        if (moveLoopTween != null) moveLoopTween.Kill();
    }

    private void ReturnObjFromTrunks()
    {
        foreach (var trunk in trunks)
        {
            if (trunk.transform.childCount != 0)
            {
                var item = trunk.transform.GetComponentInChildren<Item>();
                if (item)
                {
                    PoolManager.Instance.ReturnObjFromTrunk(
                        item.gameObject, PoolType.Item);
                }
                else if(trunk.transform.GetComponentInChildren<Enemy>())
                {
                    var enemy = trunk.transform.GetComponentInChildren<Enemy>();
                    if (!enemy) return;
                    PoolManager.Instance.ReturnObjFromTrunk(
                        enemy.gameObject, enemy.PoolType);
                }
                else if(trunk.transform.GetComponentInChildren<Buff>())
                {
                    var buff = trunk.transform.GetComponentInChildren<Buff>();
                    if (!buff) return;
                    PoolManager.Instance.ReturnObjFromTrunk(
                        buff.gameObject, buff.PoolType);
                }
            }
        }
    }

    private void ReturnEnemyFromTrunks()
    {
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

    public PoolType PoolType
    {
        get { return poolType; }
    }
    #endregion

    /// <summary>
    /// Di chuyển gỗ lên xuống tạo hiệu ứng chuyển động
    /// </summary>
    public void TrunkMoveLoop()
    {
        moveLoopTween = transform.DOLocalMoveY(
            transform.localPosition.y - 0.2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// Lấy trunk ngẫu nhiên
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Spawn item hoặc enemy từ trunk
    /// </summary>
    public void SpawnFromTrunk()
    {
        int itemRate = GameConfig.itemSpawnRate;
        int enemyRate = GameConfig.enemySpawnRate;
        int randomRate = Random.Range(0, 100);
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
                if (beforeTrunkType == TrunkType._1TrunkBetween && 
                    lastTrunkType == TrunkType._2TrunkBothSides)
                {
                    EnemyManager.Instance.SpawnEnemiesFromTrunk(this);
                }
                else ItemManager.Instance.SpawnItemsFromTrunk(this);
            }
            else ItemManager.Instance.SpawnItemsFromTrunk(this);
        }
        else
        {
            BuffManager.Instance.SpawnBuffFromTrunk(this);
        }
    }
}
