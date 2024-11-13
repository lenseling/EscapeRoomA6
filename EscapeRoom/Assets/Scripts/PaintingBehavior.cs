using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingBehavior : MonoBehaviour
{
    private bool isPickedup = false;
    private PuzzleManager pm;

    private void Start()
    {
        pm = FindAnyObjectByType<PuzzleManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))   // space
        {
            //Debug.Log("Painting Pressed");
            // Calculate the center of the screen
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            RaycastHit hit;

            // Check if the raycast hits this painting object
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    pickup();
                }
            }
        }
    }

        // actual function for handling picking up paintings
        public bool pickup()
    {
        if (!isPickedup && pm.addPainting(gameObject))
        {
            isPickedup = true;
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    // place painting in the frame
    public void updatePainting(Transform targetTransform)
    {
        Transform cubeTransform = targetTransform.Find("Cube (1)");
        cubeTransform.gameObject.SetActive(false);

        if (cubeTransform == null)
        {
            Debug.LogWarning("Cube (1) child object not found under the target transform.");
            return;
        }
        transform.position = cubeTransform.position;
        //transform.rotation = cubeTransform.rotation;
        Quaternion targetRotation = cubeTransform.rotation;
        transform.rotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        gameObject.SetActive(true);
    }
}
