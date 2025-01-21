
using System.Collections;
using UnityEngine;

public class Pickup_Movement : MonoBehaviour
{
    // Variables to control the motion of the pickups
    [Header("Movement Controls")]
    public float rotationSpeed = 0;
    public float floatingSpeed = 0;
    public float floatingHeight = 0;

    // Variables to assist with runtime calculations
    private float height = 0;
    private bool floating = false;

    // Rotate the pickup and run the floating sequence
    void FixedUpdate() {
        // Rotate pickup based on rotation speed
        transform.Rotate(new Vector3(0, rotationSpeed, 0));

        // If the object is not already in the floating sequence, run it
        if (!floating) {
            StartCoroutine(FloatMovement());
            floating = true;
        }
    }

    // Move the pickup up and down to create the effect of floating
    IEnumerator FloatMovement() {
        // When the pickup has reached the max height, move it down over time
        while(height < 50) {
            height += 1;
            transform.position = new Vector3(transform.position.x, transform.position.y + floatingHeight / 100, transform.position.z);
            yield return new WaitForSecondsRealtime(.1f / floatingSpeed);
        }

        // When the pickup has reached its minimum height, move it up over time
        while(height > 0) {
            height -= 1;
            transform.position = new Vector3(transform.position.x, transform.position.y - floatingHeight / 100, transform.position.z);
            yield return new WaitForSecondsRealtime(.1f / floatingSpeed);
        }
        floating = false;
    }
}
