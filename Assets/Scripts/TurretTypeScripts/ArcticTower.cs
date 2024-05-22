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
    private bool soundIsPlaying = false;
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
        bool enemyInRange = false;

        // Add all found enemies to the list
        foreach (var enemy in enemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null && !enemyComponent.isDead)
            {
                enemyInRange = true;
                break;
            }
        }

        if (enemyInRange)
        {
            iceAuraParticleSystem.Play();

            if(!soundIsPlaying)
            {
                soundIsPlaying = true;
                audioManager.GetComponent<AudioManager>().PlaySoundEffect("Arctic");
            }
        }
        else
        {
            iceAuraParticleSystem.Stop();
            if(soundIsPlaying)
            {
                soundIsPlaying = false;
                audioManager.Stop("Arctic");
            }
        }

        foreach (var enemy in enemiesInRange)
        {
            enemy.ApplyChillEffect(chillAmount, chillDuration, "ArcticTower");
        }
    }
}
