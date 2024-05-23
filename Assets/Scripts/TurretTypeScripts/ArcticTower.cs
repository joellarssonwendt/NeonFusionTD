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
    private float timeSinceLastTargetFound = 0f;

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
        enemiesInRange.Clear();

        // Add all found enemies to the list
        foreach (var enemy in enemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null && !enemyComponent.isDead)
            {
                enemiesInRange.Add(enemyComponent);
                break;
            }
        }

        if (enemiesInRange.Count > 0)
        {
            if(!iceAuraParticleSystem.isPlaying)
            {
                iceAuraParticleSystem.Play();
                timeSinceLastTargetFound = Time.time;

            }

            if (!soundIsPlaying)
            {
                soundIsPlaying = true;
                audioManager.PlaySoundEffect("Arctic");
            }
        }
        else
        {
            if(iceAuraParticleSystem.isPlaying) 
            {
                iceAuraParticleSystem.Stop();
            }

            if(soundIsPlaying)
            {
                if (timeSinceLastTargetFound >= 0.5f && enemiesInRange.Count == 0)
                {
                    audioManager.Stop("Arctic");
                    soundIsPlaying = false;
                }
                timeSinceLastTargetFound = Time.time;
            }
        }

        foreach (var enemy in enemiesInRange)
        {
            enemy.ApplyChillEffect(chillAmount, chillDuration, "ArcticTower");
        }
    }
}
