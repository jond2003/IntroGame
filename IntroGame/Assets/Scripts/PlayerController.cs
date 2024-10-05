using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;
    private int count;
    private int numPickups = 3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI playerPosition;
    public TextMeshProUGUI playerVelocity;
    private Vector3 previousPosition;
    private Vector3 velocity;

    void Start()
    {
        count = 0;
        winText.text = " ";
        SetCountText();
        previousPosition = transform.position;
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);

        // Update the player position
        playerPosition.text = "Position: " + transform.position.ToString("F2");

        // Velocity = difference in position divided by time
        velocity = (transform.position - previousPosition) / Time.fixedDeltaTime;

        // Update previous position
        previousPosition = transform.position;

        // Update the player velocity and speed on the display
        float speedScalar = velocity.magnitude; // Speed as a scalar = magnitude of velocity
        playerVelocity.text = "Velocity: " + velocity.ToString("F2") +
                              "\nSpeed: " + speedScalar.ToString("F2") + " units/second";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        scoreText.text = "Score: " + count.ToString();
        if (count >= numPickups)
        {
            winText.text = "You win!";
        }
    }
}
