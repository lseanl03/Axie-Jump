using Spine.Unity;
using System.Collections;
using UnityEngine;

public class TreantFighter : Enemy
{
    private float cooldownTime = 2f;
    [SerializeField] private AnimationReferenceAsset shoot;
    [SerializeField] private Transform bulletPoint;
    private TreantFighterLazer treantFighterLazer;
    private Coroutine shootLazerCoroutine;

    protected override void Awake()
    {
        base.Awake();
        if (!treantFighterLazer)
            treantFighterLazer = Resources.Load<TreantFighterLazer>(
                "Prefabs/Enemy/TreantFighterLazer");
        initialPosY = 0.4f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerController>();
            player.Die();
        }
    }

    private void OnDisable()
    {
        if (shootLazerCoroutine != null) StopCoroutine(shootLazerCoroutine);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ShootLazer();
    }

    private void ShootLazer()
    {
        shootLazerCoroutine = StartCoroutine(ShootLazerCoroutine());
    }

    private IEnumerator ShootLazerCoroutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(cooldownTime);
            anim.AnimationState.SetAnimation(0, shoot, false);
            anim.AnimationState.AddAnimation(0, idleAnim, true, 0);
            yield return new WaitForSeconds(0.4f);
            var lazerPrefab = PoolManager.Instance.GetObjFromTrunk(
                PoolType.TreantFighterLazer, bulletPoint.position, transform);
            var lazer = lazerPrefab.GetComponent<TreantFighterLazer>();
            lazer.MoveBullet(transform.localScale.x == 1);
        }
    }


}
