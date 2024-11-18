using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInteractor : MonoBehaviour
{
    public Transform rightHandController; // Assign the right-hand controller (tracked pose)
    public float maxRayDistance = 3f;   // Maximum raycast distance
    public LayerMask interactableLayer;  // Layer for interactable objects (assign board's layer)
    public GameObject path;

    void Start()
    {
        path.SetActive(false);
    }

    void Update()
    {
        // Check if the right trigger is pressed
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            //Debug.Log("trigger pressed");
            // Perform a raycast from the right-hand controller
            if (Physics.Raycast(rightHandController.position, rightHandController.forward, out RaycastHit hit, maxRayDistance))
            {
                //Debug.Log("ray casted");
                print(hit.collider);
                // Check if the ray hits the board
                if (hit.collider.gameObject == transform.parent.gameObject)
                {
                    //Debug.Log("HIT)");
                    path.SetActive(true);
                    Transform childTransform = transform.Find("cursor1");
                    if (childTransform != null)
                    {
                        // Get the script attached to the child
                        Cursor script = childTransform.GetComponent<Cursor>();

                        if (script != null)
                        {
                            // Call the function in the script
                            script.moveCursor();
                        }
                        else
                        {
                            Debug.LogError("Script not found on cursor1!");
                        }
                    }
                    else
                    {
                        Debug.LogError("Child 'cursor1' not found!");
                    }
                } 
            }
        }
    }
}
