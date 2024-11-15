using UnityEngine;

public class CapsuleFollowCamera : MonoBehaviour
{
    public Transform cameraTransform; // Assign the XR camera here
    public Rigidbody capsuleCollider;

    void Update()
    {
        if (cameraTransform == null || capsuleCollider == null)
            return;

        // Match the capsule's position to the camera's horizontal position
        Vector3 capsulePosition = capsuleCollider.transform.position;
        capsulePosition.x = cameraTransform.position.x;
        capsulePosition.z = cameraTransform.position.z;

        // Adjust the capsule's height based on camera's Y position
        //float cameraHeight = cameraTransform.position.y;
        //capsuleCollider.center = new Vector3(0, cameraHeight / 2f, 0); // Adjust the center based on height
        //capsuleCollider.height = Mathf.Max(cameraHeight, 1.0f); // Ensure a minimum height
        //capsuleCollider.transform.position = capsulePosition;
    }
}
