using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameBehavior : MonoBehaviour
{
    public GameObject painting;
    private bool isFilled = false;
    private PuzzleManager pm;

    void Start()
    {
        pm = FindAnyObjectByType<PuzzleManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))   // space
        {
            // Calculate the center of the screen
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            RaycastHit hit;

            // Check if the raycast hits this painting object
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object's root or specific parent is the same as this frame's root
                Transform rootTransform = hit.transform.root;

                if (rootTransform == transform || hit.transform.IsChildOf(transform))
                {
                    fillFrame();
                }
            }
        }
    }

    // actual function for handling painting and frame interaction
    // called by the painting object
    private bool fillFrame()
    {
        if (isFilled || !pm.containPainting(painting))
        {
            Debug.Log("Frame is already filled or you have the wrong painting.");
            return false;
        }
        
        isFilled = true;
        pm.removePainting(painting);
        Transform parentTransform = transform.parent != null ? transform.parent : transform;
        painting.GetComponent<PaintingBehavior>().updatePainting(transform);
        return true;
    }
}
