using DG.Tweening;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    private Transform[] trunkTransformList;
    private void Start()
    {
        trunkTransformList = GetComponentsInChildren<Transform>();
        transform.DOLocalMoveY(transform.position.y - 0.2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
    public int TrunkAmount()
    {
        return transform.childCount;
    }

    public float[] trunkPosXList()
    {
        float[] trunkPosXList = new float[trunkTransformList.Length];
        for (int i = 0; i < trunkTransformList.Length; i++)
        {
            trunkPosXList[i] = trunkTransformList[i].position.x;
        }
        return trunkPosXList;
    }
}
