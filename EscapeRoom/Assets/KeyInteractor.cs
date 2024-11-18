using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractor : MonoBehaviour
{
    public Transform rightHandController; // Assign the right-hand controller (tracked pose)
    public float maxRayDistance = 3f;   // Maximum raycast distance

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
                if (hit.collider.gameObject == transform.gameObject)
                {
                    GameManager.Instance.KeyCollected();
                }
            }
        }
    }
}


