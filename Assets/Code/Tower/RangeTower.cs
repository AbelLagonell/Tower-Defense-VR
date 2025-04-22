using System;
using UnityEngine;

public class RangeTower : BaseTower {
    public float projectileSpeed = 1;
    

    protected override void Attack() {
        if (currentAttackCooldown > 0) return;
        currentAttackCooldown = attackCooldown;
        var temp = Instantiate(spawnedObject, transform.position, transform.rotation);
        var script = temp.GetComponent<Projectile>();
        script.damage = damage;
        script.destroyOnHit = true;
        script.speed = projectileSpeed;

        Rigidbody rb = temp.GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * projectileSpeed;

    }

    private void OnTriggerStay(Collider other) {
        if (!active) return;
        if (!other.CompareTag("Enemy")) return;
        transform.LookAt(other.transform.position);
        
        Attack();
        Debug.DrawRay(transform.position, transform.forward * (detectionRadius * transform.localScale.x), Color.red);
    }

    protected override void OnDeath() { }
}