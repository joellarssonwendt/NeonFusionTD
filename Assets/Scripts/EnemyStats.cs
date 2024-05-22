using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "Stats/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float maxHealth;
    public float moveSpeed;
    public int damageAmount;
    public bool infinityMode = false;
}
