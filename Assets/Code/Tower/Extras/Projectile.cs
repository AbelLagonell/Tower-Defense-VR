using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Hitbox {
    public float speed = 1;
    public float maxDuration = 2f;

    private void Start() {
        Destroy(gameObject, maxDuration);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Floor")) if (destroyOnHit) Destroy(gameObject);
    }
}
