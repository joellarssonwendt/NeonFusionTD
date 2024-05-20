using System.Collections.Generic;
using UnityEngine;

public class ArcticTower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TurretStats turretStats;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private ParticleSystem iceAuraParticleSystem;

    AudioManager audioManager;

    private List<Enemy> enemiesInRange = new List<Enemy>();
    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    private void Update()
    {
        float chillEffectRange = turretStats.targetingRange;
        float chillAmount = turretStats.chillAmount;
        float chillDuration = turretStats.chillDuration;

        // Find all enemies within the chill effect range
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, chillEffectRange, enemyMask);

        // Clear the list of enemies in range
        enemiesInRange.Clear();

        // Add all found enemies to the list
        foreach (var enemy in enemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null && !enemyComponent.isDead)
            {
                enemiesInRange.Add(enemyComponent);
            }
        }

        if (enemiesInRange.Count > 0)
        {
            iceAuraParticleSystem.Play();
            audioManager.GetComponent<AudioManager>().PlaySoundEffect("Arctic");
        }
        else
        {
            iceAuraParticleSystem.Stop();
        }

        foreach (var enemy in enemiesInRange)
        {
            enemy.ApplyChillEffect(chillAmount, chillDuration, "ArcticTower");
        }
    }
}
