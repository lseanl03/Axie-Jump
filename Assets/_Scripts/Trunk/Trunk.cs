using DG.Tweening;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    [SerializeField] private TrunkType trunkType;
    private void Start()
    {
        TrunkMoveLoop();
    }

    #region Get Set
    public int TrunkAmount
    {
        get { return transform.childCount;  }
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

}
