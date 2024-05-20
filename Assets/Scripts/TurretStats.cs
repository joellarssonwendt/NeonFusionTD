using UnityEngine;

[CreateAssetMenu(fileName = "NewTurretStats", menuName = "Stats/TurretStats")]
public class TurretStats : ScriptableObject
{
    public float targetingRange;
    public float rotationSpeed;
    public float projectilesPerSecond;
    public float projectileDamage;
    public float dotDamagePerSecond; 
    public float dotDuration; 
    public float chillAmount;
    public float chillDuration;
    public int maxChains;
    public float chainRange;
}
