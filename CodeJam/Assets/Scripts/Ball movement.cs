using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float movementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal and vertical input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement based on input and speed
        Vector2 movement = new Vector2(horizontalInput * movementSpeed, verticalInput * movementSpeed);

        // Apply movement to the object's position
        transform.Translate(movement * Time.deltaTime);
    }
}


