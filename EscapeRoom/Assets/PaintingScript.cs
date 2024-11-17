using UnityEngine;

public class PaintingFrame : MonoBehaviour
{
    [Header("Frame Settings")]
    public string requiredPaintingTag; // The tag of the painting this frame accepts
    public Transform fixedPosition;   // Position to fix the painting in place
    public GameObject fixedPainting;

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the correct tag
        if (other.CompareTag(requiredPaintingTag))
        {
            Debug.Log($"Correct painting '{other.name}' placed in frame '{gameObject.name}'!");

            // Optionally, disable or destroy the painting after placement
            //Destroy(other.gameObject); // OR set it inactive: other.gameObject.SetActive(false);
            //FixPainting(other.transform);
            GetComponent<Collider>().isTrigger = false;
            other.gameObject.SetActive(false);
            fixedPainting.SetActive(true);
            GameManager.Instance.fixPainting();

        }
        else
        {
            Debug.Log($"Painting '{other.name}' does not match frame '{gameObject.name}'.");
        }
    }

    private void FixPainting(Transform paintingTransform)
    {
        Debug.Log(paintingTransform.gameObject.name);
        Transform cubeTransform = null;

        // Check if the parent exists and try to find "Cube (1)" under it
        if (transform.parent != null)
        {
            cubeTransform = transform.parent.Find("Cube (1)");
        }

        // If not found under the parent or no parent exists, search under the current object
        if (cubeTransform == null)
        {
            cubeTransform = transform.Find("Cube (1)");
        }
        cubeTransform.gameObject.SetActive(false);
        paintingTransform.gameObject.SetActive(false);

        paintingTransform.position = cubeTransform.position;
        transform.rotation = cubeTransform.rotation;
        Quaternion targetRotation = cubeTransform.rotation;
        paintingTransform.rotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        paintingTransform.gameObject.SetActive(true);
        //gameObject.SetActive(true);//painting.localScale = fixedPosition.localScale; // Match scale if needed
        Debug.Log("changed");
    }
}
