using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    private float rotationSpeed = 50f;
    private bool isSelected = false;
    public GameObject ring; // Reference to the outer ring
    public GameObject lightSphere; // Reference to the light sphere
    public float correctAngle; // Target angle for the inner ring
    private float angleTolerance = 10f; // Tolerance for matching angles
    private bool solved = false;

    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform a raycast to check if the ring was clicked
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // Check if this ring (inner ring) was clicked
                {
                    isSelected = true;
                }
                else
                {
                    isSelected = false; // Deselect if clicking elsewhere
                }
            }
        }

        // Rotate the inner and outer rings if the inner ring is selected
        if (isSelected && Input.GetMouseButton(0))
        {
            float rotationAmount = rotationSpeed * Time.deltaTime * 2;

            // Rotate the inner ring
            transform.Rotate(Vector3.up, rotationAmount);

            // Rotate the outer ring
            if (ring != null)
            {
                ring.transform.Rotate(Vector3.up, rotationAmount);
            }
        }

        // Deselect when the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
        }

        // Check if both rings are at the correct angles
        CheckRingAngles();
    }

    private void CheckRingAngles()
    {
        // Get current angles
        float ringAngle = Mathf.Abs(transform.eulerAngles.x);  // Check X-axis rotation of the inner ring

        // Check if the angle is within the tolerance range
        if (Mathf.Abs(ringAngle - correctAngle) <= angleTolerance)
        {
            // Turn the light green if aligned
            if (lightSphere != null)
            {
                lightSphere.GetComponent<Renderer>().material.color = Color.green;
                Debug.Log("Ring aligned. Light turned green.");
            }
        }
        else
        {
            // Reset the light color (optional)
            if (lightSphere != null)
            {
                lightSphere.GetComponent<Renderer>().material.color = Color.red;
                Debug.Log("Ring not aligned. Light turned red.");
            }
        }

        
    }

}
