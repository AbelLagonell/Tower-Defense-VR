using System;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(BoxCollider))]
public class Player : Entity {
    private BoxCollider myCollider;

    protected override void OnDeath() {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene("Lost");
    }

    private void OnDrawGizmos() {
        // Get the collider if we don't have it yet
        if (myCollider == null) {
            myCollider = GetComponent<BoxCollider>();
        }

        // If we have a collider, draw based on its actual shape
        if (myCollider != null) {
            Gizmos.color = new Color(1f, 0f, 0f, 0.3f);

            // Different handling based on collider type
            // Draw the box collider at its exact position and size
            Matrix4x4 originalMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(
                                          transform.TransformPoint(myCollider.center),
                                          transform.rotation,
                                          Vector3.Scale(transform.localScale, myCollider.size)
                                         );
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = originalMatrix;
        }
    }
}