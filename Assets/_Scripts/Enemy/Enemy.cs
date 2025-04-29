using Spine.Unity;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float initialPosY = 0.5f;
    [SerializeField] protected EnemyType enemyType;
    [SerializeField] protected PoolType poolType;

    [Header("Animation")]
    [SerializeField] protected AnimationReferenceAsset idleAnim;

    protected SkeletonAnimation anim;

    protected virtual void Awake()
    {
        if (!anim)
            anim = GetComponentInChildren<SkeletonAnimation>();
    }
    protected virtual void OnEnable()
    {
        anim.AnimationState.SetAnimation(0, idleAnim, true);
    }

    public EnemyType EnemyType
    {
        get => enemyType;
    }

    public PoolType PoolType
    {
        get => poolType;
    }

    /// <summary>
    /// Set hướng nhìn ban đầu của enemy
    /// </summary>
    public void SetInitialDir()
    {
        if(transform.position.x < 0) Flip(false);
        else if (transform.position.x > 0) Flip(true);
    }

    /// <summary>
    /// Set vị trí ban đầu của enemy
    /// </summary>
    public void SetInitialPosY()
    {
        transform.position = new Vector2(transform.position.x, 
            transform.position.y + initialPosY);
    }
    private void Flip(bool isLeftDirection)
    {
        float scaleX = isLeftDirection ? 1 : -1;
        transform.localScale = new Vector2(scaleX, 1);
    }
}
