using UnityEngine;
using System.Collections;

public class DotProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] private float projectileSpeed = 7f;
    [SerializeField] private float dotDuration = 3f; // Duration of the DoT effect
    [SerializeField] private float dotDamage = 5f; // Damage per second for the DoT effect
    private float projectileDamage = 0;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(float damage)
    {
        projectileDamage = damage; // Method to set the damage value
    }

    public void SetDotDamage(float damage)
    {
        dotDamage = damage; // Method to set the DoT damage value
    }

    public void SetDotDuration(float duration)
    {
        dotDuration = duration; // Method to set the DoT duration value
    }

    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized; // Calculate the direction towards the target and normalize it

        rb.velocity = direction * projectileSpeed; // Set the velocity of the Rigidbody to move the projectile towards the target
    }

    private void OnCollisionEnter2D(Collision2D other)
    {   // If the projectile collides with an object, deal damage to its health and destroy the projectile
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Apply initial damage
            enemy.TakeDamage(projectileDamage);

            // Apply DoT effect
            StartCoroutine(ApplyDotEffect(enemy));
        }

        Destroy(gameObject);
    }

    private IEnumerator ApplyDotEffect(Enemy enemy)
    {
        // Add the dotDamage to the total DoT effect for this dotProjectile
        enemy.TakeDotDamage(dotDamage, this);

        Debug.Log("DoT effect started on enemy: " + enemy.name);

        float timer = 0f;

        while (timer < dotDuration)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        // Remove the dotDamage from the total DoT effect when DoT effect ends
        dotDamage = 0;

        Debug.Log("DoT effect ended on enemy: " + enemy.name);
    }

}


