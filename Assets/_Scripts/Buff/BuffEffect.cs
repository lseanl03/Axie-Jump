using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BuffEffect : MonoBehaviour
{
    [SerializeField] private PoolType poolType;
    private Coroutine hideCoroutine;
    private ParticleSystem particle;

    private void Awake()
    {
        if (particle == null)
            particle = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideCoroutine());
    }

    private void OnDisable()
    {
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
    }

    private IEnumerator HideCoroutine()
    {
        particle.Play();
        yield return new WaitForSeconds(2f);
        PoolManager.Instance.ReturnObj(gameObject, poolType);
    }
}
