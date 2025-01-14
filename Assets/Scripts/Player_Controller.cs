using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Camera PCam;
    public int speed = 0;
    [SerializeField]
    private int health = 0;
    private Rigidbody rb;

    float XVel = 0;
    float ZVel = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        XVel = Input.GetAxis("Horizontal") * speed;
        ZVel = Input.GetAxis("Vertical") * speed;

        rb.velocity = new Vector3(XVel, 0, ZVel);
        
    }

    void LateUpdate() {
        PCam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 7, gameObject.transform.position.z - 3);

    }
}
