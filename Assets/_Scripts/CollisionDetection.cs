using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trunk"))
        {
            TrunkController trunk = collision.gameObject.GetComponent<TrunkController>();
            if(trunk) PoolManager.Instance.ReturnTrunkObj(trunk.gameObject, trunk.PoolType);
        }
        else if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die();
                collision.gameObject.SetActive(false);
            }
        }
    }
}
