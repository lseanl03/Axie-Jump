using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trunk") || collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
