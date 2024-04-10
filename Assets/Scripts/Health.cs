using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 2;

    private Animator ani;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void TakeDamage(float dmg)
    {
        hitPoints -= dmg;

        if(hitPoints<= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            ani.Play("Ghost_Death");
            Invoke("EnemyDeath", 1f);
        }
    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
