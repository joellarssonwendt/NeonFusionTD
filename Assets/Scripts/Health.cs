using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if(hitPoints<= 0)
        {
            //EnemySpawner.onEnemyDestroyInvoke(); // If there's an EnemySpawner, invoke its onEnemyDestroyInvoke method (commented out for now)
            Destroy(gameObject);
        }
    }

}
