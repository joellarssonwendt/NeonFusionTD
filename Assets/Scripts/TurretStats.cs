using UnityEngine;

[CreateAssetMenu(fileName = "NewTurretStats", menuName = "Stats/TurretStats")]
public class TurretStats : ScriptableObject
{
    public float targetingRange;
    public float rotationSpeed;
    public float projectilesPerSecond;
    public float projectileDamage;
    public float dotAmount; //Total damage from the damage over time debuff 
    public float dotDuration; //Duration of the damage over time debuff 
}
