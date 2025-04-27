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
    protected virtual void Start()
    {
        SetInitialPos();
    }
    public EnemyType EnemyType
    {
        get => enemyType;
    }

    private void SetInitialPos()
    {
        if(transform.position.x < 0) Flip(false);
        else if (transform.position.x > 0) Flip(true);
    }
    private void Flip(bool isLeftDirection)
    {
        float scaleX = isLeftDirection ? 1 : -1;
        transform.localScale = new Vector2(scaleX, 1);
    }
}
