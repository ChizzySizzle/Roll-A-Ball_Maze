
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    // References to the endscreen objects in the scene
    [Header("Endscreens")]
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject endScreenText;

    // Set the timescale to 1 when game begins
    void Awake() {
        Time.timeScale = 1;
    }

    // Quit the application if player presses escape
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    // Run the losing screen sequence
    public void YouLose() {
        // Pass the losing screen to the ending process
        StartCoroutine(Ending(loseScreen));
    }

    // Run the winning screen sequence
    public void YouWin() {
        // Pass the winning screen into the ending process
        StartCoroutine(Ending(winScreen));
    }

    // Fade in the given ending screen and end the game according to result
    IEnumerator Ending(GameObject endScreen) {
        // Get the image and color components of the endscreen
        // Turn up the color transparency over time
        // Set the image to the new alpha value
        Image endScreenImage = endScreen.GetComponent<Image>();
        Color ScreenColor = endScreenImage.color;
        for (float alpha = 0f; alpha <= 1.1; alpha += .05f) {
            ScreenColor.a = alpha;
            endScreenImage.color = ScreenColor;
            yield return new WaitForSecondsRealtime(.05f);
        }

        // If the player won, display the completion time along with the win message
        // Wait ten seconds then auto restart the game
        if (endScreen == winScreen) {
            endScreenText.GetComponent<TMP_Text>().SetText("Completion Time: " + Time.timeSinceLevelLoad.ToString("F2") + " Seconds");
            yield return new WaitForSecondsRealtime(10);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // If the plyer lost, displayt the losing text
        // Wait three seconds then restart thte game
        else {
            endScreenText.GetComponent<TMP_Text>().SetText("You Died");
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
