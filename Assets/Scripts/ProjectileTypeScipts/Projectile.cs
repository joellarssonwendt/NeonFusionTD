using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] private float projectileSpeed = 7f;
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

    private void Start()
    {
        // Start the coroutine to destroy the projectile after 3 seconds
        StartCoroutine(DestroyAfterTime(3f));
    }

    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized; // Calculate the direction towards the target and normalize it

        rb.velocity = direction * projectileSpeed; // Set the velocity of the Rigidbody to move the projectile towards the target
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Destroy the projectile
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If the projectile collides with an object, deal damage to its health and destroy the projectile
        Enemy health = other.gameObject.GetComponent<Enemy>();
        if (health != null)
        {
            health.TakeDamage(projectileDamage);
        }
        Destroy(gameObject);
    }
}
