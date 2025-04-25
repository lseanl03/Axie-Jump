using UnityEngine;

public class HiddenBox : MonoBehaviour
{
    [SerializeField] private bool isCollided = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollided)
        {
            isCollided = true;
            EnemyManager.Instance.SpawnEnemy(EnemyType.UFO, transform);
        }
    }
}
