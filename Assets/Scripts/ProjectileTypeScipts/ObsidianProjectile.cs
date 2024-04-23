using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsidianProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] private float projectileSpeed = 10f;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
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
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.HitByObsidian();
        }
        Destroy(gameObject);
    }
}
