using System;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeTower : BaseTower {
    public float attackDuration = 2f;
    public Vector3 hitboxSize;
    public Vector3 hitboxOffset;
    private Quaternion hitboxRotation; 
    

    protected override void OnDeath() { }

    protected override void Attack() {
        if (currentAttackCooldown > 0) return;
        currentAttackCooldown = attackCooldown;
        var temp = SpawnHitbox();
        Destroy(temp, attackDuration);
    }

    private GameObject SpawnHitbox() {
        Vector3 offset = transform.forward * hitboxOffset.z + transform.right * hitboxOffset.x +
                         transform.up * hitboxOffset.y;
        //TODO Change where it spawns
        GameObject hitbox =
            Instantiate(spawnedObject, transform.position + offset, hitboxRotation);
        hitbox.transform.localScale = hitboxSize;
        var script = hitbox.GetComponent<Hitbox>();
        script.damage = damage;
        script.destroyOnHit = false;
        return hitbox;
    }

    private void OnTriggerStay(Collider other) {
        if (!active) return;
        if (other.CompareTag("Enemy")) {
            transform.LookAt(other.transform);
            float yRotation = transform.eulerAngles.y;
            hitboxRotation = Quaternion.Euler(0, yRotation, 0);
            Debug.DrawRay(transform.position, transform.forward * (detectionRadius * transform.localScale.x),
                          Color.red);
            Attack();
        }
    }
}