using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hitbox : MonoBehaviour {
    public int damage = 1;
    public bool destroyOnHit;
    
    private Collider myCollider;
    
    private void Awake() {
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Enemy")) return;
        other.GetComponent<BaseEnemy>().TakeDamage(damage);
        if (destroyOnHit) Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        // Get the collider if we don't have it yet
        if (myCollider == null) {
            myCollider = GetComponent<Collider>();
        }
        
        // If we have a collider, draw based on its actual shape
        if (myCollider != null) {
            Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
            
            // Different handling based on collider type
            if (myCollider is BoxCollider boxCollider) {
                // Draw the box collider at its exact position and size
                Matrix4x4 originalMatrix = Gizmos.matrix;
                Gizmos.matrix = Matrix4x4.TRS(
                                              transform.TransformPoint(boxCollider.center), 
                                              transform.rotation,
                                              Vector3.Scale(transform.localScale, boxCollider.size)
                                             );
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                Gizmos.matrix = originalMatrix;
            }
            else if (myCollider is SphereCollider sphereCollider) {
                // For sphere colliders
                Vector3 center = transform.TransformPoint(sphereCollider.center);
                float radius = sphereCollider.radius * Mathf.Max(transform.lossyScale.x, 
                                                                 Mathf.Max(transform.lossyScale.y, transform.lossyScale.z));
                Gizmos.DrawSphere(center, radius);
            }
            else {
                // Generic fallback for other collider types
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            }
        }
    }
}