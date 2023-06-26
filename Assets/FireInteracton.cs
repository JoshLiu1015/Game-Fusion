using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FireInteraction : MonoBehaviour
{
    public GameObject redStonePrefab; // Your red stone prefab here
    private bool handInFire = false;
    private XRController controller;

    void Start()
    {
        controller = GetComponent<XRController>();
    }

    void Update()
    {
        if (handInFire)
        {
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed) && isPressed)
            {
                // Instantiate the red stone at the fire's position
                Instantiate(redStonePrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the hand is entering the fire
        if (other.CompareTag("hand") || other.CompareTag("Player")) // Make sure to add a "Hand" tag to your hand objects
        {
            handInFire = true;
            Instantiate(redStonePrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the hand is leaving the fire
        if (other.CompareTag("hand")) // Make sure to add a "Hand" tag to your hand objects
        {
            handInFire = false;
        }
    }
}


