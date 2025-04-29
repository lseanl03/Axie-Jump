using Spine.Unity;
using UnityEngine;

public class BearMom : Enemy
{
    [SerializeField] private AnimationReferenceAsset getBuff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenRequest();
        }
    }

    private void OpenRequest()
    {
        EventManager.OpenRequestAction();
    }
}
