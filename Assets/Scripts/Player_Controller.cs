using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Object References")]
    public Camera playerCamera;
    private float yOffset = 0;
    private float zOffset = 0;

    [Header("Player Variables")] 
    public int speed = 0;
    public int health = 0;
    private Rigidbody rb;  
    private float XVel = 0;
    private float ZVel = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        yOffset = gameObject.transform.position.y - playerCamera.transform.position.y;
        zOffset = gameObject.transform.position.z - playerCamera.transform.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        XVel = Input.GetAxis("Horizontal") * speed;
        ZVel = Input.GetAxis("Vertical") * speed;

    }

    private void FixedUpdate() {
        rb.velocity = new Vector3(XVel, rb.velocity.y, ZVel);

    }

    void LateUpdate() {
        playerCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - yOffset, gameObject.transform.position.z - zOffset);

    }
}
