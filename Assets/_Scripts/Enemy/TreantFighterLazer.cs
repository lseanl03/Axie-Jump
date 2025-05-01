using DG.Tweening;
using System.Collections;
using UnityEngine;

public class TreantFighterLazer : MonoBehaviour
{
    private float speed = 20f;
    private Rigidbody2D rb2d;
    private Coroutine moveCoroutine;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
    }

    public void MoveBullet(bool isLeft)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveBulletCoroutine(isLeft));
    }

    private IEnumerator MoveBulletCoroutine(bool isLeft)
    {
        Vector3 direction = isLeft ? -transform.right : transform.right;
        float distance = speed * Time.deltaTime;
        rb2d.linearVelocity = direction * speed;
        yield return new WaitForSeconds(3);
        PoolManager.Instance.ReturnObjFromTrunk(gameObject, PoolType.TreantFighterLazer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerController>();
            player.Die();
            PoolManager.Instance.ReturnObjFromTrunk(gameObject, PoolType.TreantFighterLazer);
        }
    }
}
