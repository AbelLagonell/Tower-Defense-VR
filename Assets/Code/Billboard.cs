using UnityEngine;

public class Billboard : MonoBehaviour {
    Camera mainCamera;

    void Start() {
        mainCamera = Camera.main;
    }

    void Update() {
        // Get direction from sprite to camera (horizontal only)
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        directionToCamera.y = 0; // Zero out vertical component

        if (directionToCamera.magnitude > 0.001f) // Avoid zero vector
        {
            if (transform.parent != null) {
                // WITH PARENT: Handle in local space

                // Convert direction to local space of parent
                Vector3 localDirectionToCamera = transform.parent.InverseTransformDirection(directionToCamera);

                // Calculate local rotation to face camera horizontally
                Quaternion localRotation = Quaternion.LookRotation(localDirectionToCamera, transform.parent.up);

                // Apply only Y rotation in local space
                Vector3 localEuler = localRotation.eulerAngles;
                localEuler.x = 0;
                localEuler.z = 0;

                // Set the local rotation
                transform.localRotation = Quaternion.Euler(localEuler);
            } else {
                // NO PARENT: Handle in world space

                // Calculate rotation to face camera horizontally
                Quaternion worldRotation = Quaternion.LookRotation(directionToCamera);

                // Preserve original X and Z rotation
                Vector3 currentEuler = transform.rotation.eulerAngles;
                Vector3 targetEuler = worldRotation.eulerAngles;

                // Only take Y from the camera-facing rotation
                transform.rotation = Quaternion.Euler(currentEuler.x, targetEuler.y, currentEuler.z);
            }
        }
    }
}