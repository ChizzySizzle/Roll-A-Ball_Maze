
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    // These variables reference objects in the scene
    [Header("Object References")]
    public Camera playerCamera;
    public Camera miniMapCamera;
    public GameObject Healthbar;
    public Game_Controller gameController;
    public GameObject healthParticles;

    // These variables effect the player's mechanics
    [Header("Player Variables")] 
    public int speed = 0;
    public float health = 1;
    public float wallDamage = .1f;
    public float healthPickupValue = .1f;

    // Variables used for calculations at runtime
    private Rigidbody rb;  
    private float XVector = 0;
    private float ZVector = 0;
    private float CamYOffset = 0;
    private float CamZOffset = 0;
    private float startingHealth;

    // Initialize the player's setting and capture the camera offset
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();

        // Calculate the offset between the player and the camera
        CamYOffset = transform.position.y - playerCamera.transform.position.y;
        CamZOffset = transform.position.z - playerCamera.transform.position.z;

        // Store the player's starting health
        startingHealth = health;
    }

    // Collect player input every frame
    void Update() {
        // Calculating vectors based on player input and speed
        XVector = Input.GetAxis("Horizontal") * speed;
        ZVector = Input.GetAxis("Vertical") * speed;
    }

    // Update the rigidbody velocity based on calculated vectors
    private void FixedUpdate() {
        rb.AddForce(XVector, rb.velocity.y, ZVector);
    }

    // Adjust both cameras positions after the player's position has been updated
    void LateUpdate() {
        // Utilize the camera offset variables to update the camera's position relative to the player
        var playerPosition = transform.position;
        playerCamera.transform.position = new Vector3(playerPosition.x, playerPosition.y - CamYOffset, playerPosition.z - CamZOffset);
        miniMapCamera.transform.position = new Vector3(playerPosition.x, miniMapCamera.transform.position.y, playerPosition.z);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Wall")) {
            
            // Hitting the wall has damaged the player
            health -= wallDamage;

            // Checking to see if the player has died
            if (health <= 0) {
                gameController.YouLose();
                Time.timeScale = 0;
            }
            // Update the healthbar to the new health value
            UpdateHealthBar(health);
            return;
        }
    }

    private void OnTriggerEnter(Collider other) {

        // The player has collided with a health pickup
        if (other.gameObject.CompareTag("HealthUp")) {
            
            // Player will regain a portion of health
            health += healthPickupValue;

            // Particles will burst out of the pickup
            Instantiate(healthParticles, other.transform.position, other.transform.rotation);

            // The health pickup is destroyed
            Destroy(other.gameObject);

            // Make sure the new health value does not exceed the player's total health
            if (health >= startingHealth) {
                health = startingHealth;
            }

            // Update the healthbar to reflect the player's new health value
            UpdateHealthBar(health);
            return;
        }

        // The player has reached the finish line
        if (other.gameObject.CompareTag("Finish")) {
            // Run win sequence in the game controller
            gameController.YouWin();
            Time.timeScale = 0;
            return;
        }
    }

    // Adjusts the health bar value to reflect the player's new health
    private void UpdateHealthBar(float newHealth) {
        Healthbar.GetComponent<Slider>().value = newHealth;
    }
}
