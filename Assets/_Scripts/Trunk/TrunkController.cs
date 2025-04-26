using DG.Tweening;
using UnityEngine;

public class TrunkController : MonoBehaviour
{
    [SerializeField] private TrunkType trunkType;
    [SerializeField] private Trunk[] trunks;

    private void Awake()
    {
        if(trunks.Length == 0)
            trunks = GetComponentsInChildren<Trunk>();
    }
    private void Start()
    {
        TrunkMoveLoop();
    }

    #region Get Set
    public int TrunkAmount
    {
        get { return trunks.Length;  }
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

    public Trunk GetTrunkRandom()
    {
        int trunkRandom = Random.Range(0, trunks.Length);
        return trunks[trunkRandom];
    }
}
