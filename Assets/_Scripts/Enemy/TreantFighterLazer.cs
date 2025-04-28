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

    public void MoveBullet()
    {

        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveBulletCoroutine());
    }

    private IEnumerator MoveBulletCoroutine()
    {
        Vector3 direction = -transform.right;
        float distance = speed * Time.deltaTime;
        rb2d.linearVelocity = direction * speed;
        yield return new WaitForSeconds(3);
        PoolManager.Instance.ReturnObj(gameObject, PoolType.TreantFighterLazer);
    }
}
