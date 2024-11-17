using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RingAlignmentManager : MonoBehaviour
{
    public RingController ringController1; // Reference to the first ring controller
    public RingController ringController2; // Reference to the second ring controller
    public float alignmentTolerance = 10f; // Tolerance for the ring alignment

    private void Update()
    {
        // Check if both rings are aligned
        if (IsRingAligned(ringController1) && IsRingAligned(ringController2))
        {
            GameManager.Instance.musicBoxCompleted = true;
            SwitchToMainScene();

            // If both rings are aligned, switch to the main scene
        }
    }

    // Method to check if a ring is aligned
    private bool IsRingAligned(RingController ringController)
    {
        // Check if the ring is aligned within the specified tolerance
        float ringAngle = Mathf.Abs(ringController.transform.eulerAngles.x);
        return Mathf.Abs(ringAngle - ringController.correctAngle) <= alignmentTolerance;
    }

    // Method to switch back to the main scene
    private void SwitchToMainScene()
    {
        // Load the main scene (you can replace "MainScene" with the actual scene name)
        SceneManager.LoadScene("EscapeRoom");
    }
}
