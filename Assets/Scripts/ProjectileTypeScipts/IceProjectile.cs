using UnityEngine;
using System.Collections;

public class IceProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] private float projectileSpeed = 7f;
    [SerializeField] private float chillDuration;
    [SerializeField] private float chillAmount;
    private float projectileDamage = 0;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }

    public void SetChillAmount(float amount)
    {
        chillAmount = amount;
    }

    public void SetChillDuration(float duration)
    {
        chillDuration = duration;
    }

    private void Start()
    {
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

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {   // If the projectile collides with an object, deal damage to its health and destroy the projectile
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(projectileDamage);
            enemy.ApplyChillEffect(chillAmount, chillDuration, "NotArcticTower");
        }

        Destroy(gameObject);
    }
}


