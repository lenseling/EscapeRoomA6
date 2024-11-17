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

    public Transform rightHandAnchor; // VR Right Hand anchor
    public Transform leftHandAnchor;  // VR Left Hand anchor
    private Transform activeHand;     // Active hand performing the interaction

    private Material originalMaterial; // To store the ring's original material
    public Material highlightMaterial; // Material used to highlight the ring

    void Start()
    {
        // Save the original material of the ring
        if (ring != null)
        {
            originalMaterial = ring.GetComponent<Renderer>().material;
        }
    }

    void Update()
    {
        // Detect active hand (which hand is being used)
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            activeHand = rightHandAnchor;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            activeHand = leftHandAnchor;
        }

        // If an active hand is detected, perform raycast to highlight the ring
        if (activeHand != null)
        {
            Ray ray = new Ray(activeHand.position, activeHand.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f)) // Perform raycast to detect the ring
            {
                if (hit.transform == transform) // If the ray hits the ring
                {
                    if (!isSelected) // Only highlight if not already selected
                    {
                        HighlightRing(); // Highlight the ring
                    }

                    // Keep the ring selected if the index trigger is pressed
                    isSelected = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) ||
                                 OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
                }
                else // If ray hits somewhere else, unhighlight
                {
                    if (isSelected)
                    {
                        UnhighlightRing(); // Remove highlight if not selected anymore
                    }
                    isSelected = false; // Deselect
                }
            }
            else // If ray doesn't hit anything, unhighlight
            {
                //if (isSelected)
                //{
                    UnhighlightRing(); // Remove highlight if ray misses
                //}
                isSelected = false; // Deselect
            }
        }

        // Rotate if selected
        if (isSelected && (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) ||
                           OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)))
        {
            float rotationAmount = rotationSpeed * Time.deltaTime * 2;
            transform.Rotate(Vector3.up, rotationAmount);
        }

        // Check alignment with the correct angle
        CheckRingAngles();
    }

    private void CheckRingAngles()
    {
        float ringAngle = Mathf.Abs(transform.eulerAngles.x);

        if (Mathf.Abs(ringAngle - correctAngle) <= angleTolerance)
        {
            if (lightSphere != null)
            {
                lightSphere.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else
        {
            if (lightSphere != null)
            {
                lightSphere.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    private void HighlightRing()
    {
        if (ring != null && highlightMaterial != null)
        {
            ring.GetComponent<Renderer>().material = highlightMaterial;
        }
    }

    private void UnhighlightRing()
    {
        if (ring != null && originalMaterial != null)
        {
            ring.GetComponent<Renderer>().material = originalMaterial;
        }
    }
}
