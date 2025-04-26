using Spine.Unity;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyType enemyType;
    
    [Header("Animation")]
    [SerializeField] protected AnimationReferenceAsset idleAnim;

    protected SkeletonAnimation anim;

    protected virtual void Awake()
    {
        if (!anim)
            anim = GetComponentInChildren<SkeletonAnimation>();
    }
    public EnemyType EnemyType
    {
        get => enemyType;
    }

}
