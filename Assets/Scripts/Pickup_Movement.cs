
using System.Collections;
using UnityEngine;

public class Pickup_Movement : MonoBehaviour
{
    [Header("Movement Controls")]
    public float rotationSpeed = 0;
    public float floatingSpeed = 0;
    public float floatingHeight = 0;

    private float height = 0;
    private bool floating = false;

    // Update is called once per frame
    void FixedUpdate(){
        transform.Rotate(new Vector3(0, rotationSpeed, 0));

        if (!floating) {
            StartCoroutine(FloatMovement());
            floating = true;
        }
    }

    IEnumerator FloatMovement(){
        while(height < 50) {
            height += 1;
            transform.position = new Vector3(transform.position.x, transform.position.y + floatingHeight / 100, transform.position.z);
            yield return new WaitForSecondsRealtime(.1f / floatingSpeed);
        }
        while(height > 0) {
            height -= 1;
            transform.position = new Vector3(transform.position.x, transform.position.y - floatingHeight / 100, transform.position.z);
            yield return new WaitForSecondsRealtime(.1f / floatingSpeed);
        }
        floating = false;
    }
}
