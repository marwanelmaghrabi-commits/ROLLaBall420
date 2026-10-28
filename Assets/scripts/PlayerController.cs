using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text countText;          // UI text showing current count
    public GameObject winTextObject;    // UI object for win message

    [Header("Player Settings")]
    public float speed = 10f;           // Movement speed
    public float maxVelocity = 10f;     // Max speed to prevent runaway ball

    private Rigidbody rb;
    private int count = 0;
    private float movementX;
    private float movementY;
    private int totalPickups = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Rigidbody setup
        rb.linearDamping = 0f;
        rb.angularDamping = 0.05f;
        rb.mass = 1f;
        rb.isKinematic = false;

        // Count all pickups in the scene automatically
        totalPickups = GameObject.FindGameObjectsWithTag("Pickup").Length;

        // Initialize UI
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0f, movementY);
        rb.AddForce(movement * speed);

        // Clamp max speed
        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count;

        // Show win text only when all pickups are collected
        if (count >= totalPickups)
        {
            winTextObject.SetActive(true);
        }
    }
}