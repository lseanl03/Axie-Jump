using Spine.Unity;
using System.Collections;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private AnimationReferenceAsset teleAnim;
    private Coroutine teleCoroutine;
    private BoxCollider2D boxCollider2D;

    private float teleTime = 1f;
    private float idleTime = 2f;

    protected override void Awake()
    {
        base.Awake();
        if (!anim)
            anim = GetComponentInChildren<SkeletonAnimation>();

        if (!boxCollider2D)
            boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Tele();
    }

    private void OnDisable()
    {
        if (teleCoroutine != null) StopCoroutine(teleCoroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerController>();
            player.Hurt();
        }
    }
    public void Tele()
    {
        if (teleCoroutine != null) StopCoroutine(teleCoroutine);
        teleCoroutine = StartCoroutine(TeleCoroutine());
    }

    private IEnumerator TeleCoroutine()
    {
        while(gameObject.activeSelf)
        {
            anim.AnimationState.SetAnimation(0, teleAnim, false);
            yield return new WaitForSeconds(teleTime/2);
            boxCollider2D.enabled = false;
            yield return new WaitForSeconds(teleTime/2);
            anim.AnimationState.SetAnimation(0, idleAnim, true);
            boxCollider2D.enabled = true;
            SetTelePos();
            SetInitialDir();
            yield return new WaitForSeconds(idleTime);
        }
    }

    private void SetTelePos()
    {
        var maxPosX = GameConfig.maxPos;
        var posX = transform.position.x == maxPosX ? -maxPosX : maxPosX;
        var newPos = new Vector2(posX, transform.position.y);
        transform.position = newPos;
    }
}
