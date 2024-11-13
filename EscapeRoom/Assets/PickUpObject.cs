using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public Transform player;
    public Transform playerHand;
    public float pickUpRange = 2.0f;
    private bool isHeld = false;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= pickUpRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isHeld)
                Drop();
            else
                PickUp();
        }

        if (isHeld)
        {
            // Keeps the object in the player's hand
            transform.position = playerHand.position;
            transform.rotation = playerHand.rotation;
        }
    }

    void PickUp()
    {
        isHeld = true;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.SetParent(playerHand);
    }

    void Drop()
    {
        isHeld = false;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
    }
}
