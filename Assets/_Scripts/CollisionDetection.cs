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
    }
}
