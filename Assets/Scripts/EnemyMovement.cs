using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Cache

    // Variables
    [SerializeField] private float maxPositionX, maxPositionY;

    private float moveInterval = 1f;

    // Methods
    void Start()
    {
        MoveRandomly();
    }

    private void MoveRandomly()
    {
        Vector2 randomPosition = new Vector2(Random.Range(0, maxPositionX), Random.Range(0, maxPositionY));
        transform.position = randomPosition;
        Invoke("MoveRandomly", moveInterval);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(maxPositionX, maxPositionY, 1));
    }
}
