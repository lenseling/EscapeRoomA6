using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    public GameObject button; // Reference to the Button (cube) object
    public GameObject buttonText; // Reference to the ButtonText (TextMeshPro) object

    void Update()
    {
        // Detect 'G' key press
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Cast a ray from the center of the screen
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            RaycastHit hit;
            float rayDistance = 100.0f; // Set a reasonable distance for the ray

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                // Check if the ray hit the Button or ButtonText
                if (hit.transform == button.transform || hit.transform == buttonText.transform)
                {
                    Debug.Log("Start");
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Hit another object: " + hit.transform.name);
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any object.");
            }
        }
    }
}
