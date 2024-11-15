using UnityEngine;

public class PaintingFrame : MonoBehaviour
{
    [Header("Frame Settings")]
    public string requiredPaintingTag; // The tag of the painting this frame accepts
    public Transform fixedPosition;   // Position to fix the painting in place

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the correct tag
        if (other.CompareTag(requiredPaintingTag))
        {
            Debug.Log($"Correct painting '{other.name}' placed in frame '{gameObject.name}'!");

            // Optionally, disable or destroy the painting after placement
            Destroy(other.gameObject); // OR set it inactive: other.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log($"Painting '{other.name}' does not match frame '{gameObject.name}'.");
        }
    }

    private void FixPainting(Transform painting)
    {
        // Position and rotate the painting to match the frame's fixed position
        //painting.position = fixedPosition.position;
        //painting.rotation = fixedPosition.rotation;
        //painting.localScale = fixedPosition.localScale; // Match scale if needed
    }
}
