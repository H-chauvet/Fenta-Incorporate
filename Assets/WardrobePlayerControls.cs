using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobePlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform targetPosition; // The position the player will move to when 'E' is pressed

    private Rigidbody rb;
    private bool isSliding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isSliding)
        {
            MovePlayer();
            CheckForInteraction();
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * moveSpeed;
    }

    void CheckForInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E) && targetPosition != null)
        {
            isSliding = true;
            transform.position = targetPosition.position; // Move player to the target position
            WardrobeSlide wardrobeSlide = GetComponent<WardrobeSlide>();
            if (wardrobeSlide != null)
            {
                wardrobeSlide.StartSliding(); // Start the sliding transition
            }
        }
    }

    public void SetSliding(bool sliding)
    {
        isSliding = sliding;
    }
}
