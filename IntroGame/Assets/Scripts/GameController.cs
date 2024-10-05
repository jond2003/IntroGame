using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] pickups;          // Store all pickup objects
    public Transform playerTransform;     // Reference player position
    public TextMeshProUGUI distanceText;  // Display the distance
    private LineRenderer lineRenderer;    // Draw a line between player and closest pickup
    private GameObject closestPickup;     // Store the closest pickup object

    void Start()
    {
        // Initialize LineRenderer and set its properties
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // Find and give the player transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Find the closest pickup
        FindClosestPickup();
    }

    void FindClosestPickup()
    {
        float minDistance = Mathf.Infinity;
        closestPickup = null;

        // Loop through all pickups
        foreach (GameObject pickup in pickups)
        {
            if (pickup.activeSelf)  // Only acknowledge active pickups
            {
                // Distance between player and the pickup
                float distance = Vector3.Distance(playerTransform.position, pickup.transform.position);

                // Closest?
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPickup = pickup;
                }

                // Reset all pickup colors to white
                pickup.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        if (closestPickup != null)
        {
            // Change the closest pickup's color to blue
            closestPickup.GetComponent<Renderer>().material.color = Color.blue;

            // Update the distance text
            distanceText.text = "Distance to closest pickup: " + minDistance.ToString("F2") + " units";

            // Draw a line from the player to the closest pickup
            lineRenderer.SetPosition(0, playerTransform.position);
            lineRenderer.SetPosition(1, closestPickup.transform.position);
        }
        else
        {
            // When u collect all pickups
            distanceText.text = "No active pickups are available.";
        }
    }

}
